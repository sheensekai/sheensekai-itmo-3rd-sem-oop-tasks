using System;
using System.Collections.Generic;
using System.Text;

namespace MyLabsCopy.Lab6.Account.AccountFabric
{
    class CurrentAcountFabric : IAccountFabric
    {
        internal double percent;

        public CurrentAcountFabric(double percent)
        {
            this.percent = percent;
        }

        public AAccount CreateAccount()
        {
            CurrentAccount account = new CurrentAccount(percent);
            return account;
        }
    }
}
