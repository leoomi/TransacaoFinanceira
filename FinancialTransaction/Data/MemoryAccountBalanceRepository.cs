using System;
using System.Collections.Generic;
using FinancialTransaction.Models;

namespace FinancialTransaction.Data;

public class MemoryAccountBalanceRepository: IAccountBalanceRepository
{
    private readonly Dictionary<uint, decimal> balanceDictionary;
    private readonly List<AccountBalance> accountBalances;

    public MemoryAccountBalanceRepository()
    {
        balanceDictionary = new();
        accountBalances = new();
    }

    public AccountBalance GetBalance(uint accountID)
    {
        return accountBalances.Find(ab => ab.AccountID == accountID);
    }

    public bool Update(AccountBalance accountBalance)
    {
        try
        {
            accountBalances.RemoveAll(ab => ab.AccountID == accountBalance.AccountID);
            accountBalances.Add(accountBalance);

            balanceDictionary.Remove(accountBalance.AccountID);
            balanceDictionary.Add(accountBalance.AccountID, accountBalance.Balance);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
    }
}