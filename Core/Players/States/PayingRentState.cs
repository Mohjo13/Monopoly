using System;
using Monopoly.Core.Boards;
using Monopoly.Core.Players;
using Monopoly.Core.Players.States;

namespace Monopoly.Core.Players.States
{
    #region Paying Rent State
    /// <summary>
    /// Player has landed on a property that belongs to someone else.
    /// They must pay rent to the owner. If they cannot afford it, they go bankrupt.
    /// Simplified rent rule:
    /// - If RentByHouses is set and non-empty:
    ///     * Use index = Houses (0..4) or last index if hotel.
    /// - Otherwise, rent = Price / 10.
    /// </summary>
    public sealed class PayingRentState : PlayerStateBase
    {
        public override string Name => "PayingRent";

        private readonly PropertySquare _property;

        public PayingRentState(PropertySquare property)
        {
            _property = property;
        }

        public override void Execute(Player player)
        {
            Player? owner = _property.Owner;
            if (owner == null || owner == player)
            {
                Console.WriteLine("  No valid owner to pay rent to. Ending turn.");
                player.ChangeState(new EndTurnState());
                return;
            }

            int rent = CalculateRent();

            Console.WriteLine($"  {player.Name} must pay {rent} in rent to {owner.Name} for {_property.Name}.");

            if (!player.TryDebit(rent))
            {
                Console.WriteLine($"  {player.Name} cannot afford the rent and is bankrupt!");
                player.ChangeState(new BankruptState());
                return;
            }

            owner.Credit(rent);
            Console.WriteLine($"  Rent paid. {player.Name} now has {player.Money}. {owner.Name} has {owner.Money}.");

            player.ChangeState(new EndTurnState());
        }

        private int CalculateRent()
        {
            // Default fallback if data is missing.
            int fallback = _property.Price / 10;

            if (_property.RentByHouses == null || _property.RentByHouses.Length == 0)
                return fallback;

            int index;
            if (_property.HasHotel)
            {
                index = _property.RentByHouses.Length - 1;
            }
            else
            {
                index = _property.Houses;
                if (index < 0) index = 0;
                if (index >= _property.RentByHouses.Length)
                    index = _property.RentByHouses.Length - 1;
            }

            return _property.RentByHouses[index];
        }
    }
    #endregion
}
