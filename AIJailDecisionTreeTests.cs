using NUnit.Framework;                 // nUnit
using Monopoly.Core.AI;               // AIJailDecisionTree, JailDecision
using Monopoly.Core.Players;          // Player

namespace Monopoly.Tests
{
    /// <summary>
    /// Tests for the AIJailDecisionTree.
    /// Checks how the AI decides between staying in jail or paying the fine.
    /// </summary>
    public class AIJailDecisionTreeTests
    {
        /// <summary>
        /// Helper: make a player with given money and jail turn count.
        /// Player starts flagged as InJail.
        /// </summary>
        private Player CreatePlayer(int money, int jailTurns)
        {
            var player = new Player("AI", money)
            {
                InJail = true,
                JailTurns = jailTurns
            };
            return player;
        }

        /// <summary>
        /// If the player does NOT have enough money to safely pay,
        /// the decision should be to stay in jail.
        /// </summary>
        [Test]
        public void Run_PlayerCannotPayFine_StaysInJail()
        {
            // Fine = 50, SafetyBuffer = 50 in AIJailDecisionTree.
            // Money = 30 -> cannot pay.
            var player = CreatePlayer(money: 30, jailTurns: 0);
            var tree = new AIJailDecisionTree(player);

            tree.Run();

            Assert.That(tree.Decision, Is.EqualTo(JailDecision.Stay));
        }

        /// <summary>
        /// If the player can pay the fine AND keep the safety buffer,
        /// the AI should choose to pay the fine.
        /// </summary>
        [Test]
        public void Run_PlayerCanPayFineAndKeepBuffer_PaysFine()
        {
            // Money = 150, Fine = 50, SafetyBuffer = 50
            // After paying: 100 >= 50 => safe.
            var player = CreatePlayer(money: 150, jailTurns: 1);
            var tree = new AIJailDecisionTree(player);

            tree.Run();

            Assert.That(tree.Decision, Is.EqualTo(JailDecision.PayFine));
        }

        /// <summary>
        /// If the player is at max jail turns and can safely pay,
        /// the AI must pay the fine.
        /// </summary>
        [Test]
        public void Run_PlayerAtMaxJailTurnsAndCanPay_PaysFine()
        {
            // MaxJailTurns = 3 in AIJailDecisionTree.
            var player = CreatePlayer(money: 150, jailTurns: 3);
            var tree = new AIJailDecisionTree(player);

            tree.Run();

            Assert.That(tree.Decision, Is.EqualTo(JailDecision.PayFine));
        }

        /// <summary>
        /// If the player CAN pay the fine but would drop below the safety buffer,
        /// AI should stay in jail (save money).
        /// </summary>
        [Test]
        public void Run_PlayerCanPayButBreaksBuffer_StaysInJail()
        {
            // Money = 80, Fine = 50 -> left with 30 < 50 => not safe.
            var player = CreatePlayer(money: 80, jailTurns: 1);
            var tree = new AIJailDecisionTree(player);

            tree.Run();

            Assert.That(tree.Decision, Is.EqualTo(JailDecision.Stay));
        }
    }
}
