using Architecture.Core;
using System;

namespace Architecture.Flow
{
    public class FlowBuilder<TState> where TState : new()
    {
        private Func<FlowNode<TState>> build = () => null;
        private Func<FlowNode<TState>> complete = () => null;

        internal FlowBuilder()
        {
            complete = () => build();
        }

        private FlowBuilder(Func<FlowNode<TState>> complete)
        {
            this.complete = complete;
        }

        public FlowBuilder<TState> Request<TRequest>(Func<TState, TRequest> requestFactory) where TRequest : IRequest
        {
            var builder = new FlowBuilder<TState>(complete);
            build = () => new RequestFlowNode<TRequest, TState>(requestFactory, builder.build());
            return builder;
        }

        internal FlowNode<TState> Complete()
        {
            return complete();
        }
    }
}
