using System.Collections.Generic;

namespace Architecture.Flow
{
    internal class FlowRegistry<TState>
        where TState : new()
    {
        private List<FlowNode<TState>> nodeList = new List<FlowNode<TState>>();

        internal void Add(FlowNode<TState> node)
        {
            nodeList.Insert(0, node);
        }

        internal int IndexOf(FlowNode<TState> node)
        {
            return nodeList.IndexOf(node);
        }

        internal FlowNode<TState> ByIndex(int index)
        {
            return nodeList[index];
        }
    }
}
