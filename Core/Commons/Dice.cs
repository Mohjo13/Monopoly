using System;

namespace Monopoly.Core.Commons 
{
    #region Dice Abstraction
    /// <summary>
    /// Usage for any dice source. GameManager (centralized) or the turn loop (distributed)
    /// </summary>
    public interface IDice
    {
        int Roll();
        // should: return total movement steps for a turn (2..12)
    }
    #endregion

    #region RandomDice 
    /// <summary>
    /// Dice class  2 six-sided
    /// </summary>
    public sealed class RandomDice : IDice
    {
        #region Properties
    
        public int DiceCount { get; }//number of dice count, default 2

  
        public int Sides { get; }//number of sides, default 6

 
        private readonly Random _rng;//generic random number generator
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a new dice roller with a given number of dice and sides.
        /// Defaults to 2×6-sided dice like standard Monopoly.
        /// </summary>
        public RandomDice(int diceCount = 2, int sides = 6)
        {
            DiceCount = diceCount;//store how many dice will be rolled
            Sides = sides;//store all dice sides
            _rng = new Random();//make a new RNG(random number) for rolling
        }
        #endregion

        #region Roll Method
        /// <summary>
        /// Rolls all dice and returns their total value.
        /// </summary>
        public int Roll()//main dice rolling method to be used either by centralized or distributed
        {
            int total = 0;              // start total at 0

            for (int i = 0; i < DiceCount; i++)// Loop once for each die.

            {
                int singleDie = _rng.Next(1, Sides + 1);
                // Random value between 1 and Sides.
                total += singleDie;
            }

            // Return the total rolled value ( btwn 2–12 with 2×6-sided dice)
            return total;
        }
        #endregion
    }
    #endregion

    #region Distributed behaviour
    // Dice is shared between centralized and distributed architectures.
    // In distributed play, GameManager or turn loop calls dice.Roll(),
    // and the Player handles movement through MoveByDistributed().
    #endregion
}