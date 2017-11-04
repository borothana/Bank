using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGBank.Models;
using SGBank.Models.Interface;
using System.IO;
using System.Configuration;

namespace SGBank.Data
{
    public class FileAccountRepository : IAccountRepository
    {
        string _filePath = ConfigurationSettings.AppSettings["FilePath"];
        public Account LoadAccount(string accountNumber)
        {
            return GetAccount(accountNumber, _filePath);
        }

        public Account GetAccount(string AccountNumber, string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                sr.ReadLine();
                string tmp = "";
                try
                {

                    while ((tmp = sr.ReadLine()) != null)
                    {
                        string[] tmpList = tmp.Split(',');
                        if (tmpList[0].Replace("\"", "") == AccountNumber)
                        {
                            Account account = new Account();
                            account.AccountNumber = tmpList[0].Replace("\"", "");
                            account.Name = tmpList[1].Replace("\"", "");
                            decimal balance;
                            if (!decimal.TryParse(tmpList[2].Replace("\"", ""), out balance))
                            {
                                return null;
                            }                                
                            account.Balance = balance;
                            switch (tmpList[3].Replace("\"", "").ToUpper())
                            {
                                case "P":
                                    account.Type = AccountType.Premium;
                                    break;
                                case "F":
                                    account.Type = AccountType.Free;
                                    break;
                                case "B":
                                    account.Type = AccountType.Basic;
                                    break;
                                default:
                                    return null;
                            }

                            return account;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }
            return null;
        }
        public void SaveAccount(Account account)
        {
            List<string> list = MoveData(account, _filePath);
            if (list != null)
            {
                SaveToFile(list, _filePath);
            }
        }

        public List<string> MoveData(Account account, string path)
        {
            try
            {
                if (!File.Exists(path)) return null;

                File.Move(path, "tmp_" + path);
                List<string> accountList = ListAccount(account, "tmp_" + path);
                File.Delete("tmp_" + path);

                return accountList;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return null;
            }
        }

        public bool SaveToFile(List<string> accountList, string path)
        {
            try
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("AccountNumber, Name, Balance, Type");
                    for (int i = 0; i < accountList.Count; i++)
                    {
                        sw.WriteLine(accountList[i]);
                    }                        
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return false;
            }
            return true;
        }


        public List<string> ListAccount(Account account, string path)
        {
            List<string> result = new List<string>();
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    sr.ReadLine();
                    string tmp = "";
                    while ((tmp = sr.ReadLine()) != null)
                    {
                        string[] tmpList = tmp.Split(',');
                        if (tmpList[0].Replace("\"", "") == account.AccountNumber)
                        {
                            result.Add($"{account.AccountNumber},{account.Name},{account.Balance},{account.Type.ToString().Substring(0, 1).ToUpper()}");
                        }                            
                        else
                        {
                            result.Add(tmp);
                        }                            
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return null;
            }
            return result;
        }
    }
}

