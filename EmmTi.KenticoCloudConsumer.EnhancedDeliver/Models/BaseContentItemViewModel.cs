using System.Collections.Generic;
using EmmTi.KenticoCloudConsumer.EnhancedDeliver.Helpers;
using EmmTi.KenticoCloudConsumer.EnhancedDeliver.Interfaces;
using KenticoCloud.Deliver;

namespace EmmTi.KenticoCloudConsumer.EnhancedDeliver.Models
{
    /// <summary>
    /// Base Content View Model for all Kentico Cloud Content Models
    /// </summary>
    /// <seealso cref="EmmTi.KenticoCloudConsumer.EnhancedDeliver.Interfaces.IKenticoDeliverViewModel" />
    public class BaseContentItemViewModel : IKenticoDeliverViewModel
    {
        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the system.
        /// </summary>
        /// <value>
        /// The system.
        /// </value>
        public KenticoCloud.Deliver.System System { get; set; }

        /// <summary>
        /// Maps the content for the current type.
        /// </summary>
        /// <param name="content">The content.</param>
        public void MapContent(ContentItem content)
        {
            MapCommonContentFields(content);
            MapContentForType(content);
        }

        /// <summary>
        /// Maps the content list for the current type.
        /// </summary>
        /// <param name="contentList">The content list.</param>
        public void MapContentList(List<ContentItem> contentList)
        {
            MapContentListForType(contentList);
        }

        /// <summary>
        /// Maps the content for the current type.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <remarks>Should be overridden by derived types</remarks>
        protected virtual void MapContentForType(ContentItem content)
        {
        }

        /// <summary>
        /// Maps the content list for the current type.
        /// </summary>
        /// <param name="contentList">The content list.</param>
        /// <remarks>Should be overridden by derived types</remarks>
        protected virtual void MapContentListForType(List<ContentItem> contentList)
        {
        }

        /// <summary>
        /// Maps the common content fields.
        /// </summary>
        /// <param name="content">The content.</param>
        protected virtual void MapCommonContentFields(ContentItem content)
        {
            System = content.System;
            Url = UrlHelper.GetFriendlyUrl(content.System);
        }
    }
}