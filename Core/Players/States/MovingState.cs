using Monopoly.Core.Boards;
using Monopoly.Core.Players;
using Monopoly.Core.Players.States;
#region Moving State
/// <summary>
/// This state moves the player across the board based on LastRoll.
/// Steps:
/// - Read the last dice roll from the player
/// - Move the player that many steps on the Board
/// - Print the start and end positions
/// - Then go to EndTurnState (for this simple Option B test)
/// </summary>
public sealed class MovingState : PlayerStateBase
{
    public override string Name => "Moving";

    private readonly Board _board;

    public MovingState(Board board)
    {
        _board = board;
    }

    public override void Execute(Player player)
    {
        int steps = player.LastRoll;

        int startIndex = player.Position;

        // Move the player using our helper method in Player.
        player.MoveBy(steps, _board);

        int endIndex = player.Position;

        Console.WriteLine($"{player.Name} moved from {startIndex} to {endIndex}.");

        // For Option B, we keep it simple:
        // After moving, we just end the turn.
        // Later we will insert ResolveSquareState / BuyingPropertyState here.
        player.ChangeState(new ResolveSquareState(_board));
    }
}
#endregion