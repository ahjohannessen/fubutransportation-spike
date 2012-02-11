using System;
using System.Linq;
using FubuCore.Binding;
using FubuCore.Util;
using FubuMVC.Core.Registration;

namespace FubuTransportation
{
    public class MessageModelBinder : IModelBinder
    {
        private readonly Cache<Type, bool> inputTypeCache; 

        public MessageModelBinder(BehaviorGraph graph)
        {
            inputTypeCache = new Cache<Type, bool>(type => graph.Routes.Any(r => r.Input.InputType == type));
        }

        public bool Matches(Type type)
        {
            return inputTypeCache[type];
        }

        public void Bind(Type type, object instance, IBindingContext context)
        {
            throw new NotImplementedException();
        }

        public object Bind(Type type, IBindingContext context)
        {
            return context.ValueAs<object>("Model");
        }
    }
}