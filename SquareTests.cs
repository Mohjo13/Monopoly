using NUnit.Framework;                     // nUnit attributes and asserts
using Monopoly.Core.Boards;               // Square base class lives here
using Monopoly.Core.Commons;              // SquareType enum

namespace Monopoly.Tests.Boards
{
    /// <summary>
    /// Tests for the abstract Square base class, using a tiny test subclass.
    /// </summary>
    public class SquareTests
    {
        /// <summary>
        /// Simple concrete square so we can create instances of Square for testing.
        /// </summary>
        private sealed class TestSquare : Square
        {
            public TestSquare(int index, string name, SquareType type)
                : base(index, name, type)
            {
            }
        }

        /// <summary>
        /// Make sure the Square constructor correctly stores index, name and type.
        /// </summary>
        [Test]
        public void Constructor_SetsIndexNameAndType()
        {
            // ARRANGE: create a test square
            int index = 5;
            string name = "Test";
            SquareType type = SquareType.Property;

            var square = new TestSquare(index, name, type);

            // ASSERT: all properties should match what we passed in
            Assert.That(square.Index, Is.EqualTo(index));
            Assert.That(square.Name, Is.EqualTo(name));
            Assert.That(square.Type, Is.EqualTo(type));
        }
    }
}
