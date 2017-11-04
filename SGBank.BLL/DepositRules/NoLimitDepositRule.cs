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
    public class NoLimitDepositRule : IDeposit
    {
        public AccountDepositResponse Deposit(Account account, decimal amount)
        {
            AccountDepositResponse response = new AccountDepositResponse();
            response.OldBalance = account.Balance;
            response.Account = account;

            if (account.Type != AccountType.Basic && account.Type != AccountType.Premium)
            {
                response.Success = false;
                response.Message = "Error: Only basic and premium accounts can deposit with no limit. Contact IT";
                return response;
            }
            if (amount <= 0)
            {
                response.Success = false;
                response.Message = "Deposit amounts must be positive!";
                return response;
            }

            account.Balance += amount;
            response.Amount = amount;
            response.Success = true;

            return response;
        }
    }
}
