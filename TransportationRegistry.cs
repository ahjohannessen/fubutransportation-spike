using FubuMVC.Core;
using FubuMVC.Core.Routing;

namespace FubuTransportation
{
    public class TransportationRegistry : FubuRegistry
    {
        public TransportationRegistry()
        {
            Applies.ToThisAssembly();
            Actions.IncludeTypesNamed(x => x.EndsWith("Consumer"));

            Services(x => x.SetServiceIfNone<IMessageHandledNotifier, NulloTransportationNotifier>());
            Services(x => x.SetServiceIfNone<IRoutePolicy, TransportationRoutePolicy>());
            Services(x => x.SetServiceIfNone<ISagaRepository, StubRepository>());

            Policies.Add<SagaFindByIdConvention>();
            Policies.Add<NotifyOutputConvention>();

            Models.BindModelsWith<MessageModelBinder>();
        }
    }
}