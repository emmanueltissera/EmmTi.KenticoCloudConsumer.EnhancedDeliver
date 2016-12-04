using System.Collections.Generic;
using System.Text;
using EmmTi.KenticoCloudConsumer.EnhancedDeliver.Interfaces;
using KenticoCloud.Deliver;

namespace EmmTi.KenticoCloudConsumer.EnhancedDeliver.Helpers
{
    /// <summary>
    /// Content Delivery Helper to interpret Kentico Cloud Content Elements
    /// </summary>
    public static class ContentDeliveryHelper
    {
        /// <summary>
        /// Gets a list of modular content.
        /// </summary>
        /// <typeparam name="T">Type of IKenticoDeliverModel</typeparam>
        /// <param name="items">The items.</param>
        /// <returns>A List of Modular content</returns>
        public static List<T> GetListOfModularContent<T>(this IEnumerable<ContentItem> items) where T : IKenticoDeliverViewModel, new()
        {
            var modularList = new List<T>();

            foreach (var module in items)
            {
                var model = new T();
                model.MapContent(module);
                modularList.Add(model);
            }

            return modularList;
        }

        /// <summary>
        /// Gets the selected taxonomy.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="elementCode">The element code.</param>
        /// <returns>Taxonomy as a Key Value Pair</returns>
        public static KeyValuePair<string, string> GetSelectedTaxonomy(this ContentItem content, string elementCode)
        {
            var element = content.Elements[elementCode];

            return element != null && element.value != null ? new KeyValuePair<string, string>(element.value[0].codename.ToString(), element.value[0].name.ToString()) : new KeyValuePair<string, string>();
        }

        /// <summary>
        /// Makes a string version of the filter used to retrieve Kentico Cloud content.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>A string version to be used as a cache key</returns>
        public static string StringfyFilter(this IEnumerable<IFilter> parameters)
        {
            if (parameters == null)
            {
                return "no-params";
            }

            var filterBuilder = new StringBuilder();
            foreach (var filter in parameters)
            {
                filterBuilder.Append($"{filter.GetQueryStringParameter()}-");
            }

            return filterBuilder.ToString().ToLower();
        }
    }
}