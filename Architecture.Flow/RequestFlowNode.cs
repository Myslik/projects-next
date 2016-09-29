using Architecture.Core;
using System;
using System.Threading.Tasks;

namespace Architecture.Flow
{
    public class RequestFlowNode<TRequest, TState> : FlowNode<TState>
        where TRequest : IRequest
        where TState : new()
    {
        private Func<TState, TRequest> requestFactory;
        private FlowNode<TState> next;

        public RequestFlowNode(Func<TState, TRequest> requestFactory, FlowNode<TState> next)
        {
            this.requestFactory = requestFactory;
            this.next = next;
        }

        public override FlowNode<TState> GetNext()
        {
            return next;
        }

        public async override Task<TState> Invoke(TState state)
        {
            var request = requestFactory.Invoke(state);
            await Bus.Send(request);
            return state;
        }
    }
}
