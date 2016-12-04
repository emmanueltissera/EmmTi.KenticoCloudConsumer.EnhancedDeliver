using KenticoCloud.Deliver;
using System.Collections.Generic;

namespace EmmTi.KenticoCloudConsumer.EnhancedDeliver.Interfaces
{
    /// <summary>
    /// Interface for all View Models
    /// </summary>
    public interface IKenticoDeliverViewModel
    {
        /// <summary>
        /// Gets or sets the system.
        /// </summary>
        /// <value>
        /// The system.
        /// </value>
        KenticoCloud.Deliver.System System { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        string Url { get; set; }

        /// <summary>
        /// Maps the content.
        /// </summary>
        /// <param name="content">The content.</param>
        void MapContent(ContentItem content);

        /// <summary>
        /// Maps the content list.
        /// </summary>
        /// <param name="contentList">The content list.</param>
        void MapContentList(List<ContentItem> contentList);
    }
}