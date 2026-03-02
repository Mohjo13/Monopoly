using NUnit.Framework;
using Monopoly.Core.Players;
using Monopoly.Core.Boards;
using Monopoly.Core.Commons;
using System.Collections.Generic;

namespace Monopoly.Tests.Players
{
    #region PlayerTests
    /// <summary>
    /// Tests for the Player class: identity, money, position, basic movement guard.
    /// </summary>
    [TestFixture]
    public class PlayerTests
    {
        #region Helper Types
        /// <summary>
        /// Concrete Square for building a simple Board.
        /// </summary>
        private class TestSquare : Square
        {
            public TestSquare(int index, string name, SquareType type)
                : base(index, name, type)
            {
            }
        }

        /// <summary>
        /// Creates a tiny board so Player can move on it.
        /// </summary>
        private Board CreateSimpleBoard(int count = 10)
        {
            var squares = new List<Square>();
            for (int i = 0; i < count; i++)
            {
                squares.Add(new TestSquare(i, $"S{i}", SquareType.Property));
            }
            return new Board(squares);
        }
        #endregion

        #region Tests
        /// <summary>
        /// Constructor should set Name, Money, and Position=0.
        /// Jail flags and properties should be default.
        /// </summary>
        [Test]
        public void Constructor_SetsNameMoneyAndStartPosition()
        {
            // ARRANGE + ACT
            var player = new Player("Dog", startingMoney: 1500);

            // ASSERT
            Assert.That(player.Name, Is.EqualTo("Dog"));
            Assert.That(player.Money, Is.EqualTo(1500));
            Assert.That(player.Position, Is.EqualTo(0));   // start at GO
            Assert.That(player.InJail, Is.False);         // bool defaults to false
            Assert.That(player.JailTurns, Is.EqualTo(0)); // int defaults to 0
            Assert.That(player.GetOutOfJailCards, Is.EqualTo(0));
            Assert.That(player.OwnedProperties, Is.Not.Null);
            Assert.That(player.OwnedProperties.Count, Is.EqualTo(0));
        }

        /// <summary>
        /// If MoveByDistributed is called with 0 steps, position should not change.
        /// This matches the guard: if (steps == 0) return;
        /// </summary>
        [Test]
        public void MoveByDistributed_StepsZero_DoesNotChangePosition()
        {
            var board = CreateSimpleBoard();
            var player = new Player("Hat", 1500);

            int before = player.Position;

            player.MoveByDistributed(0, board);

            Assert.That(player.Position, Is.EqualTo(before));
        }

        /// <summary>
        /// MoveBy with a small positive number should move the player
        /// forward on the board without wrapping.
        /// Example: start 0, steps = 3 => position 3.
        /// </summary>
        [Test]
        public void MoveBy_PositiveSteps_NoWrap_UpdatesPosition()
        {
            // ARRANGE: simple 10-square board, player starts at 0
            var board = CreateSimpleBoard();
            var player = new Player("Dog", 1500);

            // ACT: move 3 steps forward
            player.MoveBy(3, board);

            // ASSERT: new position should be index 3
            Assert.That(player.Position, Is.EqualTo(3));
        }

        /// <summary>
        /// MoveBy should use Board.WrapIndex so that stepping past the last
        /// square wraps around to the beginning.
        /// Example on 10 squares:
        ///   start 0 -> move 9 => position 9
        ///   then move 3 more => raw 12 => wrapped 2
        /// </summary>
        [Test]
        public void MoveBy_PositiveSteps_WithWrap_UsesBoardWrapping()
        {
            var board = CreateSimpleBoard();
            var player = new Player("Hat", 1500);

            // get to the last square first: 0 + 9 = 9
            player.MoveBy(9, board);
            Assert.That(player.Position, Is.EqualTo(9), "Sanity check: player should be on index 9 before wrap test.");

            // now move 3 more, 9 + 3 = 12 -> wrapped to index 2
            player.MoveBy(3, board);

            Assert.That(player.Position, Is.EqualTo(2));
        }

        /// <summary>
        /// MoveBy with a negative number should move the player backwards
        /// using the same wrapping logic.
        /// Example on 10 squares:
        ///   start 0 -> move 5 => position 5
        ///   then move -2 => position 3
        /// </summary>
        [Test]
        public void MoveBy_NegativeSteps_MovesBackwardsWithWrapping()
        {
            var board = CreateSimpleBoard();
            var player = new Player("Car", 1500);

            // move forward to index 5
            player.MoveBy(5, board);
            Assert.That(player.Position, Is.EqualTo(5), "Sanity check: player should be on index 5 before moving back.");

            // now move back 2 steps: 5 + (-2) = 3
            player.MoveBy(-2, board);

            Assert.That(player.Position, Is.EqualTo(3));
        }


        #endregion
    }
    #endregion
}
