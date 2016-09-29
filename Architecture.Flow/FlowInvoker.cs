using Architecture.Core;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Architecture.Flow
{
    public class FlowInvoker
    {
        private readonly IBus bus;

        public FlowInvoker(IBus bus)
        {
            this.bus = bus;
        }

        public async Task<TState> Invoke<TDefinition, TState>(TState state)
            where TDefinition : FlowDefinition<TState>
            where TState : new()
        {
            var definition = Activator.CreateInstance<TDefinition>();
            var node = definition.GetNode();

            while(node != null)
            {
                state = await node.Invoke(state, bus);
                node = node.GetNext();
            }
            return state;
        }
    }
}
