using FinancialTransaction.Models;

namespace FinancialTransaction.Data;

public interface IAccountBalanceRepository
{
    AccountBalance GetBalance(uint accountID);
    bool Update(AccountBalance accountBalance);
}