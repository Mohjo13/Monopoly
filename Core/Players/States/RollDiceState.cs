using System;
using Monopoly.Core.Players;
using Monopoly.Core.Players.States;
using Monopoly.Core.Commons;          // IDice
using Monopoly.Core.Boards;           // Board

#region Roll Dice State
public sealed class RollDiceState : PlayerStateBase
{
    public override string Name => "RollDice";

    private readonly Board _board;
    private readonly IDice _dice;

    public RollDiceState(Board board, IDice dice)
    {
        _board = board;
        _dice = dice;
    }

    public override void Execute(Player player)
    {
        int roll = _dice.Roll();

        player.SetLastRoll(roll);

        Console.WriteLine($"  {player.Name} rolled a {roll}.");

        player.ChangeState(new MovingState(_board));
    }
}
#endregion