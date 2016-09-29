using Architecture.Core;
using System;
using System.Threading.Tasks;

namespace Architecture.Flow
{
    public class FlowInvoker
    {
        private readonly IBus bus;
        private readonly IFlowStore store;

        public FlowInvoker(IBus bus, IFlowStore store)
        {
            this.bus = bus;
            this.store = store;
        }

        public async Task<TState> Invoke<TDefinition, TState>(TState state)
            where TDefinition : FlowDefinition<TState>
            where TState : new()
        {
            var definition = Activator.CreateInstance<TDefinition>();
            var node = definition.GetNode();

            while(node != null)
            {
                int index = definition.GetIndex(node);
                store.Store(definition, state, index);

                state = await node.Invoke(state, bus);
                node = node.GetNext();
            }
            return state;
        }
    }
}
