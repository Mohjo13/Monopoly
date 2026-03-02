using System;
using Monopoly.Core.Boards;
using Monopoly.Core.Commons;
using Monopoly.Core.Players;

namespace Monopoly.Core.Players.States
{
    #region Drawing Card State
    /// <summary>
    /// Player landed on a DrawCardSquare (Chance or Community Chest).
    /// For now this only prints a message.
    /// Later this will:
    /// - Ask CardService to draw a card
    /// - Apply card effects
    /// - Possibly move player, pay/receive money, etc.
    /// </summary>
    public sealed class DrawingCardState : PlayerStateBase
    {
        public override string Name => "DrawingCard";

        private readonly DrawCardSquare _square;

        public DrawingCardState(DrawCardSquare square)
        {
            _square = square;
        }

        public override void Execute(Player player)
        {
            Console.WriteLine($"  {player.Name} would now draw a card from deck: {_square.DrawFrom}.");
            Console.WriteLine("  (Card system not implemented yet.)");

            player.ChangeState(new EndTurnState());
        }
    }
    #endregion
}
