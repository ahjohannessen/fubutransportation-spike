using System;

namespace FubuTransportation
{
    public interface IMessageHandledNotifier
    {
        void Handled();
    }

    public class ActionMessageHandledNotifier : IMessageHandledNotifier
    {
        private readonly Action _onMessageHandled;

        public ActionMessageHandledNotifier(Action onMessageHandled)
        {
            _onMessageHandled = onMessageHandled;
        }

        public void Handled()
        {
            _onMessageHandled();
        }
    }

    public class NulloTransportationNotifier : IMessageHandledNotifier
    {
        public void Handled()
        {
        }
    }
}