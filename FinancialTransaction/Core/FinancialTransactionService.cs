using System;
using FinancialTransaction.Data;
using FinancialTransaction.Models;

public class FinancialTransactionService: IFinancialTransactionService
{
    private IAccountBalanceRepository balanceRepository;

    public FinancialTransactionService(IAccountBalanceRepository balanceRepository)
    {
        this.balanceRepository = balanceRepository;
    }

    public void ExecuteTransaction(Transaction transaction)
    {
        var originAccountBalance = balanceRepository.GetBalance(transaction.OriginAccountID);
        var destinationAccountBalance = balanceRepository.GetBalance(transaction.DestinationAccountID);

        lock (originAccountBalance)
        {
            lock (destinationAccountBalance)
            {
                if (originAccountBalance.Balance < transaction.Amount)
                {
                    Console.WriteLine("Transacao numero {0} foi cancelada por falta de saldo", transaction.CorrelationID);
                }
                else
                {
                    originAccountBalance.Balance -= transaction.Amount;
                    destinationAccountBalance.Balance += transaction.Amount;
                    Console.WriteLine("Transacao numero {0} foi efetivada com sucesso! Novos saldos: Conta Origem:{1} | Conta Destino: {2}",
                        transaction.CorrelationID,
                        originAccountBalance.Balance,
                        destinationAccountBalance.Balance);
                }
            }
        }
    }
}
