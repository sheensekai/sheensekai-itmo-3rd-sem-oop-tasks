using System;
using System.Collections.Generic;
using System.Text;
using MyLabsCopy.Lab6.Account;
using MyLabsCopy.Lab6.Account.AccountFabric;
using MyLabsCopy.Lab6.Requests;

namespace MyLabsCopy.Lab6.Client
{
    class Client : IRequestHandler
    {
        internal string name;
        internal string surname;
        internal string address;
        internal Int32 passport;
        internal bool suspicious;
        internal double susp_limit;
        internal List<IAccount> accounts;

        public void HandleRequest(IRequest request)
        {
            foreach (IAccount account in accounts)
            {
                account.HandleRequest(request);
            }
        }

        public void AddCreditCardAccount(double comission, double limit)
        {
            IAccount account = new CreditCardAccountFabric(comission, limit).CreateAccount();
            if (suspicious)
            {
                account = new SuspiciousAccount(account, susp_limit);
            }

            accounts.Add(account);
        }

    }
}
