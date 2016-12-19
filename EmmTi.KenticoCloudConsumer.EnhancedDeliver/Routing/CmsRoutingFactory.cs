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
    internal static class CmsRoutingFactory
    {
        internal static void MapCmsRoutes(this RouteCollection routes, List<CmsSystemTypeRoute> systemTypeRoutes)
        {
            MapRoutesInternal(routes, GetRoutingParameters(systemTypeRoutes));
            MapRoutesInternal(routes, GetNavTypeParameters());
        }

        private static void MapRoutesInternal(RouteCollection routes, List<CmsPageRoute> pageRoutes)
        {
            foreach (var page in pageRoutes)
            {
                routes.MapRoute(
                    name: Guid.NewGuid().ToString(),
                    url: page.Url.TrimStart('/').TrimEnd('/'),
                    defaults: new {controller = page.Controller, action = page.Action, id = page.CodeName}
                    );

                Debug.Print($"URL: {page.Url.TrimStart('/').TrimEnd('/')}");
                Debug.Print($"controller = {page.Controller}, action = {page.Action}, id = {page.CodeName}");
            }
        }

        private static List<CmsPageRoute> GetRoutingParameters(List<CmsSystemTypeRoute> systemTypeRoutes)
        {
            var parameters = new List<CmsPageRoute>();
            var typeFilter = systemTypeRoutes.Select(r => r.SystemType).ToArray();

            var filter = new List<IFilter>
            {
                new DepthFilter(0),
                new ElementsFilter(""),
                new InFilter("system.type", typeFilter)
            };

            var pages = DeliverClientFactory<BaseContentItemCollectionModel<BaseContentItemViewModel>>.GetItems(filter);

            foreach (var page in pages.OrderByDescending(p => p.Url.Length))
            {
                var typeRoute = systemTypeRoutes.FirstOrDefault(r => r.SystemType == page.System.Type);
                if (typeRoute == null)
                {
                    continue;
                }
                parameters.Add(new CmsPageRoute() { Action = typeRoute.Action, Controller = typeRoute.Controller, CodeName = page.System.Codename, Url = page.Url });
            }

            return parameters;
        }

        private static List<CmsPageRoute> GetNavTypeParameters()
        {
            var parameters = new List<CmsPageRoute>();

            var filters = new List<IFilter> {
                new EqualsFilter("system.type", "navigation_item")
            };

            var navItems = DeliverClientFactory<BaseContentItemCollectionModel<NavigationItemViewModel>>.GetItems(filters);
            
            foreach (var navItem in navItems.OrderByDescending(p => p.Url.Length))
            {
                parameters.Add(new CmsPageRoute() { Action = "Index" , Controller = navItem.Controller, CodeName = string.Empty, Url = navItem.TargetUrl });
            }

            return parameters;
        }
    }
}