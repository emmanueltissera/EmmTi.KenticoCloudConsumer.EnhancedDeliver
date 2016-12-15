using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace EmmTi.KenticoCloudConsumer.EnhancedDeliver.Routing
{
    public interface IRoutingEngine
    {
        void MapIgnoreRoutes(RouteCollection routes);

        void MapCmsRoutes(RouteCollection routes);

        void MapStaticRoutes(RouteCollection routes);

        void MapDefaultRoutes(RouteCollection routes);
    }
}
