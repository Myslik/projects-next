using Architecture.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Architecture.Flow
{
    internal abstract class FlowNode<TState> where TState : new()
    {
        internal IBus Bus { get; set; }

        protected Dictionary<string, FlowNode<TState>> directions = new Dictionary<string, FlowNode<TState>>();

        internal abstract Task<TState> Invoke(TState state);

        internal abstract string GetDirection();

        internal FlowNode<TState> SetDirection(string direction)
        {
            return directions[direction];
        }
    }
}
