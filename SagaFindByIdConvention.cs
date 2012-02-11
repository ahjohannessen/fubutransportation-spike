using System.Collections.Generic;
using System.Linq;
using FubuMVC.Core.Registration;

namespace FubuTransportation
{
    public class SagaFindByIdConvention : IConfigurationAction
    {
        public void Configure(BehaviorGraph graph)
        {
            graph.Actions()
                .Where(x => x.Method.Name.StartsWith("Orchestrates") || x.Method.Name.StartsWith("Initiates"))
                .Where(x => x.InputType().GetProperties().Any(y => y.Name.Equals("CorrelationId")))
                .Each(x => x.WrapWith(typeof(SagaFindByIdBehavior<,>).MakeGenericType(x.HandlerType, x.HandlerType.GetProperty("State").PropertyType)));
        }
    }
}