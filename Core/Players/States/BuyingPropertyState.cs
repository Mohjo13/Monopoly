using System;
using Monopoly.Core.Boards;
using Monopoly.Core.Players;
using Monopoly.Core.Players.States;

namespace Monopoly.Core.Players.States
{
    #region Buying Property State
    /// <summary>
    /// Player has landed on an unowned property.
    /// They can choose to buy it (if they can afford it) or skip.
    /// For now:
    /// - We ask via console (y/n) for ALL players.
    /// - Later, AI can use a Decision Tree here instead of console input.
    /// </summary>
    public sealed class BuyingPropertyState : PlayerStateBase
    {
        public override string Name => "BuyingProperty";

        private readonly PropertySquare _property;

        public BuyingPropertyState(PropertySquare property)
        {
            _property = property;
        }

        public override void Execute(Player player)
        {
            // If somehow this property is already owned, just end.
            if (_property.Owner != null)
            {
                Console.WriteLine("  This property is already owned. No purchase possible.");
                player.ChangeState(new EndTurnState());
                return;
            }

            Console.WriteLine($"  {player.Name} can buy {_property.Name} for {_property.Price}.");
            Console.Write($"  Do you want to buy it? (y/n): ");

            string? input = Console.ReadLine();
            if (string.Equals(input, "y", StringComparison.OrdinalIgnoreCase))
            {
                // Try to pay.
                if (player.TryDebit(_property.Price))
                {
                    _property.Owner = player;
                    player.OwnedProperties.Add(_property);

                    Console.WriteLine($"  {player.Name} bought {_property.Name}. Remaining money: {player.Money}");
                }
                else
                {
                    Console.WriteLine($"  {player.Name} cannot afford {_property.Name}. Purchase failed.");
                }
            }
            else
            {
                Console.WriteLine($"  {player.Name} chose not to buy {_property.Name}.");
            }

            player.ChangeState(new EndTurnState());
        }
    }
    #endregion
}
