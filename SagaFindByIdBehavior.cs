using System;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Runtime;

namespace FubuTransportation
{
    public class SagaFindByIdBehavior<THandler, TSagaState> : IActionBehavior
    {
        private readonly dynamic _handler;
        private readonly IFubuRequest _fubuRequest;
        private readonly ISagaRepository _sagaRepository;

        public SagaFindByIdBehavior(THandler handler, IFubuRequest fubuRequest, ISagaRepository sagaRepository)
        {
            _handler = handler;
            _fubuRequest = fubuRequest;
            _sagaRepository = sagaRepository;
        }

        public IActionBehavior InsideBehavior { get; set; }

        public void Invoke()
        {
            var sagaCorrelation = _fubuRequest.Get<SagaInformation>();
            var state = _sagaRepository.Get<TSagaState>(sagaCorrelation.CorrelationId);

            //TODO don't invoke if null and not initiated by
            dynamic handler = _handler;
            handler.State = state;
            handler.Id = sagaCorrelation.CorrelationId;

            InsideBehavior.Invoke();
 
            if (_handler.IsComplete)
            {
                _sagaRepository.Delete(sagaCorrelation.CorrelationId);
            }
            else
            {
                _sagaRepository.Save(sagaCorrelation.CorrelationId, handler.State);
            }
        }

        public void InvokePartial()
        {
            throw new NotImplementedException();
        }
    }
}