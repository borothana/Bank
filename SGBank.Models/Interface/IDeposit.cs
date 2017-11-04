using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGBank.Models.Response;

namespace SGBank.Models.Interface
{
    public interface IDeposit
    {
        AccountDepositResponse Deposit(Account account, decimal amount);
    }
}
