namespace Architecture.Flow
{
    public class FlowDefinition<TState> where TState : new()
    {
        private FlowNode<TState> startNode;

        public FlowBuilder<TState> CreateBuilder()
        {
            var flowBuilder = new FlowBuilder<TState>();
            flowBuilder.FlowNodeBuilt += (sender, e) =>
            {
                startNode = e.FlowNode;
            };
            return flowBuilder;
        }
    }
}
