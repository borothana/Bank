using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGBank.Models;
using SGBank.Models.Interface;
using SGBank.Models.Response;

namespace SGBank.BLL.WithdrawRules
{
    public class FreeAccountWithdrawRule : IWithdraw
    {
        public AccountWithdrawResponse Withdraw(Account account, decimal amount)
        {
            AccountWithdrawResponse response = new AccountWithdrawResponse();
            response.Account = account;
            response.OldBalance = account.Balance;

            if (account.Type != AccountType.Free)
            {
                response.Success = false;
                response.Message = "Error: a non-free account hit the Free Withdraw Rule. Contact IT";
                return response;
            }
            if(amount >=0)
            {
                response.Success = false;
                response.Message = "Withdrawal amounts must be negative!";
                return response;
            }
            if (Math.Abs(amount) > 100)
            {
                response.Success = false;
                response.Message = "Free accounts cannot withdraw more than $100!";
                return response;
            }

            if (amount + account.Balance  < 0)
            {
                response.Success = false;
                response.Message = "Free accounts cannot overdraft!";
                return response;
            }
            
            response.Success = true;
            response.Amount = amount;
            account.Balance += amount;
            return response;
        }
    }
}
