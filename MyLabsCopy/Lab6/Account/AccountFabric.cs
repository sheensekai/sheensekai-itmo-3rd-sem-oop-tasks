using System;
using System.Collections.Generic;
using System.Text;

namespace MyLabsCopy.Lab6.Account
{
     class AccountFabric : IAccountFabric
    {
         public AAccount CreateAccount(double comission, double limit)
        {
            CreditCardAccount account = new CreditCardAccount(comission, limit);
            return account;
        }

         public AAccount CreateAccount(double percent)
        {
            CurrentAccount account = new CurrentAccount(percent);
            return account;
        }

         public AAccount CreateAccount(double percent, object date)
        {
            DepositAccount account = new DepositAccount(percent, date);
            return account;
        }
    }
}
