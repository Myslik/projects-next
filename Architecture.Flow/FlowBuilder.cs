using Architecture.Core;
using System;

namespace Architecture.Flow
{
    internal delegate FlowNode<TState> BuildNode<TState>(FlowRegistry<TState> registry) where TState : new();

    public class FlowBuilder<TState> where TState : new()
    {
        private BuildNode<TState> build = r => null;
        private BuildNode<TState> complete = r => null;

        internal FlowBuilder()
        {
            complete = r => build(r);
        }

        private FlowBuilder(BuildNode<TState> complete)
        {
            this.complete = complete;
        }

        public FlowBuilder<TState> Request<TRequest>(Func<TState, TRequest> requestFactory) where TRequest : IRequest
        {
            var builder = new FlowBuilder<TState>(complete);
            build = r =>
            {
                var next = builder.build(r);
                var node = new RequestFlowNode<TRequest, TState>(requestFactory, next);
                r.Add(node);
                return node;
            };
            return builder;
        }

        internal FlowRegistry<TState> Complete()
        {
            var registry = new FlowRegistry<TState>();
            complete(registry);
            return registry;
        }
    }
}
