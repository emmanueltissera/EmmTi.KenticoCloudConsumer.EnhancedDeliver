using EmmTi.KenticoCloudConsumer.EnhancedDeliver.Helpers;
using KenticoCloud.Deliver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmmTi.KenticoCloudConsumer.EnhancedDeliver.Models
{
    public class SampleViewModel : BaseContentItemViewModel
    {
        public const string ItemCodeName = "article";
        public HtmlString BodyCopy { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeywords { get; set; }
        public Dictionary<string, string> Personas { get; set; }
        public DateTime PostDate { get; set; }
        public List<SampleViewModel> RelatedArticles { get; set; }
        public string Summary { get; set; }
        public Asset TeaserImage { get; set; }
        public string Title { get; set; }

        protected override void MapContentForType(ContentItem content, int currentDepth)
        {
            BodyCopy = new HtmlString(content.GetString("body_copy"));
            MetaDescription = content.GetString("meta_description");
            MetaKeywords = content.GetString("meta_keywords");
            Personas = content.GetTaxonomyItems("personas");
            PostDate = content.GetDateTime("post_date");
            RelatedArticles = content.GetModularContent("related_articles").GetListOfModularContent<SampleViewModel>(currentDepth + 1);
            Summary = content.GetString("summary");
            TeaserImage = content.GetAssets("teaser_image").FirstOrDefault();
            Title = content.GetString("title");
        }
    }
}