using System;
using System.Collections;
using System.Collections.Generic;

namespace ReactorUI
{
    public class VisualNodeCollection : IReadOnlyList<VisualNode>
    {
        private readonly VisualNode _owner;
        private readonly Action<VisualNode, int> _childAdded;
        private readonly Action<VisualNode, int> _childRemoved;
        private readonly List<VisualNode> _internalList = new List<VisualNode>();

        internal VisualNodeCollection(VisualNode owner, Action<VisualNode, int> childAdded, Action<VisualNode, int> childRemoved)
        {
            _owner = owner ?? throw new ArgumentNullException(nameof(owner));
            _childAdded = childAdded ?? throw new ArgumentNullException(nameof(childAdded));
            _childRemoved = childRemoved ?? throw new ArgumentNullException(nameof(childRemoved));
        }

        public VisualNode this[int index] => _internalList[index];

        public int Count => _internalList.Count;

        public IEnumerator<VisualNode> GetEnumerator()
        {
            return _internalList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void Add(VisualNode visualNode)
        {
            if (visualNode == null)
                throw new ArgumentNullException(nameof(visualNode));
            if (visualNode.Parent != null)
                throw new InvalidOperationException();

            _internalList.Add(visualNode);
            _childAdded(visualNode, _internalList.Count - 1);
        }

        public void Remove(VisualNode visualNode)
        {
            if (visualNode == null)
                throw new ArgumentNullException(nameof(visualNode));
            if (visualNode.Parent != _owner)
                throw new InvalidOperationException();

            var indexOfVisualNode = _internalList.IndexOf(visualNode);
            if (indexOfVisualNode >= 0)
            {
                _internalList.RemoveAt(indexOfVisualNode);
                _childRemoved(visualNode, indexOfVisualNode);
            }
        }
    }
}