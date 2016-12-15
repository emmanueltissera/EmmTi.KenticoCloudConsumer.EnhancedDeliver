using EmmTi.KenticoCloudConsumer.EnhancedDeliver.Helpers;
using EmmTi.KenticoCloudConsumer.EnhancedDeliver.Interfaces;
using KenticoCloud.Deliver;
using System.Collections.Generic;

namespace EmmTi.KenticoCloudConsumer.EnhancedDeliver.Models
{
    public class BaseContentItemCollectionModel<T> : List<T>, IKenticoDeliverViewModel where T : IKenticoDeliverViewModel, new()
    {
        public string ParentPath { get; set; }
        public KenticoCloud.Deliver.System System { get; set; }

        public string Url { get; set; }

        public void MapContent(ContentItem content, int currentDepth = 0)
        {
        }

        public virtual void MapContentList(List<ContentItem> contentList, int currentDepth = 0)
        {
            AddRange(contentList.GetListOfModularContent<T>());
        }
    }
}