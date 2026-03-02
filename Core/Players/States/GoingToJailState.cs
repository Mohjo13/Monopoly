using System;
using Monopoly.Core.Boards;
using Monopoly.Core.Players;

namespace Monopoly.Core.Players.States
{
    #region Going To Jail State
    /// <summary>
    /// Player landed on GoToJailSquare or received a card that sends them to jail.
    /// This state moves the player to the Jail square index and marks them as InJail.
    /// </summary>
    public sealed class GoingToJailState : PlayerStateBase
    {
        public override string Name => "GoingToJail";

        private readonly Board _board;
        private readonly GoToJailSquare _goToJailSquare;

        public GoingToJailState(Board board, GoToJailSquare square)
        {
            _board = board;
            _goToJailSquare = square;
        }

        public override void Execute(Player player)
        {
            int jailIndex = _goToJailSquare.JailIndex;
            int oldIndex = player.Position;

            // Move directly to jail position.
            // We use WrapIndex in case data is odd.
            player.Position = _board.WrapIndex(jailIndex);

            player.InJail = true;
            player.JailTurns = 0;

            Console.WriteLine($"  {player.Name} is sent to Jail (from {oldIndex} to {player.Position}).");

            player.ChangeState(new EndTurnState());
        }
    }
    #endregion
}
