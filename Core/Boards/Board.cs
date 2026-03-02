using System.Collections.Generic;

namespace Monopoly.Core.Boards
{
    #region Board (container of squares)
    /// <summary>
    /// main board where all the squares and the players will exist
    /// square positions and their properites to read from square class and place at index
    /// </summary>
    public class Board
    {
        #region SquareProperties
        //only get the properites fromt he square class no actions or change to the actual squares
        public IReadOnlyList<Square> Squares { get; }
        
        #endregion

        #region Constructor
        //constructor for building the list of sqaures, future allSquares fo all the squares
        public Board(IReadOnlyList<Square> squares)
        {
            Squares = squares;
        }
        #endregion
        #region Index Wrapping & Access Methods
        /// <summary>
        /// Keeps an index within the board’s range (supports negative and large values).
        /// </summary>
        public int WrapIndex(int index)
        {
            int n = Squares.Count;
            return ((index % n) + n) % n;//wrapping so that there is no -1 nor 42 square
        }

        /// <summary>
        /// Returns the Square at the (wrapped) index.
        /// </summary>
        public Square GetSquare(int index)
        {
            return Squares[WrapIndex(index)];//if outside of range return to the index
        }
        #endregion

        #region Future behaviour
        // (Optional later): helper utilities??
        #endregion

    }
    #endregion
}
