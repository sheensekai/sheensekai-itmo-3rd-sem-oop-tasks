using System;
using System.Collections.Generic;
using System.Text;

namespace MyLabsCopy.Lab6.Account.AccountFabric
{
    class CreditCardAccountFabric : IAccountFabric
    {
        private double comission;
        private double limit;

        public CreditCardAccountFabric(double comission, double limit)
        {
            this.comission = comission;
            this.limit = limit;
        }

        public AAccount CreateAccount()
        {
            CreditCardAccount account = new CreditCardAccount(comission, limit);
            return account;
        }
    }
}
