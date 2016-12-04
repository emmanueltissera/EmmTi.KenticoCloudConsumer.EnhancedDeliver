using EmmTi.KenticoCloudConsumer.EnhancedDeliver.Helpers;
using EmmTi.KenticoCloudConsumer.EnhancedDeliver.Interfaces;
using KenticoCloud.Deliver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmmTi.KenticoCloudConsumer.EnhancedDeliver.Factories
{
    /// <summary>
    /// Gets items from Kentico Cloud and caches locally
    /// </summary>
    /// <typeparam name="T">Class derived from <seealso cref="EmmTi.KenticoCloudConsumer.EnhancedDeliver.Interfaces.IKenticoDeliverViewModel" /></typeparam>
    /// <seealso cref="EmmTi.KenticoCloudConsumer.EnhancedDeliver.Factories.StaticDeliverClient" />
    public class DeliverClientFactory<T> : StaticDeliverClient where T : IKenticoDeliverViewModel, new()
    {
        /// <summary>
        /// Synchronously gets a single item.
        /// </summary>
        /// <param name="itemCodename">The item codename.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>Cached item of Type T</returns>
        public static T GetItem(string itemCodename, IEnumerable<IFilter> parameters = null)
        {
            return Task.Run(() => GetCachedItemAsync(itemCodename, parameters)).Result;
        }

        /// <summary>
        ///  Asynchronously gets a single item.
        /// </summary>
        /// <param name="itemCodename">The item codename.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>Cached item of Type T</returns>
        public static async Task<T> GetItemAsync(string itemCodename, IEnumerable<IFilter> parameters = null)
        {
            return await GetCachedItemAsync(itemCodename, parameters);
        }

        /// <summary>
        /// Asynchronously gets a single item specified by identifier.
        /// </summary>
        /// <param name="itemId">The item identifier.</param>
        /// <returns>Cached item of Type T</returns>
        public static async Task<T> GetItemByIdAsync(string itemId)
        {
            itemId = UrlHelper.GetKenticoIdFromUrl(itemId);
            return await GetItemAsync(itemId);
        }

        /// <summary>
        /// Asynchronously gets multiple items.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>Cached items of Type T</returns>
        public static async Task<T> GetItemsAsync(IEnumerable<IFilter> parameters = null)
        {
            return await GetCachedItemsAsync(parameters);
        }

        /// <summary>
        /// Asynchronously gets the cached item.
        /// </summary>
        /// <param name="itemCodename">The item codename.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>Cached item of Type T</returns>
        private static async Task<T> GetCachedItemAsync(string itemCodename, IEnumerable<IFilter> parameters = null)
        {
            var enumerableParams = parameters as IList<IFilter> ?? parameters?.ToList();
            var cacheKey = $"dcf-cache-{itemCodename ?? string.Empty}-{enumerableParams.StringfyFilter()}";
            return await ContentCache.AddOrGetExisting(cacheKey, () => GetItemAsyncInternal(itemCodename, enumerableParams));
        }

        /// <summary>
        /// Asynchronously gets the cached items.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>Cached items of Type T</returns>
        private static async Task<T> GetCachedItemsAsync(IEnumerable<IFilter> parameters = null)
        {
            var enumerableParams = parameters as IList<IFilter> ?? parameters?.ToList();
            var cacheKey = $"dcf-cache-items-{enumerableParams.StringfyFilter()}";
            return await ContentCache.AddOrGetExisting(cacheKey, () => GetItemsAsyncInternal(enumerableParams));
        }

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>Item of Type T</returns>
        private static T GetItem(ContentItem content)
        {
            var item = new T();
            item.MapContent(content);
            return item;
        }

        /// <summary>
        /// Asynchronously gets the item (internal method).
        /// </summary>
        /// <param name="itemCodename">The item codename.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>Items of Type T</returns>
        private static async Task<T> GetItemAsyncInternal(string itemCodename, IEnumerable<IFilter> parameters = null)
        {
            var response = await Client.GetItemAsync(itemCodename, parameters);
            return GetItem(response.Item);
        }

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <param name="contentList">The content list.</param>
        /// <returns>Items of Type T</returns>
        private static T GetItems(List<ContentItem> contentList)
        {
            var item = new T();
            item.MapContentList(contentList);
            return item;
        }

        /// <summary>
        /// Asynchronously gets the items (internal method).
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>Items of Type T</returns>
        private static async Task<T> GetItemsAsyncInternal(IEnumerable<IFilter> parameters = null)
        {
            var response = await Client.GetItemsAsync(parameters);
            return GetItems(response.Items);
        }
    }
}