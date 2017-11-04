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
using NUnit.Framework;

namespace SGBank.Tests
{
    [TestFixture]
    class PremiumAccountTests
    {
        [TestCase("55555", "Premium", 100, AccountType.Free, 150, 100, false)]
        [TestCase("55555", "Premium", 100, AccountType.Premium, -200, 100, false)]
        [TestCase("55555", "Premium", 100, AccountType.Premium, 50, 150, true)]

        public void PremiumAccountDepositRuleTest(string accountNumber, string name, decimal balance, AccountType type, decimal amount, decimal newBalance, bool expected)
        {
            IDeposit deposit = new NoLimitDepositRule();
            Account account = new Account() { AccountNumber = accountNumber, Name = name, Balance = balance, Type = type };

            AccountDepositResponse response = deposit.Deposit(account, amount);
            Assert.AreEqual(expected, response.Success);
            Assert.AreEqual(response.Account.Balance, newBalance);       
        }

        [TestCase("55555", "Premium", 500, AccountType.Premium, -1000, -500, true)]
        [TestCase("55555", "Premium", 200, AccountType.Free, -100, 200, false)]
        [TestCase("55555", "Premium", 200, AccountType.Premium, 100, 200, false)]
        [TestCase("55555", "Premium", 350, AccountType.Premium, -50, 300, true)]
        [TestCase("55555", "Premium", 200, AccountType.Premium, -250, -50, true)]

        public void PremiumAccountWithdrawRuleTest(string accountNumber, string name, decimal balance, AccountType type, decimal amount, decimal newbalance, bool expected)
        {
            IWithdraw withdraw = new PremiumAccountWithdrawRule();
            Account account = new Account() { AccountNumber = accountNumber, Name = name, Balance = balance, Type = type };

            AccountWithdrawResponse response = withdraw.Withdraw(account, amount);
            Assert.AreEqual(expected, response.Success);
            Assert.AreEqual(newbalance, response.Account.Balance);
        }
    }
}
