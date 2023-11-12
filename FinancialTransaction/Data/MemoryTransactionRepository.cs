using System;
using System.Collections.Generic;
using FinancialTransaction.Models;
using System.Linq;

namespace FinancialTransaction.Data;

public class MemoryTransactionRepository: ITransactionRepository
{
    private readonly List<Transaction> transactions;

    public MemoryTransactionRepository()
    {
        transactions = new();
    }

    public IEnumerable<Transaction> GetLaterTransactions(uint originAccountID, DateTime dateTime)
    {
        return transactions.Where(t => t.OriginAccountID == originAccountID &&
            t.Datetime > dateTime);
    }

    public void Add(Transaction transaction)
    {
        transactions.Add(transaction);
    }
}