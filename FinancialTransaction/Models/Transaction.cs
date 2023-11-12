using System;

namespace FinancialTransaction.Models;

public class Transaction
{
    public uint CorrelationID { get; set; }
    public DateTime Datetime { get; set; }
    public uint OriginAccountID { get; set; } 
    public uint DestinationAccountID { get; set; }
    public decimal Amount { get; set; }
}
