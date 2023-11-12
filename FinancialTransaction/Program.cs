using System;
using System.Linq;
using System.Threading.Tasks;
using FinancialTransaction.Data;
using FinancialTransaction.Models;


//Obs: Voce é livre para implementar na linguagem de sua preferência, desde que respeite as funcionalidades e saídas existentes, além de aplicar os conceitos solicitados.

namespace FinancialTransaction;

class Program
{
    static void Main()
    {
        var repository = new MemoryAccountBalanceRepository();
        AccountAccountBalanceDataInitializer.Initialize(repository);

        var transactions = new Transaction[]
        {
            new() {CorrelationID= 1,Datetime= new DateTime(2023, 9, 9, 14, 15, 00), OriginAccountID= 938485762,  DestinationAccountID= 2147483649, Amount= 150},
            new() {CorrelationID= 2,Datetime= new DateTime(2023, 9, 9, 14, 15, 05), OriginAccountID= 2147483649, DestinationAccountID= 210385733,  Amount= 149},
            new() {CorrelationID= 3,Datetime= new DateTime(2023, 9, 9, 14, 15, 29), OriginAccountID= 347586970,  DestinationAccountID= 238596054,  Amount= 1100},
            new() {CorrelationID= 4,Datetime= new DateTime(2023, 9, 9, 14, 17, 00), OriginAccountID= 675869708,  DestinationAccountID= 210385733,  Amount= 5300},
            new() {CorrelationID= 5,Datetime= new DateTime(2023, 9, 9, 14, 18, 00), OriginAccountID= 238596054,  DestinationAccountID= 674038564,  Amount= 1489},
            new() {CorrelationID= 6,Datetime= new DateTime(2023, 9, 9, 14, 18, 20), OriginAccountID= 573659065,  DestinationAccountID= 563856300,  Amount= 49},
            new() {CorrelationID= 7,Datetime= new DateTime(2023, 9, 9, 14, 19, 00), OriginAccountID= 938485762,  DestinationAccountID= 2147483649, Amount= 44},
            new() {CorrelationID= 8,Datetime= new DateTime(2023, 9, 9, 14, 19, 01), OriginAccountID= 573659065,  DestinationAccountID= 675869708,  Amount= 150},
        };

        FinancialTransactionService transactionService = new(repository);
        Parallel.ForEach(transactions, item =>
        {
            transactionService.ExecuteTransaction(item);
        });
    }
}
