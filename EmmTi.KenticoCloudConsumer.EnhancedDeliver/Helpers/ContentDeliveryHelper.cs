using EmmTi.KenticoCloudConsumer.EnhancedDeliver.Interfaces;
using KenticoCloud.Deliver;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmmTi.KenticoCloudConsumer.EnhancedDeliver.Helpers
{
    /// <summary>
    /// Content Delivery Helper to interpret Kentico Cloud Content Elements
    /// </summary>
    public static class ContentDeliveryHelper
    {
        /// <summary>
        /// Gets the date time or default value specified.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="elementCode">The element code name.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The date time if the element is not null or the default value if the element is null</returns>
        public static DateTime GetDateTimeOrDefault(this ContentItem content, string elementCode, DateTime defaultValue = default(DateTime))
        {
            return content.Elements[elementCode] == null ? defaultValue : content.GetDateTime(elementCode);
        }

        /// <summary>
        /// Gets a list of modular content.
        /// </summary>
        /// <typeparam name="T">Type of IKenticoDeliverModel</typeparam>
        /// <param name="items">The items.</param>
        /// <param name="currentDepth">The current depth of this item in a recursive tree</param>
        /// <returns>A List of Modular content</returns>
        public static List<T> GetListOfModularContent<T>(this IEnumerable<ContentItem> items, int currentDepth = 0) where T : IKenticoDeliverViewModel, new()
        {
            var modularList = new List<T>();

            foreach (var module in items)
            {
                var model = new T();
                model.MapContent(module, currentDepth);
                modularList.Add(model);
            }

            return modularList;
        }

        /// <summary>
        /// Gets the modular content or default.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="elementCode">The element code name.</param>
        /// <returns>The modular content if the element is not null or the default value if the element is null</returns>
        public static IEnumerable<ContentItem> GetModularContentOrDefault(this ContentItem content, string elementCode)
        {
            return content.Elements[elementCode] == null ? new List<ContentItem>() : content.GetModularContent(elementCode);
        }

        /// <summary>
        /// Gets the number or default.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="elementCode">The element code.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The double value if the element is not null or the default value if the element is null</returns>
        public static double GetNumberOrDefault(this ContentItem content, string elementCode, double defaultValue = 0)
        {
            return content.Elements[elementCode] == null ? defaultValue : content.GetNumber(elementCode);
        }

        /// <summary>
        /// Gets the selected taxonomy.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="elementCode">The element code name.</param>
        /// <returns>Taxonomy as a Key Value Pair</returns>
        public static KeyValuePair<string, string> GetSelectedTaxonomy(this ContentItem content, string elementCode)
        {
            var element = content.Elements[elementCode];
            return element == null || element.value.Count == 0 ? new KeyValuePair<string, string>() : new KeyValuePair<string, string>(element.value[0].codename.ToString(), element.value[0].name.ToString());
        }

        /// <summary>
        /// Gets the string or default.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="elementCode">The element code name.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The string value if the element is not null or the default value if the element is null</returns>
        public static string GetStringOrDefault(this ContentItem content, string elementCode, string defaultValue = "")
        {
            return content.Elements[elementCode] == null ? defaultValue : content.GetString(elementCode);
        }

        /// <summary>
        /// Gets the taxonomy items.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="elementCode">The element code name.</param>
        /// <returns>A Dictionary of taxonomy items</returns>
        public static Dictionary<string, string> GetTaxonomyItems(this ContentItem content, string elementCode)
        {
            var element = content.Elements[elementCode];
            var taxonomyList = new Dictionary<string, string>();

            if (element == null || element.value == null)
            {
                return taxonomyList;
            }

            foreach (var item in element.value)
            {
                taxonomyList.Add(item.codename.ToString(), item.name.ToString());
            }

            return taxonomyList;
        }

        /// <summary>
        /// Maps the content to the Model.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content">The content.</param>
        /// <param name="currentDepth">The current depth.</param>
        /// <returns>Content mapped to the Model</returns>
        public static T MapContent<T>(this ContentItem content, int currentDepth = 0) where T : IKenticoDeliverViewModel, new()
        {
            var model = new T();
            model.MapContent(content, currentDepth);
            return model;
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