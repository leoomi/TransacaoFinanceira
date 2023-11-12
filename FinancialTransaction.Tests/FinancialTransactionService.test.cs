using System;
using System.Threading;
using System.Threading.Tasks;
using FinancialTransaction.Data;
using FinancialTransaction.Models;
using Microsoft.VisualBasic;
using Moq;
using Xunit;

namespace FinancialTransaction.Tests;

public class FinancialTransactionServiceTests: IDisposable
{
    private IFinancialTransactionService service;
    private Mock<IAccountBalanceRepository> repository;

    public FinancialTransactionServiceTests()
    {
        repository = new Mock<IAccountBalanceRepository>();
        service = new FinancialTransactionService(repository.Object);
    }

    public void Dispose()
    {
        service = null;
        repository = null;
    }

    [Fact]
    public void ExistingAccounts_ExecuteShouldGetBothAccounts()
    {
        var acc1 = new AccountBalance(1, 100);
        var acc2 = new AccountBalance(2, 100);
        AddAccountToRepository(acc1);
        AddAccountToRepository(acc2);

        service.ExecuteTransaction(new Transaction{
            CorrelationID= 1,
            OriginAccountID= 1,
            DestinationAccountID=2,
            Amount= 100}
        );

        repository.Verify(r => r.GetBalance(1), Times.Once);
        repository.Verify(r => r.GetBalance(2), Times.Once);
    }

    [Fact]
    public void ExistingAccountsEnoughBalance_ExecuteShouldTransferBalance()
    {
        var acc1 = new AccountBalance(1, 100);
        var acc2 = new AccountBalance(2, 100);
        AddAccountToRepository(acc1);
        AddAccountToRepository(acc2);

        service.ExecuteTransaction(new Transaction{
            CorrelationID= 1,
            OriginAccountID= 1,
            DestinationAccountID=2,
            Amount= 100}
        );

        Assert.Equal(0, acc1.Balance);
        Assert.Equal(200, acc2.Balance);
    }

    [Fact]
    public void ExistingAccountsInsuficcienthBalance_ExecuteShouldDoNothing()
    {
        var acc1 = new AccountBalance(1, 50);
        var acc2 = new AccountBalance(2, 100);
        AddAccountToRepository(acc1);
        AddAccountToRepository(acc2);

        service.ExecuteTransaction(new Transaction{
            CorrelationID= 1,
            OriginAccountID= 1,
            DestinationAccountID=2,
            Amount= 100}
        );

        Assert.Equal(50, acc1.Balance);
        Assert.Equal(100, acc2.Balance);
    }

    [Fact]
    public void LockedAccount1_ExecuteShouldBeLocked()
    {
        var acc1 = new AccountBalance(1, 100);
        var acc2 = new AccountBalance(2, 100);
        Task task;

        try {
            AddAccountToRepository(acc1);
            AddAccountToRepository(acc2);
            Monitor.Enter(acc1);

            task = Task.Run(() =>
            service.ExecuteTransaction(new Transaction{
                CorrelationID= 1,
                OriginAccountID= 1,
                DestinationAccountID=2,
                Amount= 100}
            ));
            Thread.Sleep(1000);

            Assert.Equal(100, acc1.Balance);
            Assert.Equal(100, acc2.Balance);
        } finally {
            Monitor.Exit(acc1);
        }
    }

        [Fact]
    public void LockedAccount2_ExecuteShouldBeLocked()
    {
        var acc1 = new AccountBalance(1, 100);
        var acc2 = new AccountBalance(2, 100);
        Task task;

        try {
            AddAccountToRepository(acc1);
            AddAccountToRepository(acc2);
            Monitor.Enter(acc2);

            task = Task.Run(() =>
            service.ExecuteTransaction(new Transaction{
                CorrelationID= 1,
                OriginAccountID= 1,
                DestinationAccountID=2,
                Amount= 100}
            ));
            Thread.Sleep(1000);

            Assert.Equal(100, acc1.Balance);
            Assert.Equal(100, acc2.Balance);
        } finally {
            Monitor.Exit(acc2);
        }
    }

    private void AddAccountToRepository(AccountBalance balance)
    {
        repository.Setup(r => r.GetBalance(balance.AccountID)).Returns(balance);
    }
}