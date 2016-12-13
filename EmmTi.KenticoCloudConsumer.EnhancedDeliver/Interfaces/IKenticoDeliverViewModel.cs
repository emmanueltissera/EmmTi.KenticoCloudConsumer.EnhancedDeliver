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
        /// Gets or sets the parent path.
        /// </summary>
        /// <value>
        /// The parent path.
        /// </value>
        string ParentPath { get; set; }

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
        /// <param name="currentDepth">The current depth of this item in a recursive tree</param>
        void MapContent(ContentItem content, int currentDepth = 0);

        /// <summary>
        /// Maps the content list.
        /// </summary>
        /// <param name="contentList">The content list.</param>
        /// <param name="currentDepth">The current depth of this item in a recursive tree</param>
        void MapContentList(List<ContentItem> contentList, int currentDepth = 0);
    }
}