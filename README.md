# GameHub

<div>
  <img src="https://gamehubmvc.vikyn.io/Images/BeatTheBanker/BeatTheBanker01.PNG" alt="GameHub Image"/>
  <br/>
  <p>A fun website for gaming built with a .Net Core Web Application that calls a .Net Core Web API, both secured using Azure AD B2C. That leverages the PlayFab API for the games backend.</p>
  <p>
    Want to see it live in action, goto <a href="https://bit.ly/_gamehub">https://bit.ly/_gamehub</a>
    <br/>
    If you don't want to create a personal account, use these credentials to test..
    <br/>
    <code>Email</code>: gamehub@mailinator.com
    <br/>
    <code>Password</code>: Gamehub2020
    </p>
</div>

<div>
  <h2>Getting Started</h2>
  
  <h3>The sample covers the following: </h3>
  <ul>
    <li>Calling an OpenID connect Identity Provider (Azure AD B2C)</li>
    <li>Acquiring a token from Azure AD B2C using MSAL</li>
    <li>Use acquired token to access protected API resources</li>
    <li>Leverage the PlayFab API to create and maintian games/players</li>
  </ul>
   <h3>Project Structure: </h3>
  <ul>
    <li>API: Brains of the application</li>
    <li>MVC: Frontend of the product, using MVC.</li>
    <li>Shared: Models and libraries shared between applications</li>
  </ul>
  <h3>A few of the technologies used: </h3>
  <ul>
    <li>.Net Core 2.2 --> TODO: Update to 3.X</li>
    <li>Bootstrap 4, JQuery, Knockout</li>
    <li>Entity Framework, Linq, Docker, Dependecy Injection, CI/CD</li>
  </ul>
</div>
<div>
  <h2>Prerequisites</h2>
  
  <h3><a id="user-content-using-your-own-azure-ad-b2c-tenant" class="anchor" aria-hidden="true" href="#using-your-own-azure-ad-b2c-tenant"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M4 9h1v1H4c-1.5 0-3-1.69-3-3.5S2.55 3 4 3h4c1.45 0 3 1.69 3 3.5 0 1.41-.91 2.72-2 3.25V8.59c.58-.45 1-1.27 1-2.09C10 5.22 8.98 4 8 4H4c-.98 0-2 1.22-2 2.5S3 9 4 9zm9-3h-1v1h1c1 0 2 1.22 2 2.5S13.98 12 13 12H9c-.98 0-2-1.22-2-2.5 0-.83.42-1.64 1-2.09V6.25c-1.09.53-2 1.84-2 3.25C6 11.31 7.55 13 9 13h4c1.45 0 3-1.69 3-3.5S14.5 6 13 6z"></path></svg></a>Using your own Azure AD B2C Tenant</h3>
  <p>In this section, you'll learn how to configure the ASP.NET Web Application and the ASP.NET Web API to work with your own Azure AD B2C Tenant.</p>

  <h4><a id="user-content-step-1-get-your-own-azure-ad-b2c-tenant" class="anchor" aria-hidden="true" href="#step-1-get-your-own-azure-ad-b2c-tenant"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M4 9h1v1H4c-1.5 0-3-1.69-3-3.5S2.55 3 4 3h4c1.45 0 3 1.69 3 3.5 0 1.41-.91 2.72-2 3.25V8.59c.58-.45 1-1.27 1-2.09C10 5.22 8.98 4 8 4H4c-.98 0-2 1.22-2 2.5S3 9 4 9zm9-3h-1v1h1c1 0 2 1.22 2 2.5S13.98 12 13 12H9c-.98 0-2-1.22-2-2.5 0-.83.42-1.64 1-2.09V6.25c-1.09.53-2 1.84-2 3.25C6 11.31 7.55 13 9 13h4c1.45 0 3-1.69 3-3.5S14.5 6 13 6z"></path></svg></a>Step 1: Get your own Azure AD B2C tenant</h4>
  <p>First, you'll need an Azure AD B2C tenant. If you don't have an existing Azure AD B2C tenant that you can use for testing purposes, you can create your own by following these <a href="https://azure.microsoft.com/documentation/articles/active-directory-b2c-get-started/" rel="nofollow">instructions</a>.</p>
  <h4><a id="user-content-step-2-create-your-own-policies" class="anchor" aria-hidden="true" href="#step-2-create-your-own-policies"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M4 9h1v1H4c-1.5 0-3-1.69-3-3.5S2.55 3 4 3h4c1.45 0 3 1.69 3 3.5 0 1.41-.91 2.72-2 3.25V8.59c.58-.45 1-1.27 1-2.09C10 5.22 8.98 4 8 4H4c-.98 0-2 1.22-2 2.5S3 9 4 9zm9-3h-1v1h1c1 0 2 1.22 2 2.5S13.98 12 13 12H9c-.98 0-2-1.22-2-2.5 0-.83.42-1.64 1-2.09V6.25c-1.09.53-2 1.84-2 3.25C6 11.31 7.55 13 9 13h4c1.45 0 3-1.69 3-3.5S14.5 6 13 6z"></path></svg></a>Step 2: Create your own policies</h4>
  <p>This sample uses three types of policies: a unified sign-up/sign-in policy, a profile editing policy, and a password reset policy. Create one policy of each type by following <a href="https://azure.microsoft.com/documentation/articles/active-directory-b2c-reference-policies" rel="nofollow">the built-in policy instructions</a>. You may choose to include as many or as few identity providers as you wish.</p>
  <p>This sample uses three types of policies: a unified sign-up/sign-in policy, a profile editing policy, and a password reset policy. Create one policy of each type by following <a href="https://azure.microsoft.com/documentation/articles/active-directory-b2c-reference-policies" rel="nofollow">the built-in policy instructions</a>. You may choose to include as many or as few identity providers as you wish.</p>
  <p>Make sure that all the three policies return <strong>User's Object ID</strong> and <strong>Display Name</strong> on <strong>Application Claims</strong>. To do that, on Azure Portal, go to your B2C Directory then click <strong>User flows (policies)</strong> on the left menu and select your policy. Then click on <strong>Application claims</strong> and make sure that <strong>User's Object ID</strong> and <strong>Display Name</strong> is checked.</p>
  
  </div>

