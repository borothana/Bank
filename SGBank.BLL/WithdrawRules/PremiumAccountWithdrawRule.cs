using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGBank.BLL.DepositRules;
using SGBank.BLL.WithdrawRules;
using SGBank.Data;
using SGBank.Models.Interface;
using SGBank.Models.Response;
using SGBank.BLL;
using SGBank.Models;

namespace SGBank.BLL.WithdrawRules
{
    public class PremiumAccountWithdrawRule : IWithdraw
    {
        public AccountWithdrawResponse Withdraw(Account account, decimal amount)
        {
            AccountWithdrawResponse response = new AccountWithdrawResponse();
            response.Account = account;
            response.OldBalance = account.Balance;

            if (account.Type != AccountType.Premium)
            {
                response.Success = false;
                response.Message = "Error: a non-premium account hit the Premium Withdraw Rule. Contact IT";
                return response;
            }
            if (amount >= 0)
            {
                response.Success = false;
                response.Message = "Withdrawal amounts must be negative!";
                return response;
            }
            if (amount + account.Balance < -500)
            {
                response.Success = false;
                response.Message = "This amount will overdraft more than your $500 limit!";
                return response;
            }

            response.Success = true;
            response.Amount = amount;
            account.Balance += amount;            
            return response;
        }
    }
}
