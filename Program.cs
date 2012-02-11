using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;
using FubuCore;
using FubuCore.Binding;
using FubuMVC.Core;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Runtime;
using FubuMVC.StructureMap;
using Rhino.ServiceBus;
using Rhino.ServiceBus.Impl;
using Rhino.ServiceBus.MessageModules;
using StructureMap;

namespace FubuTransportation
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container();
            container.Configure(x => x.For<IMessageModule>().Singleton().Use<FubuTransportationMessageModule>());
            new RhinoServiceBusConfiguration()
                .UseStructureMap(container)
                .Configure();

            var inputMessage = new Input{Message = "Test", CorrelationId = Guid.NewGuid()};
            FubuApplication.For<TransportationRegistry>()
                .StructureMap(container)
                .Bootstrap();

            var bus = container.GetInstance<IStartableServiceBus>();
            bus.Start();
            bus.Send(inputMessage);

            Console.ReadKey();
        }
    }
}
