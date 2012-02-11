using System.Collections.Generic;
using System.Web.Routing;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Routing;
using FubuMVC.Core.Runtime;

namespace FubuTransportation
{
    public class TransportationRoutePolicy : IRoutePolicy
    {
        public IList<RouteBase> BuildRoutes(BehaviorGraph graph, IBehaviorFactory factory)
        {
            var routes = new List<RouteBase>();
            graph.VisitRoutes(x =>
            {
                x.BehaviorFilters += chain => !chain.IsPartialOnly;
                x.Actions += (routeDef, chain) =>
                {
                    var route = routeDef.ToRoute();
                    var transportationRoute = new TransportationRoute(route, new BehaviorInvoker(factory, chain), chain.InputType());
                    routes.Add(transportationRoute);
                };
            });
            return routes;
        }
    }
}