using Architecture.Core;
using System;

namespace Architecture.Flow
{
    public class FlowBuilder<TState> where TState : new()
    {
        internal event FlowNodeBuiltEventHandler<TState> FlowNodeBuilt;

        internal virtual void OnFlowNodeBuilt(FlowNode<TState> node)
        {
            FlowNodeBuilt?.Invoke(this, new FlowNodeEventArgs<TState>(node));
        }

        public FlowBuilder<TState> Request<TRequest>(Func<TState, TRequest> requestFactory) where TRequest : IRequest
        {
            var requestNode = new RequestFlowNode<TRequest, TState>(requestFactory);
            var flowBuilder = new FlowBuilder<TState>();
            flowBuilder.FlowNodeBuilt += (sender, e) =>
            {
                requestNode.Next = e.FlowNode;
            };
            return flowBuilder;
        }
    }

    internal delegate void FlowNodeBuiltEventHandler<TState>(object sender, FlowNodeEventArgs<TState> e) where TState : new();

    internal class FlowNodeEventArgs<TState> : EventArgs
        where TState : new()
    {
        internal FlowNodeEventArgs(FlowNode<TState> node)
        {
            FlowNode = node;
        }

        internal FlowNode<TState> FlowNode { get; }
    }
}
