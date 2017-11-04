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
    class FileAccountTests
    {
        
        [TestCase("10001", "Visal", 700, AccountType.Basic)]
        [TestCase("10002", "Jame", 500, AccountType.Basic)]
        [TestCase("10003", "Kris", 300, AccountType.Premium)]
        [TestCase("10004", "Sophie", 100, AccountType.Free)]
        [TestCase("10005", "Ken", 600, AccountType.Premium)]
        public void CanGetAccount(string accountNumber, string name, decimal balance, AccountType type)
        {
            List<string> accountList = new List<string>();
            accountList.Add($"{accountNumber},{name},{balance},{type.ToString().Substring(0, 1).ToUpper()}");
            FileAccountRepository file = new FileAccountRepository();
            file.SaveToFile(accountList, @"C:\Test\FileTest.txt");

            Account accountToCheck = new Account() { AccountNumber = accountNumber, Name = name, Balance = balance, Type = type };
            Account accountExtract = file.GetAccount(accountNumber, @"C:\Test\FileTest.txt");

            Assert.AreEqual(accountToCheck.AccountNumber, accountExtract.AccountNumber);
        }


        [TestCase("20001", "Sot", 700, AccountType.Basic)]
        [TestCase("20002", "Oka", 500, AccountType.Basic)]
        [TestCase("20003", "Den", 300, AccountType.Premium)]
        [TestCase("20004", "Koola", 100, AccountType.Free)]
        [TestCase("20005", "Yean", 600, AccountType.Premium)]
        public void CanNotGetAccount(string accountNumber, string name, decimal balance, AccountType type)
        {
            List<string> accountList = new List<string>();
            accountList.Add($"{accountNumber},{name},{balance},{type.ToString().Substring(0, 1).ToUpper()}");
            FileAccountRepository file = new FileAccountRepository();
            file.SaveToFile(accountList, @"C:\Test\FileTest.txt");

            Account accountExtract = file.GetAccount("00000", @"C:\Test\FileTest.txt");

            Assert.IsNull(accountExtract);
        }

        [TestCase("55555", "Roth", 700, AccountType.Basic, true)]
        [TestCase("66666", "Ben", 500, AccountType.Basic, true)]
        [TestCase("77777", "Joe", 300, AccountType.Premium, true)]
        [TestCase("88888", "Rosy", 100, AccountType.Free, true)]
        [TestCase("99999", "Borothana", 600, AccountType.Premium, true)]
        public void CanSaveToFile(string accountNumber, string name, decimal balance, AccountType type, bool expected)
        {
            List<string> accountList = new List<string>();
            accountList.Add($"{accountNumber},{name},{balance},{type.ToString().Substring(0, 1).ToUpper()}");
            
            FileAccountRepository file = new FileAccountRepository();
            Assert.AreEqual(expected, file.SaveToFile(accountList, @"C:\Test\FileTest.txt"));
        }
        
        [TestCase("12345", "Pha", 700, AccountType.Basic)]
        [TestCase("23456", "Hav", 500, AccountType.Basic)]
        [TestCase("34567", "Dom", 300, AccountType.Premium)]
        [TestCase("45678", "Sam", 100, AccountType.Free)]
        [TestCase("56789", "Rose", 600, AccountType.Premium)]
        public void CanListAccount(string accountNumber, string name, decimal balance, AccountType type)
        {
            List<string> accountList = new List<string>();
            accountList.Add($"{accountNumber},{name},{balance},{type.ToString().Substring(0, 1).ToUpper()}");
            FileAccountRepository file = new FileAccountRepository();
            file.SaveToFile(accountList, @"C:\Test\FileTest.txt");

            Account accountToCheck = new Account() { AccountNumber = "00000", Name = name, Balance = balance, Type = type };
            List<string> accountExtract = file.ListAccount(accountToCheck, @"C:\Test\FileTest.txt");
                        
            Assert.AreEqual(accountList[0], accountExtract[0]);
        }        
    }
}
