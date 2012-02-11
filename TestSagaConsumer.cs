using System;

namespace FubuTransportation
{
    public class TestSagaConsumer
    {
        public Guid Id { get; set; }
        public HelloState State { get; set; }
        public bool IsComplete { get; set; }

        public Output Initiates(Input input)
        {
            return new Output();
        }

        public Output Orchestrates(IMessage message)
        {
            return new Output();
        }
    }

    public class HelloState
    {
        
    }

    public class Input : IMessage
    {
        public string Message { get; set; }
        public Guid CorrelationId { get; set; }
    }

    public interface IMessage
    {
        Guid CorrelationId { get; set; }
    }

    public class Output
    {
    }
}