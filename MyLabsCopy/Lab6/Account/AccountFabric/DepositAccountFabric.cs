using System;
using System.Collections.Generic;
using System.Text;

namespace MyLabsCopy.Lab6.Account.AccountFabric
{
    class DepositAccountFabric : IAccountFabric
    {
        private double percent;
        private object date;

        public DepositAccountFabric(double percent, object date)
        {
            this.percent = percent;
            this.date = date;
        }

        public AAccount CreateAccount()
        {
            DepositAccount account = new DepositAccount(percent, date);
            return account;
        }
    }
}
