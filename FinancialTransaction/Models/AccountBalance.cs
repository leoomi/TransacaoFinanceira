using System.Threading;

namespace FinancialTransaction.Models;

public class AccountBalance
{
    public AccountBalance(uint accountID, decimal balance)
    {
        AccountID = accountID;
        Balance = balance;
    }
    public uint AccountID { get; set; }
    public decimal Balance { get; set; }
}