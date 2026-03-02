using System;
using Monopoly.Core.Boards;
using Monopoly.Core.Players;

namespace Monopoly.Core.Players.States
{
    #region Landed On GO State
    /// <summary>
    /// Player landed exactly on GO.
    /// They receive the GO salary once.
    /// </summary>
    public sealed class LandedOnGoState : PlayerStateBase
    {
        public override string Name => "LandedOnGO";

        private readonly Board _board;
        private readonly GoSquare _goSquare;

        public LandedOnGoState(Board board, GoSquare goSquare)
        {
            _board = board;
            _goSquare = goSquare;
        }

        public override void Execute(Player player)
        {
            int salary = _goSquare.Salary;

            if (salary > 0)
            {
                player.Credit(salary);
                Console.WriteLine($"  {player.Name} landed on GO and received {salary}. New money: {player.Money}.");
            }
            else
            {
                Console.WriteLine($"  {player.Name} landed on GO, but salary is set to 0 for this test.");
            }

            player.ChangeState(new EndTurnState());
        }
    }
    #endregion
}
