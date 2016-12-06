# EmmTi.KenticoCloudConsumer.EnhancedDeliver

## SUMMARY
Enhanced Deliver Client built on top of [KenticoCloud.Deliver SDK](https://github.com/Kentico/Deliver-.NET-SDK). Strong types, Caching and SEO Friendly URLs out of the box.

## PREREQUISITIES
1. Kentico Cloud Account and an MVC Application

## Quick Setup Guide
1. Follow the steps in the [Kentico Dancing Goat Example](https://github.com/Kentico/Deliver-Dancing-Goat-.NET-MVC/blob/master/README.md) to setup a sample site.
2. Add the following config setting in appsettings of web.config. Tweak the cache time as you see fit.
```XML
 <add key="DeliveryContentCacheTimeSeconds" value="300"/> 
```
3. Create a new ViewModel and inherit from BaseContentItemViewModel
4. Add a ItemCodeName constant with the code name for the Kentico Content Type
5. Create public properties for other Content Elements
6. Override MapContentForType method and map each field - [See example]( https://github.com/emmanueltissera/EmmTi.KenticoCloudConsumer.EnhancedDeliver/blob/master/EmmTi.KenticoCloudConsumer.EnhancedDeliver/Models/SampleViewModel.cs)
```C#
using System;
using System.Collections.Generic;
using System.Linq;
using EmmTi.KenticoCloudConsumer.EnhancedDeliver.Helpers;
using KenticoCloud.Deliver;

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
```