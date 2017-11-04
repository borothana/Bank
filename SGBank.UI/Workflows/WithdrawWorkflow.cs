using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGBank.Models;
using SGBank.Models.Interface;
using SGBank.Models.Response;
using SGBank.BLL;

namespace SGBank.UI.Workflows
{
    public class WithdrawWorkflow
    {
        public void Execute()
        {
            Console.Clear();
            AccountManager accountManager = AccountManagerFactory.Create();
            
            Console.Write("Enter an acount number: ");
            string accountNumber = Console.ReadLine();

            Console.Write("Enter a withdraw amount: ");
            decimal amount;
            while (!decimal.TryParse(Console.ReadLine(), out amount))
            {
                Console.WriteLine("");
                Console.WriteLine("Invalid amount!");
                Console.WriteLine("Enter a deposit amount: ");
            }
            AccountWithdrawResponse response = accountManager.Withdraw(accountNumber, amount);

            if (response.Success)
            {
                Console.WriteLine("Withdraw complete!");
                Console.WriteLine($"Account Number: {response.Account.AccountNumber}");
                Console.WriteLine($"Old Balance: {response.OldBalance}");
                Console.WriteLine($"Amount Withdraw: {response.Amount}");
                Console.WriteLine($"New Balance: {response.Account.Balance}");

            }
            else
            {
                Console.Write("An error occurred:");
                Console.WriteLine(response.Message);
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
