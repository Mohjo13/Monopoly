using System;
using Monopoly.Core.Players;

namespace Monopoly.Core.Players.States
{
    #region Free Parking State
    /// <summary>
    /// Player landed on Free Parking.
    /// Default rule: nothing happens.
    /// </summary>
    public sealed class FreeParkingState : PlayerStateBase
    {
        public override string Name => "FreeParking";

        public override void Execute(Player player)
        {
            Console.WriteLine($"  {player.Name} is on Free Parking. Nothing happens.");
            player.ChangeState(new EndTurnState());
        }
    }
    #endregion
}
