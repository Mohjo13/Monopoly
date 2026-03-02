namespace Monopoly.Core.Commons
{
    #region Enums (Core constant types)
    /// <summary>
    /// Identifies what kind of square this is on the board.
    /// Used by Board/Square and its subclasses, and referenced by Player logic.
    /// </summary>
    public enum SquareType//enums becasue the number of squares will never change and all sqaures are allways reachable
    {
        Go,
        Property,
        Railroad,
        Utility,
        Tax,
        Chance,
        CommunityChest,
        Jail,
        GoToJail,
        FreeParking
            //10 base categories of sqaures
            //specifcity to be added in subclasses

    }

    /// <summary>
    /// Monopoly color groups 
    /// Used by PropertySquare 
    /// </summary>
    public enum ColorGroup//allways the same aount of colors and utility positions
    {
        Brown, LightBlue, Pink, Orange, Red, Yellow, Green, DarkBlue,
        Railroad, Utility, None
    }

    /// <summary>
    /// Card deck types. Used by Card + DrawCardSquare + CardDeck.
    /// </summary>
    public enum CardType { Chance, CommunityChest }//which type of card it id
    #endregion
}
