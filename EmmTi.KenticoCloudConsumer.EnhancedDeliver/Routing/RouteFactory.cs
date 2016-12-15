using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using EmmTi.KenticoCloudConsumer.EnhancedDeliver.Factories;
using EmmTi.KenticoCloudConsumer.EnhancedDeliver.Models;
using KenticoCloud.Deliver;

namespace EmmTi.KenticoCloudConsumer.EnhancedDeliver.Routing
{
    public static class RouteFactory
    {
        private static readonly List<CmsSystemTypeRoute> SystemTypeRoutes = GetRouteConfigValues();

        public static void MapCmsRoutes(this RouteCollection routes)
        {
            foreach (var page in GetRoutingParameters())
            {
                routes.MapRoute(
                    name: Guid.NewGuid().ToString(),
                    url: page.Url.TrimStart('/').TrimEnd('/'),
                    defaults: new { controller = page.Controller, action = page.Action, id = page.CodeName }
                );

                Debug.Print($"URL: {page.Url.TrimStart('/').TrimEnd('/')}");
                Debug.Print($"controller = {page.Controller}, action = {page.Action}, id = {page.CodeName}");
            }
        }

        public static void RefreshRoutes()
        {
            var routes = RouteTable.Routes;
            using (routes.GetWriteLock())
            {
                routes.Clear();
            }
        }

        private static List<CmsSystemTypeRoute> GetRouteConfigValues()
        {
            var routes = new List<CmsSystemTypeRoute>();
            routes.Add(new CmsSystemTypeRoute() { Action = "Show", Controller = "Articles", SystemType = "article" });
            routes.Add(new CmsSystemTypeRoute() { Action = "Detail", Controller = "Product", SystemType = "brewer" });
            routes.Add(new CmsSystemTypeRoute() { Action = "Detail", Controller = "Product", SystemType = "coffee" });
            return routes;
        }

        private static IEnumerable<CmsPageRoute> GetRoutingParameters()
        {
            var parameters = new List<CmsPageRoute>();
            var typeFilter = SystemTypeRoutes.Select(r => r.SystemType).ToArray();

            var filter = new List<IFilter>
            {
                new DepthFilter(0),
                new ElementsFilter(""),
                new InFilter("system.type", typeFilter)
            };

            var pages = DeliverClientFactory<BaseContentItemCollectionModel<BaseContentItemViewModel>>.GetItems(filter);

            foreach (var page in pages.OrderByDescending(p => p.Url.Length))
            {
                var typeRoute = SystemTypeRoutes.FirstOrDefault(r => r.SystemType == page.System.Type);
                if (typeRoute == null)
                {
                    continue;
                }
                parameters.Add(new CmsPageRoute() { Action = typeRoute.Action, Controller = typeRoute.Controller, CodeName = page.System.Codename, Url = page.Url });
            }

            return parameters;
        }
    }
}