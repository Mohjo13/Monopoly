using Monopoly.Core.Commons;
using Monopoly.Core.Players;

namespace Monopoly.Core.Boards
{
    #region Base Square (implements ISquare)
    /// <summary>
    /// Abstract base for all board squares (Go, Property, Tax, etc.).
    /// Connections:
    /// - Implements ISquare (Commons).
    /// - Subclasses: GoSquare, PropertySquare, RailroadSquare, UtilitySquare, TaxSquare, DrawCardSquare,
    ///               JailSquare, GoToJailSquare, FreeParkingSquare.
    /// - Referenced by: Board (collection of squares), Player (current position/square).
    /// subclasses??? do i need an interface?
    /// </summary>
    public abstract class Square 
    {
        #region Properties (state)
        public int Index { get; protected set; }
        //get postion of specifick sqaure and set it to a protected index that can not be manipulated from teh putside
        public string Name { get; protected set; }
        //get name of specifick sqaure and set it to a protected index that can not be manipulated from teh putside

        public SquareType Type { get; protected set; }
        //get and set protected type
        #endregion

        #region Constructor
        protected Square(int index, string name, SquareType type)//base square datat for all other squares to share and 
        {
            Index = index;//position of square on board enum
            Name = name;//name of square 
            Type = type;//type go, jail, ...
        }
        #endregion

        #region Distributed behaviour
        /// <summary>
        /// if using distributed behavior
        /// manily mobemnt based focus
        /// </summary>
        public virtual void OnPass(Player player, Board board)
        {
            // Example override later: GoSquare may award salary on pass.
        }

        public virtual void OnLand(Player player, Board board)
        {
            // Example for later: TaxSquare charges; GoToJailSquare moves to jail.
        }
        #endregion
    }
    #endregion
}
