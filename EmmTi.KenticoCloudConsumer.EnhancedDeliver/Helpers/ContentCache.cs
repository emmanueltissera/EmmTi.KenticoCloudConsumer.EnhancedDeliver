using System;
using System.Configuration;
using System.Runtime.Caching;

namespace EmmTi.KenticoCloudConsumer.EnhancedDeliver.Helpers
{
    /// <summary>
    /// Cache Helper for caching content
    /// </summary>
    public class ContentCache
    {
        /// <summary>
        /// The cache
        /// </summary>
        private static readonly MemoryCache Cache = new MemoryCache("ContentCache");

        /// <summary>
        /// The cache expiry in seconds
        /// </summary>
        private static readonly int CacheExpiryInSeconds = GetCacheExpiryValue(600);

        /// <summary>
        /// Adds the or get existing cache Items.
        /// </summary>
        /// <typeparam name="T">Generic Type</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="valueFactory">The value factory.</param>
        /// <returns>Cached Value or Instantiated Value</returns>
        public static T AddOrGetExisting<T>(string key, Func<T> valueFactory)
        {
            var newValue = new Lazy<T>(valueFactory);
            var cacheItemPolicy = new CacheItemPolicy()
            {
                AbsoluteExpiration = new DateTimeOffset(DateTime.UtcNow.AddSeconds(CacheExpiryInSeconds))
            };
            var oldValue = Cache.AddOrGetExisting(key, newValue, cacheItemPolicy) as Lazy<T>;
            try
            {
                return (oldValue ?? newValue).Value;
            }
            catch
            {
                // Handle cached lazy exception by evicting from cache.
                Cache.Remove(key);
                return newValue.Value;
            }
        }

        /// <summary>
        /// Gets the cache expiry value.
        /// </summary>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>Cache Expiry from the web.config</returns>
        private static int GetCacheExpiryValue(int defaultValue)
        {
            int numberOfSeconds;
            if (int.TryParse(ConfigurationManager.AppSettings["DeliveryContentCacheTimeSeconds"], out numberOfSeconds))
            {
                return numberOfSeconds;
            }

            return defaultValue;
        }
    }
}