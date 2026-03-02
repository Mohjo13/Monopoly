using Monopoly.Core.AI;
using Monopoly.Core.Boards;
using Monopoly.Core.Commons;
using Monopoly.Core.Players;
using NUnit.Framework;

namespace Monopoly.Tests
{
    /// <summary>
    /// Tests for the AI buying decision tree.
    /// We check different money + price + owned-properties situations.
    /// </summary>
    public class AIBuyingDecisionTreeTests
    {
        /// <summary>
        /// Helper to create a player with some money and some owned properties.
        /// We don't care what the properties ARE, only how many there are,
        /// so we can safely add nulls to the list.
        /// </summary>
        private Player CreatePlayer(int money, int ownedPropertyCount)
        {
            var player = new Player("AI", money);

            // OwnedProperties is a List<PropertySquare>.
            // For late-game tests, we just need the Count, so null entries are fine.
            for (int i = 0; i < ownedPropertyCount; i++)
            {
                player.OwnedProperties.Add(null!);
            }

            return player;
        }

        /// <summary>
        /// Helper to create a property with a specific price.
        /// ⚠ NOTE:
        /// This assumes you have a constructor like:
        ///     PropertySquare(int index, string name, int price)
        /// If your PropertySquare constructor is different,
        /// adjust this method to match your real version.
        /// </summary>
        private PropertySquare CreateProperty(int price)
        {
            int index = 0;
            string name = "Test Property";

            // any valid color group is fine for the test
            ColorGroup group = ColorGroup.Brown;

            // dummy rent values, not used by the decision tree
            int[] rentByHouse = new[] { 10, 20, 30, 40, 50 };

            int houseCost = 50;
            int mortgageValue = 25;

            return new PropertySquare(index, name, group, price, rentByHouse, houseCost, mortgageValue);
        }

        /// <summary>
        /// If the player cannot afford the property price at all,
        /// AI should skip.
        /// </summary>
        [Test]
        public void Run_CannotAffordProperty_Skips()
        {
            var player = CreatePlayer(money: 100, ownedPropertyCount: 0);
            var property = CreateProperty(price: 300); // too expensive

            var tree = new AIBuyingDecisionTree(player, property);

            tree.Run();

            Assert.That(tree.Decision, Is.EqualTo(BuyingDecision.Skip));
        }

        /// <summary>
        /// If the player CAN afford the property, but buying it would
        /// drop money below the safety buffer (50), AI should skip.
        /// </summary>
        [Test]
        public void Run_CanAffordButBreaksSafetyBuffer_Skips()
        {
            // Money = 100, Price = 60
            // MoneyLeft = 40 < SafetyBuffer(50) => Skip.
            var player = CreatePlayer(money: 100, ownedPropertyCount: 0);
            var property = CreateProperty(price: 60);

            var tree = new AIBuyingDecisionTree(player, property);

            tree.Run();

            Assert.That(tree.Decision, Is.EqualTo(BuyingDecision.Skip));
        }

        /// <summary>
        /// If the property is cheap AND we keep the safety buffer,
        /// AI should buy.
        /// </summary>
        [Test]
        public void Run_CheapPropertyAndSafeBuffer_Buys()
        {
            // Cheap threshold = 120
            // Money = 200, Price = 100
            // Left = 100 >= 50 => safe + cheap => Buy.
            var player = CreatePlayer(money: 200, ownedPropertyCount: 0);
            var property = CreateProperty(price: 100);

            var tree = new AIBuyingDecisionTree(player, property);

            tree.Run();

            Assert.That(tree.Decision, Is.EqualTo(BuyingDecision.Buy));
        }

        /// <summary>
        /// If the property is expensive, but we are in "late game"
        /// (own many properties) and keep safety buffer, AI should buy.
        /// </summary>
        [Test]
        public void Run_ExpensivePropertyLateGameAndSafeBuffer_Buys()
        {
            // LateGamePropertyCountThreshold = 5
            // so give the player 5 owned properties (count only).
            var player = CreatePlayer(money: 400, ownedPropertyCount: 5);
            var property = CreateProperty(price: 200); // > cheap threshold 120

            var tree = new AIBuyingDecisionTree(player, property);

            tree.Run();

            Assert.That(tree.Decision, Is.EqualTo(BuyingDecision.Buy));
        }

        /// <summary>
        /// If the property is expensive, and we are NOT yet in "late game",
        /// even with safe buffer, AI should skip.
        /// </summary>
        [Test]
        public void Run_ExpensivePropertyEarlyGame_Skips()
        {
            var player = CreatePlayer(money: 400, ownedPropertyCount: 2);
            var property = CreateProperty(price: 200);

            var tree = new AIBuyingDecisionTree(player, property);

            tree.Run();

            Assert.That(tree.Decision, Is.EqualTo(BuyingDecision.Skip));
        }
    }
}
