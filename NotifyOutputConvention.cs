using System.Collections.Generic;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;

namespace FubuTransportation
{
    public class NotifyOutputConvention : IConfigurationAction
    {
        public void Configure(BehaviorGraph graph)
        {
            graph.Actions()
                .Each(x => x.AddToEnd(new Wrapper(typeof(NotifyOutputBehavior))));
        }
    }
}