using System;
using System.Web;
using System.Web.Routing;
using FubuMVC.Core.Runtime;

namespace FubuTransportation
{
    public class TransportationRoute : RouteBase
    {
        private readonly RouteBase _inner;
        private readonly IBehaviorInvoker _invoker;
        private readonly Type _inputType;

        public TransportationRoute(RouteBase inner, IBehaviorInvoker invoker, Type inputType)
        {
            _inner = inner;
            _invoker = invoker;
            _inputType = inputType;
        }

        public IBehaviorInvoker Invoker { get { return _invoker; } }
        public Type InputType { get { return _inputType; } }

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            return _inner.GetRouteData(httpContext);
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            return _inner.GetVirtualPath(requestContext, values);
        }
    }
}