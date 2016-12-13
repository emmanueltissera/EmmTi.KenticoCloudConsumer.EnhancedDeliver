using EmmTi.KenticoCloudConsumer.EnhancedDeliver.Helpers;
using EmmTi.KenticoCloudConsumer.EnhancedDeliver.Interfaces;
using KenticoCloud.Deliver;
using System.Collections.Generic;

namespace EmmTi.KenticoCloudConsumer.EnhancedDeliver.Models
{
    /// <summary>
    /// Base Content View Model for all Kentico Cloud Content Models
    /// </summary>
    /// <seealso cref="EmmTi.KenticoCloudConsumer.EnhancedDeliver.Interfaces.IKenticoDeliverViewModel" />
    public class BaseContentItemViewModel : IKenticoDeliverViewModel
    {
        /// <summary>
        /// Gets or sets the maximum depth for recursive functions.
        /// </summary>
        /// <value>
        /// The maximum depth recursive functions.
        /// </value>
        public int MaxDepth { get; set; } = 2;

        /// <summary>
        /// Gets or sets the parent path.
        /// </summary>
        /// <value>
        /// The parent path.
        /// </value>
        public string ParentPath { get; set; }

        /// <summary>
        /// Gets or sets the system.
        /// </summary>
        /// <value>
        /// The system.
        /// </value>
        public KenticoCloud.Deliver.System System { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        public string Url { get; set; }

        /// <summary>
        /// Maps the content for the current type.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="currentDepth">The current depth of this item in a recursive tree</param>
        public void MapContent(ContentItem content, int currentDepth = 0)
        {
            if (currentDepth > MaxDepth || content.Elements == null)
            {
                return;
            }

            MapCommonContentFields(content);
            MapContentForType(content, currentDepth);
        }

        /// <summary>
        /// Maps the content list for the current type.
        /// </summary>
        /// <param name="contentList">The content list.</param>
        /// <param name="currentDepth">The current depth of this item in a recursive tree</param>
        public void MapContentList(List<ContentItem> contentList, int currentDepth = 0)
        {
            if (currentDepth > MaxDepth || contentList == null || contentList.Count == 0)
            {
                return;
            }

            MapContentListForType(contentList, currentDepth);
        }

        /// <summary>
        /// Maps the common content fields.
        /// </summary>
        /// <param name="content">The content.</param>
        protected virtual void MapCommonContentFields(ContentItem content)
        {
            ParentPath = UrlHelper.GetFriendlyParentPath(content.System);
            System = content.System;
            Url = UrlHelper.GetFriendlyUrl(content.System);
        }

        /// <summary>
        /// Maps the content for the current type.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="currentDepth">The current depth of this item in a recursive tree</param>
        /// <remarks>Should be overridden by derived types</remarks>
        protected virtual void MapContentForType(ContentItem content, int currentDepth)
        {
        }
        
        /// <summary>
        /// Maps the content list for the current type.
        /// </summary>
        /// <param name="contentList">The content list.</param>
        /// <param name="currentDepth">The current depth of this item in a recursive tree</param>
        /// <remarks>Should be overridden by derived types</remarks>
        protected virtual void MapContentListForType(List<ContentItem> contentList, int currentDepth)
        {
        }
    }
}