namespace Architecture.Flow
{
    public interface IFlowStore
    {
        void Store<TDefinition, TState>(TDefinition definition, TState state, int index)
            where TDefinition : FlowDefinition<TState>
            where TState : new();
    }
}
