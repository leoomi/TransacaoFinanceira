using System.Collections.Generic;
using System.Threading;
using FinancialTransaction.Models;

namespace FinancialTransaction.Data;

public static class AccountAccountBalanceDataInitializer
{
    public static void Initialize(IAccountBalanceRepository repository)
    {
        var accountBalances = new List<AccountBalance>
        {
            new AccountBalance(938485762, 180),
            new AccountBalance(347586970, 1200),
            new AccountBalance(2147483649, 0),
            new AccountBalance(675869708, 4900),
            new AccountBalance(238596054, 478),
            new AccountBalance(573659065, 787),
            new AccountBalance(210385733, 10),
            new AccountBalance(674038564, 400),
            new AccountBalance(563856300, 1200)
        };

        accountBalances.ForEach((ab) => repository.Update(ab));
    }
}