using System;
using Monopoly.Core.Players;

namespace Monopoly.Core.Players.States
{
    #region Bankrupt State
    /// <summary>
    /// Player cannot pay what they owe.
    /// They are removed from active play.
    /// Current implementation:
    /// - Mark player as bankrupt
    /// - Print message
    /// - Return them to Idle state (they will be skipped by GameManager)
    /// </summary>
    public sealed class BankruptState : PlayerStateBase
    {
        public override string Name => "Bankrupt";

        public override void Execute(Player player)
        {
            if (!player.IsBankrupt)
            {
                player.MarkBankrupt();
                Console.WriteLine($"  {player.Name} is now BANKRUPT and out of the game.");
            }

            // After bankruptcy, we can still go to Idle
            // so the FSM doesn't crash when UpdateState is called.
            player.ChangeState(new IdleState());
        }
    }
    #endregion
}
