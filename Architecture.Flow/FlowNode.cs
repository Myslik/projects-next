using Architecture.Core;
using System.Threading.Tasks;

namespace Architecture.Flow
{
    public abstract class FlowNode<TState> where TState : new()
    {
        public abstract Task<TState> Invoke(TState state, IBus bus);

        public abstract FlowNode<TState> GetNext();
    }
}
