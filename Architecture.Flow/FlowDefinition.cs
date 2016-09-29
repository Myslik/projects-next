namespace Architecture.Flow
{
    public abstract class FlowDefinition<TState>
        where TState : new()
    {
        private FlowNode<TState> start;

        public FlowDefinition()
        {
            var builder = new FlowBuilder<TState>();
            start = OnDefinitionCreating(builder).Complete();
        }

        public FlowNode<TState> GetNode()
        {
            return start;
        }

        protected abstract FlowBuilder<TState> OnDefinitionCreating(FlowBuilder<TState> builder);
    }
}
