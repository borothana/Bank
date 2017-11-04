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
    class BasicAccountTests
    {
        [TestCase("33333", "Basic Account", 100, AccountType.Free, 250, 100, false)]
        [TestCase("33333", "Basic Account", 100, AccountType.Basic, -100, 100, false)]
        [TestCase("33333", "Basic Account", 100, AccountType.Basic, 250, 350, true)]

        public void BasicAccountDepositRuleTest(string accountNumber, string name, decimal balance, AccountType type, decimal amount, decimal newBalance, bool expected)
        {
            IDeposit deposit = new NoLimitDepositRule();
            Account account = new Account() { AccountNumber = accountNumber, Name = name, Balance = balance, Type = type };

            AccountDepositResponse response = deposit.Deposit(account, amount);
            Assert.AreEqual(expected, response.Success);
            Assert.AreEqual(newBalance, response.Account.Balance);
        }

        [TestCase("33333", "Basic Account", 1500, AccountType.Basic, -1000, 1500, false)]
        [TestCase("33333", "Basic Account", 100, AccountType.Free, -100, 100, false)]
        [TestCase("33333", "Basic Account", 100, AccountType.Basic, 100, 100, false)]
        [TestCase("33333", "Basic Account", 150, AccountType.Basic, -50, 100, true)]
        [TestCase("33333", "Basic Account", 100, AccountType.Basic, -150, -60, true)]

        public void BasicAccountWithdrawRuleTest(string accountNumber, string name, decimal balance, AccountType type, decimal amount, decimal newBalance, bool expected)
        {
            IWithdraw withdraw = new BasicAccountWithdrawRule();
            Account account = new Account() { AccountNumber = accountNumber, Name = name, Balance = balance, Type = type };

            AccountWithdrawResponse response = withdraw.Withdraw(account, amount);
            Assert.AreEqual(expected, response.Success);
            Assert.AreEqual(newBalance, response.Account.Balance);
        }
    }
}
