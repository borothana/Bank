using NUnit.Framework;
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

namespace SGBank.Tests
{
    [TestFixture]
    public class FreeAccountTests
    {
        [Test]
        public void CanLoadFreeAccountTestData()
        {
            AccountManager manager = AccountManagerFactory.Create();

            AccountLookupResponse response = manager.LookupAccount("12345");

            Assert.IsNotNull(response.Account);
            Assert.IsTrue(response.Success);
            Assert.AreEqual("12345", response.Account.AccountNumber);
        }
        
        [TestCase("12345", "Free Account", 100, AccountType.Free, 250, 100, false)]
        [TestCase("12345", "Free Account", 100, AccountType.Free, -100, 100, false)]
        [TestCase("12345", "Free Account", 100, AccountType.Basic, 50, 100, false)]
        [TestCase("12345", "Free Account", 100, AccountType.Free, 50, 150, true)]
        public void FreeAccountDepositRuleTest(string accountNumber, string name, decimal balance, AccountType type, decimal amount, decimal newBalance, bool expected)
        {
            IDeposit deposit = new FreeAccountDepositRule();
            Account account = new Account() { AccountNumber = accountNumber, Name = name, Balance = balance, Type = type };
            AccountDepositResponse response = deposit.Deposit(account, amount);
            Assert.AreEqual(expected, response.Success);
            Assert.AreEqual(newBalance, response.Account.Balance);
        }
        
        [TestCase("12345", "Free Account", 100, AccountType.Free, 50, 100, false)]
        [TestCase("12345", "Free Account", 100, AccountType.Free, -150, 100, false)]
        [TestCase("12345", "Free Account", 100, AccountType.Basic, -50, 100, false)]
        [TestCase("12345", "Free Account", 50, AccountType.Free, -60, 50, false)]
        [TestCase("12345", "Free Account", 90, AccountType.Free, -50, 40, true)]
        public void FreeAccountWithdrawRuleTest(string accountNumber, string name, decimal balance, AccountType type, decimal amount, decimal newBalance, bool expected)
        {
            IWithdraw withdraw = new FreeAccountWithdrawRule();
            Account account = new Account() { AccountNumber = accountNumber, Name = name, Balance = balance, Type = type };
            AccountWithdrawResponse response = withdraw.Withdraw(account, amount);
            Assert.AreEqual(expected, response.Success);
            Assert.AreEqual(newBalance, response.Account.Balance);
        }


    }       
}
