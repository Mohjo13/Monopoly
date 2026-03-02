using Monopoly.Core.Boards;
using Monopoly.Core.Players;

namespace Monopoly.Core.Managers
{
    #region Bank Manager
    /// <summary>
    /// Central bank manager (hehehe)
    /// Responsible for handling money flow between the bank and players,
    /// as well as ownership and properties and  tracking later on.
    /// 
    /// </summary>
    public sealed class BankManager
    {
        #region Properties
  
        public int TotalFunds { get; private set; }
        //Total money reserve of the bank, finite, infinite??

        public List<PropertySquare> UnsoldProperties { get; private set; }
        // Placeholder for holding all owned properties.
        // hold a list of properties not yet purchased by players
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a new BankManager instance with optional starting funds.
        /// </summary>
        public BankManager(int startingFunds = int.MaxValue)
            // Creates a new BankManager instance .
        {
            TotalFunds = startingFunds;
            // should: initialize bank with given or infinite funds
        }
        #endregion

        #region Player Transactions
        /// <summary>
        /// Gives money from the bank to a player (e.g., GO salary, chance card).
        /// </summary>
        public void CreditPlayer(Player player, int amount)
            //Gives money from the bank to the player, GOsquare, chance card...
        {
            // should: increase player's balance by amount
            // should: decrease TotalFunds if tracking finite funds
            // example later: player.Credit(amount);
        }

        public void DebitPlayer(Player player, int amount)
        // Takes money from a player and sends it to the bank (tax payments).

        {
            // should: decrease player's balance by amount
            // should: increase TotalFunds if tracking finite funds
            // example later: player.Debit(amount);
        }
        #endregion

        #region Property Management
        public void SellPropertyToPlayer(Player player, PropertySquare property)
        // Handles selling a property to a player.

        {
            // should: check player funds
            // should: debit cost from player
            // should: assign property.Owner = player
            // should: remove property from UnsoldProperties
        }


        public void ReclaimProperty(PropertySquare property)
        // Handles collecting a property back to the bank (e.g., mortgage, bankruptcy).

        {
            // should: set property.Owner = null
            // should: add property back to UnsoldProperties
        }
        #endregion

        #region Distributed behaviour
        // This manager is shared between centralized and distributed flows.
        // In distributed mode, squares (like TaxSquare or PropertySquare)
        // can call BankManager directly to handle player payments or ownership.
        //
        // In centralized mode, GameManager calls BankManager methods instead.
        #endregion
    }
    #endregion
}
