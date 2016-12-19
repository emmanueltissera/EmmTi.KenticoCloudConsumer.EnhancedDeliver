using KenticoCloud.Deliver;

namespace EmmTi.KenticoCloudConsumer.EnhancedDeliver.Models
{
    public class NavigationItemViewModel : BaseContentItemViewModel
    {
        public const string ItemCodeName = "navigation_item";

        public string Controller { get; set; }
        public string TargetUrl { get; set; }
        public string Title { get; set; }

        protected override void MapContentForType(ContentItem content, int currentDepth)
        {
            Controller = content.GetString("controller");
            TargetUrl = content.GetString("target_url");
            Title = content.System.Name;
        }
    }
}