using System.Collections.Generic;
using NUnit.Framework;
using Monopoly.Core.Boards;
using Monopoly.Core.Commons;

namespace Monopoly.Tests.Boards
{
    #region BoardTests
    /// <summary>
    /// Tests for the Board class: WrapIndex and GetSquare.
    /// </summary>
    [TestFixture]
    public class BoardTests
    {
        #region Helper Types
        /// <summary>
        /// Simple concrete Square only for testing.
        /// The real Square in the project is abstract, so we need this.
        /// </summary>
        private class TestSquare : Square
        {
            public TestSquare(int index, string name, SquareType type)
                : base(index, name, type)
            {
                // No extra logic, just passes data to the base Square.
            }
        }
        #endregion

        #region Helpers
        /// <summary>
        /// Builds a simple board with "count" squares.
        /// Index = 0..count-1, all as Property squares.
        /// </summary>
        private Board CreateSimpleBoard(int count = 10)
        {
            var squares = new List<Square>();

            for (int i = 0; i < count; i++)
            {
                squares.Add(new TestSquare(i, $"S{i}", SquareType.Property));
            }

            // List<Square> implements IReadOnlyList<Square>, so this is fine.
            return new Board(squares);
        }
        #endregion

        #region Tests
        /// <summary>
        /// WrapIndex should turn -1 into the last index (count-1).
        /// This checks negative index wrapping.
        /// </summary>
        [Test]
        public void WrapIndex_NegativeIndex_ReturnsLastIndex()
        {
            // ARRANGE
            var board = CreateSimpleBoard(10);

            // ACT
            int wrapped = board.WrapIndex(-1);

            // ASSERT
            Assert.That(wrapped, Is.EqualTo(9));
        }

        /// <summary>
        /// WrapIndex should wrap too-large indices back into range.
        /// Example: index 13 on 10 squares should become 3.
        /// </summary>
        [Test]
        public void WrapIndex_IndexTooLarge_WrapsAround()
        {
            var board = CreateSimpleBoard(10);

            int wrapped = board.WrapIndex(13);

            Assert.That(wrapped, Is.EqualTo(3));
        }

        /// <summary>
        /// GetSquare should internally use WrapIndex for out-of-range indices.
        /// Getting -1 should give us the last square.
        /// </summary>
        [Test]
        public void GetSquare_UsesWrappedIndex_ForOutOfRangeIndex()
        {
            var board = CreateSimpleBoard(10);

            Square square = board.GetSquare(-1);

            Assert.That(square.Index, Is.EqualTo(9));
            Assert.That(square.Name, Is.EqualTo("S9"));
        }
        #endregion
    }
    #endregion
}
