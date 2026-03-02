using System;
using Monopoly.Core.Boards;
using Monopoly.Core.Commons;
using Monopoly.Core.Players;
using Monopoly.Core.Players.States;

#region Resolve Square State
/// <summary>
/// Player has finished moving. This state looks at the square under the player
/// and decides which "effect" state to go to next.
/// For example:
/// - Property         -> BuyingProperty or PayingRent
/// - Tax              -> PayingTax
/// - Chance/Chest     -> DrawingCard
/// - Go To Jail       -> GoingToJail
/// - GO               -> LandedOnGo
/// - Free Parking     -> FreeParking
/// - Jail (landed)    -> Just visiting -> EndTurn
/// </summary>
public sealed class ResolveSquareState : PlayerStateBase
{
    public override string Name => "ResolveSquare";

    private readonly Board _board;

    public ResolveSquareState(Board board)
    {
        _board = board;
    }

    public override void Execute(Player player)
    {
        Square square = _board.GetSquare(player.Position);

        Console.WriteLine($"  {player.Name} landed on square [{square.Index}] {square.Name} ({square.Type}).");

        switch (square.Type)
        {
            case SquareType.Go:
                player.ChangeState(new LandedOnGoState(_board, (GoSquare)square));
                break;

            case SquareType.Property:
                HandleProperty(player, (PropertySquare)square);
                break;

            case SquareType.Tax:
                player.ChangeState(new PayingTaxState((TaxSquare)square));
                break;

            case SquareType.Chance:
            case SquareType.CommunityChest:
                player.ChangeState(new DrawingCardState((DrawCardSquare)square));
                break;

            case SquareType.GoToJail:
                player.ChangeState(new GoingToJailState(_board, (GoToJailSquare)square));
                break;

            case SquareType.FreeParking:
                player.ChangeState(new FreeParkingState());
                break;

            case SquareType.Jail:
                // Landing on Jail square is "Just Visiting" by default.
                Console.WriteLine($"  {player.Name} is just visiting Jail.");
                player.ChangeState(new EndTurnState());
                break;

            case SquareType.Railroad:
                Console.WriteLine("  Railroad logic not fully implemented yet. Ending turn.");
                player.ChangeState(new EndTurnState());
                break;

            case SquareType.Utility:
                Console.WriteLine("  Utility logic not fully implemented yet. Ending turn.");
                player.ChangeState(new EndTurnState());
                break;

            default:
                player.ChangeState(new EndTurnState());
                break;
        }
    }

    private void HandleProperty(Player player, PropertySquare property)
    {
        if (property.Owner == null)
        {
            // Unowned property: offer to buy.
            player.ChangeState(new BuyingPropertyState(property));
        }
        else if (property.Owner == player)
        {
            // Owned by self: nothing happens.
            Console.WriteLine($"  {player.Name} owns this property. No rent to pay.");
            player.ChangeState(new EndTurnState());
        }
        else
        {
            // Owned by someone else: pay rent.
            player.ChangeState(new PayingRentState(property));
        }
    }
}

#endregion