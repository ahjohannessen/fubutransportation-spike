using FubuCore.Binding;
using FubuCore.Util;

namespace FubuTransportation
{
    public class TransportationServiceArguments : ServiceArguments
    {
        public TransportationServiceArguments(object message) //possibly a message envelope for transport format
        {
            With<AggregateDictionary>(new TransportationAggregateDictionary(message));
        }
    }

    public class TransportationAggregateDictionary : AggregateDictionary
    {
        private readonly Cache<string, object> propertyCache; 
        public TransportationAggregateDictionary(object message)
        {
            propertyCache = new Cache<string, object>(name =>
            {
                var property = message.GetType().GetProperty(name);
                if (property == null) return null;
                return property.GetValue(message, null);
            });
            AddLocator("Message Properties", source => propertyCache[source], () => propertyCache.GetAllKeys());
            AddLocator("Message", source => message, () => new []{"Message"});
        }
    }
}