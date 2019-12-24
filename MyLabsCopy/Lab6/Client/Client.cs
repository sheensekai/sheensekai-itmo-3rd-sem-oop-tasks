using System;
using System.Collections.Generic;
using System.Text;
using MyLabsCopy.Lab6.Account;
using MyLabsCopy.Lab6.Requests;

namespace MyLabsCopy.Lab6.Client
{
    class Client
    {
        public Client(string name, string surname, string address, int id)
        {
            this.name = name;
            this.surname = surname;
            this.address = address;
            this.passport = id;
            this.Accounts = new List<IAccount>();

            if (address == null || id == 0)
            {
                suspicious = true;
                susp_limit = 1000;
            }
            else
            {
                suspicious = false;
            }

        }

        private string name;
        private string surname;
        private string address;
        private Int32 passport;
        public bool suspicious { get; }
        private double susp_limit;
        public List<IAccount> Accounts { get; }

        public void AddCreditCardAccount(double comission, double limit)
        {
            IAccount account = new AccountFabric().CreateAccount(comission, limit);
            if (suspicious)
            {
                account = new SuspiciousAccount(account, susp_limit);
            }

            Accounts.Add(account);
        }

        public void AddAccount(IAccount account)
        {
            Accounts.Add(account);
        }

    }
}
