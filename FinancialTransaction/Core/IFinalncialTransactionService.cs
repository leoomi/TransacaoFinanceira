using FinancialTransaction.Models;

public interface IFinancialTransactionService
{
    void ExecuteTransaction(Transaction transaction);
}
