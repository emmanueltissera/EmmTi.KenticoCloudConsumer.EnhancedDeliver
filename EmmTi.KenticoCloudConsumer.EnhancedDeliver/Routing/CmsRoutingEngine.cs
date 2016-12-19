using System.Collections.Generic;
using System.Web.Routing;

namespace EmmTi.KenticoCloudConsumer.EnhancedDeliver.Routing
{
    public class CmsRoutingEngine
    {
        protected virtual List<CmsSystemTypeRoute> GetSystemTypeRoutes()
        {
            return new List<CmsSystemTypeRoute>();
        }

        protected void MapAllRoutes(RouteCollection routes)
        {
            MapIgnoreRoutes(routes);
            MapCmsRoutes(routes);
            MapStaticRoutes(routes);
            MapDefaultRoutes(routes);
        }

        protected void MapCmsRoutes(RouteCollection routes)
        {
            routes.MapCmsRoutes(GetSystemTypeRoutes());
        }

        protected virtual void MapDefaultRoutes(RouteCollection routes)
        {
            // No implementation here
        }

        protected virtual void MapIgnoreRoutes(RouteCollection routes)
        {
            // No implementation here
        }

        protected virtual void MapStaticRoutes(RouteCollection routes)
        {
            // No implementation here
        }
    }
}