using System;
using Monopoly.Core.Players;

namespace Monopoly.Core.Players.States
{
    #region In Jail State
    /// <summary>
    /// Player is in jail.
    /// Simplified rules for now:
    /// - Each time they start a turn in jail, they "wait" one turn (skip).
    /// - After 3 turns in jail, they are freed and next turn will be normal.
    /// 
    /// Later this is a great place to plug in a Decision Tree:
    /// - Pay fine?
    /// - Use "Get Out of Jail Free" card?
    /// - Try to roll doubles?
    /// </summary>
    public sealed class InJailState : PlayerStateBase
    {
        public override string Name => "InJail";

        private const int MaxJailTurns = 3;

        public override void Execute(Player player)
        {
            player.JailTurns++;

            Console.WriteLine($"  {player.Name} is in jail (turn {player.JailTurns}/{MaxJailTurns}).");

            if (player.JailTurns >= MaxJailTurns)
            {
                // Free the player.
                player.InJail = false;
                player.JailTurns = 0;
                Console.WriteLine($"  {player.Name} has served their time and is now free.");

                // After being freed, this turn still ends now.
                player.ChangeState(new EndTurnState());
            }
            else
            {
                // Still serving. Turn ends.
                player.ChangeState(new EndTurnState());
            }
        }
    }
    #endregion
}
