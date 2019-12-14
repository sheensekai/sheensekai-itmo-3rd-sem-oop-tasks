using System;
using System.Collections.Generic;
using System.Text;

namespace MyLabsCopy.Lab6.Account
{
    class SuspiciousAccount : IAccount
    {
        private IAccount account;
        private double limit;

        public SuspiciousAccount(IAccount account, double limit)
        {
            this.account = account;
            this.limit = limit;
        }

        public void Withdrawal(double amount)
        {
            if (amount < limit)
            {
                account.Withdrawal(amount);
            }
            else
            {
                throw new AccountException("Suspicious Client");
            }
        }

        public void Transfer(AAccount receiver, double amount)
        {
            if (amount < limit)
            {
                account.Transfer(receiver, amount);
            }
            else
            {
                throw new AccountException("Suspicious Client");
            }
        }

        public void Replenishment(double amount)
        {
            account.Replenishment(amount);
        }
    }
}
