using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using Microsoft.VisualBasic;

namespace Bank
{
    class Program
    {
        static void Main(string[] args)
        {
            Top:
            Console.WriteLine("**********");
            Console.WriteLine("1.Create Account");
            Console.WriteLine("2. GetSavingsAccountDetails");
            Console.WriteLine("3.GetcurrentAccountDetails");
            Console.WriteLine("4.GetfixedAccountDatails");
            Console.WriteLine("5.AddAllAccounts");
            Console.WriteLine("6.GetAllAccounts");
            Console.WriteLine("7.Exit");

            Console.WriteLine("select the Option");
            string Optsel = Console.ReadLine();

            if (Optsel == "1")
            {
                CreateAccount();
            }
            else if (Optsel == "2")
            {
                List<Account> savings = GetSavingAccountDetails();
                foreach (var item in savings)
                {
                    Console.WriteLine("your Account Number :" + item.AccountNumber);
                    Console.WriteLine("your AccountType :" + item.AccountType);
                    Console.WriteLine("Your AccountBalance: " + item.AccountBalance);
                    Console.WriteLine("Your Account : " + item.AccCreatedDate);
                    Console.WriteLine("your Account Number :" + item.UserBio);
                    Console.WriteLine();

                }
            }
            else if (Optsel == "3")
            {
                List<Account> current = GetCurrentAccountDetails();
                foreach (var item in current)
                {
                    Console.WriteLine("your Account Number :" + item.AccountNumber);
                    Console.WriteLine("your AccountType :" + item.AccountType);
                    Console.WriteLine("Your AccountBalance: " + item.AccountBalance);
                    Console.WriteLine("Your Account : " + item.AccCreatedDate);
                    Console.WriteLine("your Account Number :" + item.UserBio);
                    Console.WriteLine();

                }
            }
            else if (Optsel == "4")
            {
                List<Account> fixedAcc = GetFixedAccountDetails();
                foreach (var item in fixedAcc)
                {
                    Console.WriteLine("your Account Number :" + item.AccountNumber);
                    Console.WriteLine("your AccountType :" + item.AccountType);
                    Console.WriteLine("Your AccountBalance: " + item.AccountBalance);
                    Console.WriteLine("Your Account : " + item.AccCreatedDate);
                    Console.WriteLine("your Account Number :" + item.UserBio);
                    Console.WriteLine();
                }

            }
            else if (Optsel == "5")
            {
                AddAllAccounts();
            }
            else if (Optsel=="6")
            {
                List<List<Account>> allAccounts = GetAllAcounts();
                foreach (var i in allAccounts)
                {
                    foreach (var item in i)
                    {
                        Console.WriteLine("your Account Number :" + item.AccountNumber);
                        Console.WriteLine("your AccountType :" + item.AccountType);
                        Console.WriteLine("Your AccountBalance: " + item.AccountBalance);
                        Console.WriteLine("Your Account : " + item.AccCreatedDate);
                        Console.WriteLine("your Account Number :" + item.UserBio);
                        Console.WriteLine();
                    }
                }

            }
            else if(Optsel=="7")
            {
                System.Environment.Exit(0);
            }

            else
            {
                Console.WriteLine("Enter valid input");
                goto Top;
            }
            
            goto Top;
        }
        static void CreateAccount()
        {
           

            List<Account> savings = new List<Account>();
            List<Account> current = new List<Account>();
            List<Account> fixedAcc = new List<Account>();

            Account a = new Account();

            a.UserBio = new List<Bio>();
            Bio b1 = new Bio();

            b1.UserAddress=new List<Address>();
            Address ad = new Address();

            b1.UserName = new List<Name>();
            Name n = new Name();


            Console.WriteLine("Enter your firstName");
            n.FirstName = Console.ReadLine();

            Console.WriteLine("Enter your LastName");
            n.LastName = Console.ReadLine();

            b1.UserName.Add(n);

            Console.WriteLine("Enter your Age");
            b1.Age = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter your MobileNumber");
            b1.MobileNo = Console.ReadLine();

            Console.WriteLine("Enter your MailId");
            b1.MailId = Console.ReadLine();

            a.UserBio.Add(b1);

            Console.WriteLine("Enter your H_No");
            ad.H_No = Console.ReadLine();

            Console.WriteLine("Enter street Name");
            ad.Street = Console.ReadLine();


            Console.WriteLine("Enter city");
            ad.City = Console.ReadLine();

            Console.WriteLine("Enter State");
            ad.State = Console.ReadLine();

            Console.WriteLine("Enter PinCode");
            ad.PinCode = Console.ReadLine();

            b1.UserAddress.Add(ad);


            Random random = new Random();
            string str =Convert.ToString(random.Next(35271519,35271590));
            a.AccountNumber = str;

          
            a.AccCreatedDate = Convert.ToDateTime(DateTime.Today);

            Ac:
            Console.WriteLine("Enter your AccountType: ?(savings/Current/Fixed):");
            a.AccountType = Console.ReadLine();
            a.AccountType = a.AccountType.ToUpper();

            if(a.AccountType=="SAVINGS")
            {
                savings = GetSavingAccountDetails();
                savings.Add(a);
                string strObject = JsonConvert.SerializeObject(savings);
                StreamWriter sw = new StreamWriter("savings.json");
                sw.Write(strObject);
                sw.Close();
            }
            else if(a.AccountType == "CURRENT")
            {
                current=GetCurrentAccountDetails();
                current.Add(a);
                string strObj = JsonConvert.SerializeObject(current);
                StreamWriter sw = new StreamWriter("current.json");
                sw.Write(strObj);
                sw.Close();
            }
            else if(a.AccountType == "FIXED")
            {
                fixedAcc=GetFixedAccountDetails();
                fixedAcc.Add(a);
                string strObje = JsonConvert.SerializeObject(fixedAcc);
                StreamWriter sw = new StreamWriter("fixed.json");
                sw.Write(strObje);
                sw.Close();
            }
            else
            {
                Console.WriteLine("ENter a valid AccountType");
                goto Ac;
            }
            Console.WriteLine("Your Account has been created");
            
            
        }
        static List<Account> GetSavingAccountDetails()
        {
            string strData = File.ReadAllText("savings.json");
            List<Account> lst=JsonConvert.DeserializeObject<List<Account>>(strData);
            return lst;
        }
        static List<Account> GetCurrentAccountDetails()
        {
            string strData = File.ReadAllText("current.json");
            List<Account> lst = JsonConvert.DeserializeObject<List<Account>>(strData);
            return lst;
        }
        static List<Account> GetFixedAccountDetails()
        {
            string strData = File.ReadAllText("fixed.json");
            List<Account> lst = JsonConvert.DeserializeObject<List<Account>>(strData);
            return lst;
        }
        static void AddAllAccounts()
        {
            List<List<Account>> allAccounts = new List<List<Account>>();
            allAccounts = GetAllAcounts();
            List<Account> savings = GetSavingAccountDetails();
            List<Account> current = GetCurrentAccountDetails();
            List<Account> fixedAcc = GetFixedAccountDetails();

            allAccounts.Add(savings);

            allAccounts.Add(current);

            allAccounts.Add(fixedAcc);

            string strObject = JsonConvert.SerializeObject(allAccounts);
            StreamWriter sw = new StreamWriter("AllAccounts.json");
            sw.Write(strObject);
            sw.Close();
            Console.WriteLine("All List has been addded");
        }
        static List<List<Account>> GetAllAcounts()
        {
            string strData = File.ReadAllText("AllAccounts.json");
            List<List<Account>> lst = JsonConvert.DeserializeObject<List<List<Account>>>(strData);
            return lst;

        }
    }
    public class Account
    {     
        public List<Bio> UserBio { get; set; }
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }
        public double AccountBalance { get; set; }
        public DateTime AccCreatedDate { get; set; }
       
    }
    public class Bio
    {
        public List<Name> UserName { get; set; }
        public int Age { get; set; }
        public string MobileNo { get; set; }
        public string MailId { get; set; }
        public List<Address> UserAddress { get; set; }
    }
    public class Name
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    public class Address
    {
        public string H_No { get; set; }
        public string Street { get; set; }
        public string PinCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }

    }
    
}
