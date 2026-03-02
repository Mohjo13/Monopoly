using System;
using DecisionTree;                 // Node, Test, TreeAction
using Monopoly.Core.Players;

namespace Monopoly.Core.AI
{
    #region Jail Decision Enum
    /// <summary>
    /// Result of AI decision while in jail.
    /// </summary>
    public enum JailDecision
    {
        Stay,
        PayFine
    }
    #endregion

    #region AI Jail Decision Tree
    /// <summary>
    /// Decision tree that decides if the AI should stay in jail
    /// this turn or pay the fine to get out.
    /// Rules:
    /// 1) If reached max jail turns and can safely pay -> PayFine
    /// 2) Else if can safely pay                      -> PayFine
    /// 3) Else                                        -> Stay
    /// </summary>
    public class AIJailDecisionTree
    {
        #region Constants
        private const int MaxJailTurns = 3;
        private const int FineAmount = 50;
        private const int SafetyBuffer = 50;
        #endregion

        #region Fields
        private readonly Player _player;

        /// <summary>
        /// Output decision: Stay or PayFine.
        /// </summary>
        public JailDecision Decision { get; private set; }
        #endregion

        #region Constructor
        public AIJailDecisionTree(Player player)
        {
            _player = player ?? throw new ArgumentNullException(nameof(player));
            Decision = JailDecision.Stay; // default
        }
        #endregion

        #region Public API
        public void Run()
        {
            Node root = BuildTree();
            root.Execute();
        }
        #endregion

        #region Tree Building
        private Node BuildTree()
        {
            // Leaf nodes
            TreeAction payFineNode = new TreeAction(SetPayFineDecision);
            TreeAction stayNode = new TreeAction(SetStayDecision);

            // Branch when we reached max jail turns
            Node maxTurnsBranch = BuildMaxTurnsBranch(payFineNode, stayNode);

            // Branch when we are still under max jail turns
            Node normalBranch = BuildNormalBranch(payFineNode, stayNode);

            // Root test: reached max turns?
            Test root = new Test(
                test: ReachedMaxJailTurns,
                left: maxTurnsBranch,
                right: normalBranch
            );

            return root;
        }

        private Node BuildMaxTurnsBranch(TreeAction payFineNode, TreeAction stayNode)
        {
            Test canPayTest = new Test(
                test: CanPayFineAndKeepBuffer,
                left: payFineNode,
                right: stayNode
            );

            return canPayTest;
        }

        private Node BuildNormalBranch(TreeAction payFineNode, TreeAction stayNode)
        {
            Test canPayTest = new Test(
                test: CanPayFineAndKeepBuffer,
                left: payFineNode,
                right: stayNode
            );

            return canPayTest;
        }
        #endregion

        #region Test Functions
        private bool ReachedMaxJailTurns()
        {
            return _player.JailTurns >= MaxJailTurns;
        }

        private bool CanPayFineAndKeepBuffer()
        {
            int moneyLeft = _player.Money - FineAmount;
            return _player.Money >= FineAmount && moneyLeft >= SafetyBuffer;
        }
        #endregion

        #region Leaf Actions
        private void SetStayDecision()
        {
            Decision = JailDecision.Stay;
        }

        private void SetPayFineDecision()
        {
            Decision = JailDecision.PayFine;
        }
        #endregion
    }
    #endregion
}
