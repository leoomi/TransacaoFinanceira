using System;
using System.Collections.Generic;
using FinancialTransaction.Models;

namespace FinancialTransaction.Data;

public interface ITransactionRepository
{
    IEnumerable<Transaction> GetLaterTransactions(uint originAccountID, DateTime dateTime);
    void Add(Transaction transaction);
}