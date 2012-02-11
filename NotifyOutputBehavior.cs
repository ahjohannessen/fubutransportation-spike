using System;
using FubuMVC.Core.Behaviors;

namespace FubuTransportation
{
    public class NotifyOutputBehavior : IActionBehavior
    {
        private readonly IMessageHandledNotifier _notifier;

        public NotifyOutputBehavior(IMessageHandledNotifier notifier)
        {
            _notifier = notifier;
        }

        public void Invoke()
        {
            _notifier.Handled();
        }

        public void InvokePartial()
        {
            throw new NotSupportedException();
        }
    }
}