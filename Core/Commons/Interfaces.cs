namespace Monopoly.Core.Commons
{
    #region Interfaces (low coupling edges)
    /// <summary>
    /// Contract for all board squares. Keeps Player decoupled from square classes.
    /// Implemented by abstract Square (Board/Square) and all specific squares.
    /// 
    /// We use an "interface" to define a  list of things a class MUST have.
    ///It doesn't contain logic, only the rule "any class that uses this must include these properties or methods."
    ///This helps keep the code flexible and loosely connected.
    /// </summary>


    public interface ISquare
    {
        // Every square on the board must have an Index number (its position).
        int Index { get; }

        // Every square must have a Name (like "GO", "Chance", "Jail", etc.).
        string Name { get; }

        // Every square must have a Type (from the SquareType enum: Go, Property, Tax, etc.).
        SquareType Type { get; }

        // (Later) add a shared action like OnLand(Player);
        // describes the data all squares share.
    }

    /// <summary>
    ///  Implemented by Card base and specific card types.
    /// </summary>
    public interface ICard
    {
        // Every card must have a Title (short name or label, like "Bank Error in Your Favor").
        string Title { get; }

        // Every card must have a Description (the text that explains what happens).
        string Description { get; }

        // Every card must know what kind it is (Chance or Community Chest).
        CardType CardType { get; }

        // (Later) Cards will have a method like Apply(Player),
    }
    #endregion
}
