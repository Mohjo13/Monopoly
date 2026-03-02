using Monopoly.Core.Commons;
using Monopoly.Core.Players;

namespace Monopoly.Core.Boards
{
    #region Property Square
    /// <summary>
    /// This class represents a buyable property square (like "Boardwalk" or "Baltic Avenue").
    /// It inherits from the base class "Square" has the shared properties:
    /// Index (position), Name, and Type.
    /// This class adds new data specific to buyable properties — price, rent, owner, etc.
    /// private???
    /// </summary>
    public class PropertySquare : Square
    {
        #region Properties
   
        public ColorGroup Group { get; }
        // The color group this property belongs to (Red, Blue, Green...).
        // owning all of a color increases rent.

 
        public int Price { get; }
        // The cost to buy this property from the bank.

        public int[] RentByHouses { get; }

        // An array of numbers showing how much rent costs depending on houses/hotel.
        // Index 0 = base rent, 1–4 = rent with houses, 5 = rent with a hotel.
        // Example: [10, 50, 150, 450, 625, 750]


        public int HouseCost { get; }
        // How much it costs to buy one house on this property.

 
        public int HotelCost { get; }
        // How much it costs to buy a hotel (usually after 4 houses).

        public Player? Owner { get; set; }
        // The player who owns this property. 



        public int Houses { get; set; }
        // How many houses are currently on the property (0 to 4).

        public bool HasHotel { get; set; }
        // True if the property has a hotel (usually replaces the 4 houses).

        #endregion


        #region Constructor
        // runs when a new PropertySquare is created.
        // We give it all the data it needs:
        //      where it is (index), its name,
        //      what color group it belongs to, how much it costs,
        //      how rent scales with buildings,
        //       and how much houses/hotels cost.
        public PropertySquare(int index,string name,ColorGroup group,int price,int[] rentByHouses,int houseCost,int hotelCost)
            : base(index, name, SquareType.Property)
        // Calls the base (parent) class "Square" to set shared properties.
        // tell the parent that this square’s type is "Property".
        {
            // Store the details passed in as ARGUMENTS in this new property object.
            Group = group;
            Price = price;
            RentByHouses = rentByHouses;
            HouseCost = houseCost;
            HotelCost = hotelCost;
        }
        #endregion

        #region Distributed behaviour (effects)
        /// <summary>
        /// Called when a player lands on a property.
        /// </summary>
        public override void OnLand(Player player, Board board)
        {
            // should: if unowned, offer to buy
            // should: if owned by another player, pay rent (based on houses/hotel)
            // should: if owned by self, do nothing
            // for now: no logic, placeholder only
        }
        #endregion


    }
    #endregion

}