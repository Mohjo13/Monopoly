using System;

namespace DecisionTree
{
    #region Base Node
    /// <summary>
    /// Base class for all nodes in the decision tree.
    /// Every node must implement Execute().
    /// </summary>
    public abstract class Node
    {
        /// <summary>
        /// Run this node's behavior.
        /// For a Test node, this means asking a question and
        /// going to one of its children.
        /// For an Action node, this means performing the action.
        /// </summary>
        public abstract void Execute();
    }
    #endregion

    #region Test Node
    /// <summary>
    /// A Test node asks a yes/no question and then chooses
    /// which child node to execute based on the answer.
    /// - If the test returns true  -> go to left child.
    /// - If the test returns false -> go to right child.
    /// </summary>
    public class Test : Node
    {
        private readonly Node _left;
        private readonly Node _right;
        private readonly Func<bool> _test;

        /// <summary>
        /// Create a new Test node.
        /// test  = function that returns true/false.
        /// left  = child node to execute if test() is true.
        /// right = child node to execute if test() is false.
        /// </summary>
        public Test(Func<bool> test, Node left, Node right)
        {
            _test = test;
            _left = left;
            _right = right;
        }
        /// <summary>
        /// Run this test: if test() is true, execute left child,
        /// otherwise execute right child.
        /// </summary>
        public override void Execute()
        {
            if (_test())
                _left.Execute();
            else
                _right.Execute();
        }
    }
    #endregion

    #region Action Node
    /// <summary>
    /// An Action node is a leaf in the tree.
    /// It doesn't ask questions; it just runs the given action.
    /// </summary>
    public class TreeAction : Node
    /// <summary>
    /// Leaf node: wraps a System.Action and runs it when executed.
    /// Renamed to TreeAction to avoid name clash with System.Action.
    /// </summary>
    {
        private readonly System.Action _action;

        public TreeAction(System.Action action)
        {
            _action = action;
        }

        public override void Execute()
        {
            _action();
        }
    }
    #endregion
}
