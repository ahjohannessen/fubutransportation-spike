using System;

namespace FubuTransportation
{
    public interface ISagaRepository
    {
        TSagaState Get<TSagaState>(Guid id);
        void Delete(Guid id);
        void Save<TSagaState>(Guid id, TSagaState state);
    }

    public class StubRepository : ISagaRepository
    {
        public TSagaState Get<TSagaState>(Guid id)
        {
            return Activator.CreateInstance<TSagaState>();
        }

        public void Delete(Guid id)
        {
        }

        public void Save<TSagaState>(Guid id, TSagaState state)
        {
        }
    }
}