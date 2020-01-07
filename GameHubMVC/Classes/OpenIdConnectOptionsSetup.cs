using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GameHubMVC.Models;
using System.Net.Http;
using GameHub.Models;
using GameHubMVC.Interfaces;
using Newtonsoft.Json;

namespace GameHubMVC.Classes
{
    public static class AzureAdB2CAuthenticationBuilderExtensions
    {
        public static AuthenticationBuilder AddAzureAdB2C(this AuthenticationBuilder builder)
            => builder.AddAzureAdB2C(_ =>
            {
            });

        public static AuthenticationBuilder AddAzureAdB2C(this AuthenticationBuilder builder, Action<AzureAdB2COptions> configureOptions)
        {
            builder.Services.Configure(configureOptions);
            builder.Services.AddSingleton<IConfigureOptions<OpenIdConnectOptions>, OpenIdConnectOptionsSetup>();
            builder.Services.AddSingleton<IUserContext, MVCUserContext>();
            builder.AddOpenIdConnect();
            return builder;
        }

        public class OpenIdConnectOptionsSetup : IConfigureNamedOptions<OpenIdConnectOptions> 
        {

            public OpenIdConnectOptionsSetup(IOptions<AzureAdB2COptions> b2cOptions, IUserContext userContext)
            {
                AzureAdB2COptions = b2cOptions.Value;
                AzureB2COptions = b2cOptions;
                UserContext = userContext;
            }

            public AzureAdB2COptions AzureAdB2COptions { get; set; }
            public IOptions<AzureAdB2COptions> AzureB2COptions { get; set; }
            public IUserContext UserContext { get; set; }

            public void Configure(string name, OpenIdConnectOptions options)
            {
                options.ClientId = AzureAdB2COptions.ClientId;
                options.Authority = AzureAdB2COptions.Authority;
                options.UseTokenLifetime = true;
                options.TokenValidationParameters = new TokenValidationParameters() { NameClaimType = "name" };

                options.Events = new OpenIdConnectEvents()
                {
                    OnRedirectToIdentityProvider = OnRedirectToIdentityProvider,
                    OnRemoteFailure = OnRemoteFailure,
                    OnAuthorizationCodeReceived = OnAuthorizationCodeReceived
                };
            }

            public void Configure(OpenIdConnectOptions options)
            {
                Configure(Options.DefaultName, options);
            }

            public Task OnRedirectToIdentityProvider(RedirectContext context)
            {
                var defaultPolicy = AzureAdB2COptions.DefaultPolicy;
                if (context.Properties.Items.TryGetValue(AzureAdB2COptions.PolicyAuthenticationProperty, out var policy) &&
                    !policy.Equals(defaultPolicy))
                {
                    context.ProtocolMessage.Scope = OpenIdConnectScope.OpenIdProfile;
                    context.ProtocolMessage.ResponseType = OpenIdConnectResponseType.IdToken;
                    context.ProtocolMessage.IssuerAddress = context.ProtocolMessage.IssuerAddress.ToLower().Replace(defaultPolicy.ToLower(), policy.ToLower());
                    context.Properties.Items.Remove(AzureAdB2COptions.PolicyAuthenticationProperty);
                }
                else if (!string.IsNullOrEmpty(AzureAdB2COptions.ApiUrl))
                {
                    context.ProtocolMessage.Scope += $" offline_access {AzureAdB2COptions.ApiScopes}";
                    context.ProtocolMessage.ResponseType = OpenIdConnectResponseType.CodeIdToken;
                }
                return Task.FromResult(0);
            }

            public Task OnRemoteFailure(RemoteFailureContext context)
            {
                context.HandleResponse();
                switch (context.Failure)
                {
                    // Handle the error code that Azure AD B2C throws when trying to reset a password from the login page 
                    // because password reset is not supported by a "sign-up or sign-in policy"
                    case OpenIdConnectProtocolException _ when context.Failure.Message.Contains("AADB2C90118"):
                        // If the user clicked the reset password link, redirect to the reset password route
                        context.Response.Redirect("/Session/ResetPassword");
                        break;
                    case OpenIdConnectProtocolException _ when context.Failure.Message.Contains("access_denied"):
                        context.Response.Redirect("/");
                        break;
                    default:
                        context.Response.Redirect("/Home/Error?message=" + Uri.EscapeDataString(context.Failure.Message));
                        break;
                }
                return Task.FromResult(0);
            }

            public async Task OnAuthorizationCodeReceived(AuthorizationCodeReceivedContext context)
            {
                // Use MSAL to swap the code for an access token
                // Extract the code from the response notification
                var code = context.ProtocolMessage.Code;
                var signedInUserId = context.Principal.FindFirst(ClaimTypes.NameIdentifier).Value;
                var cca = ConfidentialClientApplicationBuilder.Create(AzureAdB2COptions.ClientId)
                    .WithB2CAuthority(AzureAdB2COptions.Authority)
                    .WithRedirectUri(AzureAdB2COptions.RedirectUri)
                    .WithClientSecret(AzureAdB2COptions.ClientSecret)
                    .Build();
                new MSALStaticCache(signedInUserId, context.HttpContext).EnablePersistence(cca.UserTokenCache);

                var result = await cca.AcquireTokenByAuthorizationCode(AzureAdB2COptions.ApiScopes.Split(' '), code)
                    .ExecuteAsync();
                try
                {
                    var claims = ConvertClaims(context.Principal);
                    var json = JsonConvert.SerializeObject(claims);
                    context.HandleCodeRedemption(result.AccessToken, result.IdToken);
                    var x = await ApiHelper.ApiCallAsync($"{AzureAdB2COptions.ApiUrl}/user/login", json, HttpMethod.Post, UserContext, AzureB2COptions, signedInUserId);
                }
                catch (Exception ex)
                {
                    //TODO: Handle
                    throw;
                }
            }

            private static UserModel ConvertClaims(ClaimsPrincipal claims)
            {
                try
                {
                    var converted = new UserModel()
                    {
                        FirstName = claims.FindFirst(ClaimTypes.GivenName).Value ?? "",
                        LastName = claims.FindFirst(ClaimTypes.Surname).Value ?? "",
                        Email = claims.Claims.FirstOrDefault(a => a.Type == "emails")?.Value ?? "",
                        Username = claims.Claims.FirstOrDefault(a => a.Type == "name")?.Value ?? "",
                        GUID = claims.FindFirst(ClaimTypes.NameIdentifier).Value ?? "",
                    };
                    return converted;
                }
                catch (Exception exception)
                {
                    return new UserModel() { ErrorFlag = true, Message = exception.Message };
                }
            }
        }
    }

}
