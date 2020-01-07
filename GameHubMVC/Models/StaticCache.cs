using Microsoft.AspNetCore.Http;
using Microsoft.Identity.Client;
using System.Collections.Generic;
using System.Threading;

namespace GameHubMVC.Models
{
    public class MSALStaticCache
    {
        private static Dictionary<string, byte[]> staticCache = new Dictionary<string, byte[]>();

        private static readonly ReaderWriterLockSlim SessionLock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
        private readonly string _userId;
        private readonly string _cacheId;
        private readonly HttpContext _httpContext = null;
        private ITokenCache _cache;

        public MSALStaticCache(string userId, HttpContext httpcontext)
        {
            // not object, we want the SUB
            this._userId = userId;
            _cacheId = this._userId + "_TokenCache";
            _httpContext = httpcontext;
        }

        public ITokenCache EnablePersistence(ITokenCache cache)
        {
            this._cache = cache;
            cache.SetBeforeAccess(BeforeAccessNotification);
            cache.SetAfterAccess(AfterAccessNotification);
            return cache;
        }

        public void Load(TokenCacheNotificationArgs args)
        {
            SessionLock.EnterReadLock();
            byte[] blob = staticCache.ContainsKey(_cacheId) ? staticCache[_cacheId] : null;
            if (blob != null)
            {
                args.TokenCache.DeserializeMsalV3(blob);
            }
            SessionLock.ExitReadLock();
        }

        public void Persist(TokenCacheNotificationArgs args)
        {
            SessionLock.EnterWriteLock();

            // Reflect changes in the persistent store
            staticCache[_cacheId] = args.TokenCache.SerializeMsalV3();
            SessionLock.ExitWriteLock();
        }

        // Triggered right before MSAL needs to access the cache.
        // Reload the cache from the persistent store in case it changed since the last access.
        void BeforeAccessNotification(TokenCacheNotificationArgs args)
        {
            Load(args);
        }

        // Triggered right after MSAL accessed the cache.
        void AfterAccessNotification(TokenCacheNotificationArgs args)
        {
            // if the access operation resulted in a cache update
            if (args.HasStateChanged)
            {
                Persist(args);
            }
        }
    }

}
