namespace Architecture.Flow
{
    public abstract class FlowDefinition<TState>
        where TState : new()
    {
        private FlowRegistry<TState> registry;

        public FlowDefinition()
        {
            var builder = new FlowBuilder<TState>();
            registry = OnDefinitionCreating(builder).Complete();
        }

        internal FlowNode<TState> GetNode(int index = 0)
        {
            return registry.ByIndex(index);
        }

        internal int GetIndex(FlowNode<TState> node)
        {
            return registry.IndexOf(node);
        }

        protected abstract FlowBuilder<TState> OnDefinitionCreating(FlowBuilder<TState> builder);
    }
}
