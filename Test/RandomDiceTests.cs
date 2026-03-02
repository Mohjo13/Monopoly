using NUnit.Framework;
using Monopoly.Core.Commons;

namespace Monopoly.Tests.Commons
{
    #region RandomDiceTests
    /// <summary>
    /// Tests for the RandomDice class.
    /// </summary>
    [TestFixture]
    public class RandomDiceTests
    {
        #region Tests
        /// <summary>
        /// Constructor should store DiceCount and Sides correctly.
        /// </summary>
        [Test]
        public void Constructor_SetsDiceCountAndSides()
        {
            var dice = new RandomDice(diceCount: 3, sides: 8);

            Assert.That(dice.DiceCount, Is.EqualTo(3));
            Assert.That(dice.Sides, Is.EqualTo(8));
        }

        /// <summary>
        /// INTENDED BEHAVIOUR TEST – will FAIL until Roll() is implemented:
        /// Default (2 dice, 6 sides) should give result between 2 and 12.
        /// </summary>
        [Test]
        public void Roll_DefaultTwoSixSidedDice_ResultBetween2And12()
        {
            var dice = new RandomDice(); // 2d6 by default

            int result = dice.Roll();

            // This will fail now because Roll() returns 0. That's OK:
            // it tells us we still need to implement the real logic.
            Assert.That(result, Is.InRange(2, 12));
        }
        #endregion
    }
    #endregion
}
