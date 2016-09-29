using Architecture.Core;
using System.Threading.Tasks;

namespace Architecture.Flow
{
    public abstract class FlowNode<TState> where TState : new()
    {
        internal IBus Bus { get; set; }

        public abstract Task<TState> Invoke(TState state);

        public abstract FlowNode<TState> GetNext();
    }
}
