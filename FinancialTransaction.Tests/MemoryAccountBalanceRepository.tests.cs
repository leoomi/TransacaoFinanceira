using System;
using FinancialTransaction.Data;
using FinancialTransaction.Models;
using Microsoft.VisualBasic;
using Xunit;

namespace FinancialTransaction.Tests;

public class MemoryAccountBalanceRepositoryTests: IDisposable
{
    private IAccountBalanceRepository repository;

    public MemoryAccountBalanceRepositoryTests()
    {
        repository = new MemoryAccountBalanceRepository();
    }

    public void Dispose()
    {
        repository = null;
    }

    [Fact]
    public void EmptyRepository_GetBalanceShouldAlwaysReturnNull()
    {
        Assert.Null(repository.GetBalance(0));
        Assert.Null(repository.GetBalance(1));
        Assert.Null(repository.GetBalance(2));
    }

    [Fact]
    public void IDNotInRepository_GetBalanceShouldReturnNull()
    {
        var balance = new AccountBalance(0, 0);
        repository.Update(balance);

        Assert.Null(repository.GetBalance(1));
    }

    [Fact]
    public void EmptyRepository_UpdateShouldReturnTrue()
    {
        Assert.True(repository.Update(new AccountBalance(1, 0)));
    }

    [Fact]
    public void InvalidParameter_UpdateShouldReturnFalse()
    {
        Assert.False(repository.Update(null));
    }

    [Fact]
    public void ExistingBalance_GetBalanceShouldReturnCorrectBalance()
    {
        var balance = new AccountBalance(0, 0);
        repository.Update(balance);

        Assert.Equal(balance, repository.GetBalance(balance.AccountID));
    }

    [Fact]
    public void MultipleBalances_GetBalanceShouldReturnCorrectBalance()
    {
        var balance1 = new AccountBalance(0, 0);
        repository.Update(balance1);
        var balance2 = new AccountBalance(1, 1);
        repository.Update(balance2);
        var balance3 = new AccountBalance(2, 2);
        repository.Update(balance3);

        Assert.Equal(balance1, repository.GetBalance(balance1.AccountID));
        Assert.Equal(balance2, repository.GetBalance(balance2.AccountID));
        Assert.Equal(balance3, repository.GetBalance(balance3.AccountID));
    }

    [Fact]
    public void UpdatedBalance_GetBalanceShouldReturnUpdatedBalance()
    {
        repository.Update(new AccountBalance(0, 0));
        Assert.Equal(0, repository.GetBalance(0).Balance);

        repository.Update(new AccountBalance(0, 1000));
        Assert.Equal(1000, repository.GetBalance(0).Balance);
    }
}