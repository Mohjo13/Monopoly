using System;
using Monopoly.Core.Boards;
using Monopoly.Core.Players;
using Monopoly.Core.Players.States;


namespace Monopoly.Core.Players.States
{
    #region Paying Tax State
    /// <summary>
    /// Player landed on a TaxSquare and must pay a fixed amount.
    /// If they cannot pay, they become bankrupt.
    /// </summary>
    public sealed class PayingTaxState : PlayerStateBase
    {
        public override string Name => "PayingTax";

        private readonly TaxSquare _taxSquare;

        public PayingTaxState(TaxSquare taxSquare)
        {
            _taxSquare = taxSquare;
        }

        public override void Execute(Player player)
        {
            int amount = _taxSquare.Amount;

            Console.WriteLine($"  {player.Name} must pay tax of {amount} on {_taxSquare.Name}.");

            if (!player.TryDebit(amount))
            {
                Console.WriteLine($"  {player.Name} cannot afford the tax and is bankrupt!");
                player.ChangeState(new BankruptState());
                return;
            }

            Console.WriteLine($"  Tax paid. {player.Name} now has {player.Money}.");
            player.ChangeState(new EndTurnState());
        }
    }
    #endregion
}
