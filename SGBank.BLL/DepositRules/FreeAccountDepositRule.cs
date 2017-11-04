using SGBank.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGBank.Models;
using SGBank.Models.Response;

namespace SGBank.BLL.DepositRules
{
    public class FreeAccountDepositRule : IDeposit
    {
        public AccountDepositResponse Deposit(Account account, decimal amount)
        {
            AccountDepositResponse response = new AccountDepositResponse();
            response.Account = account;
            response.OldBalance = account.Balance;

            if (account.Type != AccountType.Free)
            {
                response.Success = false;
                response.Message = "Error: a non free account hit the Free Deposit Rule.  Contact IT";
                return response;
            }

            if(amount > 100)
            {
                response.Success = false;
                response.Message = "Free accounts cannot deposit more than $100 at a time";
                return response;
            }

            if (amount <= 0)
            {
                response.Success = false;
                response.Message = "Deposit amounts must be greater than zero.";
                return response;
            }

            account.Balance += amount;
            response.Amount = amount;
            response.Success = true;

            return response;
        }
    }
}
