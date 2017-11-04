using System;
using System.Collections.Generic;
using System.Text;

namespace SGBank.Models.Interface
{
    public interface IAccountRepository
    {
        Account LoadAccount(string AccountNumber);
        void SaveAccount(Account account);
    }
}
