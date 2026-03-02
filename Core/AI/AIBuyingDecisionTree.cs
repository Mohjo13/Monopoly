using System;
using DecisionTree;                 // Node, Test, TreeAction
using Monopoly.Core.Boards;
using Monopoly.Core.Players;

namespace Monopoly.Core.AI
{
    #region Buying Decision Enum
    /// <summary>
    /// Result of the AI buying decision.
    /// </summary>
    public enum BuyingDecision
    {
        Buy,
        Skip
    }
    #endregion

    #region AI Buying Decision Tree
    /// <summary>
    /// Decision tree that decides if the AI should buy a property or skip.
    /// Uses simple rules:
    /// 1) If cannot afford -> Skip.
    /// 2) If buying breaks safety buffer -> Skip.
    /// 3) If property is cheap       -> Buy.
    /// 4) Else if "late game" (owns many properties) -> Buy.
    /// 5) Otherwise -> Skip.
    /// </summary>
    public class AIBuyingDecisionTree
    {
        #region Constants
        private const int SafetyBuffer = 50;
        private const int CheapPriceThreshold = 120;
        private const int LateGamePropertyCountThreshold = 5;
        #endregion

        #region Fields
        private readonly Player _player;
        private readonly PropertySquare _property;

        /// <summary>
        /// Output: what the AI decided.
        /// </summary>
        public BuyingDecision Decision { get; private set; }
        #endregion

        #region Constructor
        public AIBuyingDecisionTree(Player player, PropertySquare property)
        {
            _player = player ?? throw new ArgumentNullException(nameof(player));
            _property = property ?? throw new ArgumentNullException(nameof(property));
            Decision = BuyingDecision.Skip; // default
        }
        #endregion

        #region Public API
        /// <summary>
        /// Build and execute the decision tree once.
        /// Fills the Decision property.
        /// </summary>
        public void Run()
        {
            Node root = BuildTree();
            root.Execute();
        }
        #endregion

        #region Tree Building
        /// <summary>
        /// Root of the tree.
        /// </summary>
        private Node BuildTree()
        {
            // Leaf nodes (actions)
            TreeAction buyNode = new TreeAction(SetBuyDecision);
            TreeAction skipNode = new TreeAction(SetSkipDecision);

            // Branch when we *can* afford the property.
            Node safetyBranch = BuildSafetyBufferBranch(buyNode, skipNode);

            // Root: can we even afford it?
            Test canAffordTest = new Test(
                test: HasEnoughMoney,
                left: safetyBranch,  // true  -> go deeper
                right: skipNode      // false -> Skip
            );

            return canAffordTest;
        }

        /// <summary>
        /// Subtree after we know we can afford the property.
        /// </summary>
        private Node BuildSafetyBufferBranch(TreeAction buyNode, TreeAction skipNode)
        {
            // If we don't keep safety buffer -> Skip.
            Node cheapOrLateGameBranch = BuildCheapOrLateGameBranch(buyNode, skipNode);

            Test keepsBufferTest = new Test(
                test: KeepsSafetyBuffer,
                left: cheapOrLateGameBranch, // true  -> more checks
                right: skipNode              // false -> Skip
            );

            return keepsBufferTest;
        }

        /// <summary>
        /// Subtree that checks cheap / late game.
        /// </summary>
        private Node BuildCheapOrLateGameBranch(TreeAction buyNode, TreeAction skipNode)
        {
            // If cheap -> Buy, else -> maybe late game?
            Node lateGameBranch = BuildLateGameBranch(buyNode, skipNode);

            Test cheapTest = new Test(
                test: IsCheapProperty,
                left: buyNode,       // cheap -> Buy
                right: lateGameBranch
            );

            return cheapTest;
        }

        /// <summary>
        /// Tail branch: late game or not.
        /// </summary>
        private Node BuildLateGameBranch(TreeAction buyNode, TreeAction skipNode)
        {
            Test lateGameTest = new Test(
                test: IsLateGame,
                left: buyNode,   // late game -> Buy
                right: skipNode  // early + expensive -> Skip
            );

            return lateGameTest;
        }
        #endregion

        #region Test Functions
        private bool HasEnoughMoney()
        {
            return _player.Money >= _property.Price;
        }

        private bool KeepsSafetyBuffer()
        {
            int moneyLeft = _player.Money - _property.Price;
            return moneyLeft >= SafetyBuffer;
        }

        private bool IsCheapProperty()
        {
            return _property.Price <= CheapPriceThreshold;
        }

        private bool IsLateGame()
        {
            int totalOwned = _player.OwnedProperties.Count;
            return totalOwned >= LateGamePropertyCountThreshold;
        }
        #endregion

        #region Leaf Actions
        private void SetBuyDecision()
        {
            Decision = BuyingDecision.Buy;
        }

        private void SetSkipDecision()
        {
            Decision = BuyingDecision.Skip;
        }
        #endregion
    }
    #endregion
}
