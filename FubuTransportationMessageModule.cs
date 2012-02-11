using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;
using FubuCore;
using Rhino.ServiceBus;
using Rhino.ServiceBus.Impl;
using Rhino.ServiceBus.Internal;
using Rhino.ServiceBus.MessageModules;

namespace FubuTransportation
{
    public class FubuTransportationMessageModule : IMessageModule
    {
        private RouteCollection _routes;

        public FubuTransportationMessageModule()
        {
            _routes = RouteTable.Routes;
        }

        public void Init(ITransport transport, IServiceBus bus)
        {
            transport.MessageArrived += TransportMessageArrived;
        }

        bool TransportMessageArrived(CurrentMessageInformation arg)
        {
            var msg = arg.Message;
            bool handled = false;
            _routes.OfType<TransportationRoute>().Where(x => msg.GetType().CanBeCastTo(x.InputType))
                .Each(x =>
                {
                    var arguments = new TransportationServiceArguments(msg);
                    arguments.With<IMessageHandledNotifier>(new ActionMessageHandledNotifier(() => handled = true));
                    x.Invoker.Invoke(arguments, new Dictionary<string, object>());
                });
            return handled;
        }

        public void Stop(ITransport transport, IServiceBus bus)
        {
            transport.MessageArrived -= TransportMessageArrived;
        }
    }
}