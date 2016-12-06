# EmmTi.KenticoCloudConsumer.EnhancedDeliver

## SUMMARY
Enhanced Deliver Client built on top of [KenticoCloud.Deliver SDK](https://github.com/Kentico/Deliver-.NET-SDK). Strong types, Caching and SEO Friendly URLs out of the box.

## NUGET PACKAGE
This project is available as a nuget package at https://www.nuget.org/packages/EmmTi.KenticoCloudConsumer.EnhancedDeliver/
To install EmmTi.KenticoCloudConsumer.EnhancedDeliver, run the following command in the Package Manager Console
```commandline
PM > Install-Package EmmTi.KenticoCloudConsumer.EnhancedDeliver
```

## PREREQUISITIES
1. Kentico Cloud Account and an MVC Application

## Quick Setup Guide
### Initial Setup
1. Follow the steps in the [Kentico Dancing Goat Example](https://github.com/Kentico/Deliver-Dancing-Goat-.NET-MVC/blob/master/README.md) to setup a sample site.
2. Add the following config setting in appsettings of web.config. Tweak the cache time as you see fit.
```XML
 <add key="DeliveryContentCacheTimeSeconds" value="300"/> 
```
### Creating the View Model
1. Create a new ViewModel and inherit from BaseContentItemViewModel
2. Add a ItemCodeName constant with the code name for the Kentico Content Type
3. Create public properties for other Content Elements
4. Override MapContentForType method and map each field - [See example]( https://github.com/emmanueltissera/EmmTi.KenticoCloudConsumer.EnhancedDeliver/blob/master/EmmTi.KenticoCloudConsumer.EnhancedDeliver/Models/SampleViewModel.cs)
```C#
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
```
### The Controller
In your Controller return the new ViewModel using the DeliveryFactory
```C#
	public async Task<ActionResult> Index()
        {
            var response = await DeliverClientFactory<SampleViewModel>.GetItemAsync(SampleViewModel.ItemCodeName);
            return View(response);
        }
```
### The View
Use the strongly typed model in your ViewModel
```html
@model EmmTi.KenticoCloudConsumer.EnhancedDeliver.Models.SampleViewModel

<div class="article-tile article-tile-large">
    <div class="col-md-12 col-lg-6">
        <a href="@Url.Action("Show", "Articles", new { id = Model.System.Codename })">
            <img src="@Model.TeaserImage.Url" class="article-tile-image" alt="@Model.System.Name" />
        </a>
    </div>
    <div class="col-md-12 col-lg-6">
        <div class="article-tile-date">
            @Model.PostDate.ToString("m")
        </div>
        <div class="article-tile-content">
            <h2>
                <a href="@Url.Action("Show", "Articles", new { id = Model.System.Codename })">@Model.System.Name</a>
            </h2>
            <p class="article-tile-text lead-paragraph">
                @Model.Summary
            </p>
        </div>
    </div>
</div>
```

## Full Sample site
See an [enhanced version of the Dancing Goat Sample site](https://github.com/emmanueltissera/Deliver-Dancing-Goat-Enhanced) for a working example
