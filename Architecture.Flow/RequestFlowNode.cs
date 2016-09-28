using Architecture.Core;
using System;
using System.Threading.Tasks;

namespace Architecture.Flow
{
    internal class RequestFlowNode<TRequest, TState> : FlowNode<TState>
        where TRequest : IRequest
        where TState : new()
    {
        private Func<TState, TRequest> requestFactory;

        public RequestFlowNode(Func<TState, TRequest> requestFactory)
        {
            this.requestFactory = requestFactory;
        }

        internal FlowNode<TState> Next { get; set; }

        internal override string GetDirection()
        {
            throw new NotImplementedException();
        }

        internal async override Task<TState> Invoke(TState state)
        {
            var request = requestFactory.Invoke(state);
            await Bus.Send(request);
            return state;
        }
    }
}
