using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Reflection.PortableExecutable;
using System.Text;
using MyLabsCopy.Lab6.Requests;

namespace MyLabsCopy.Lab6.Account
{
    abstract class AAccount : IAccount
    {
        internal double balance;

        public AAccount()
        {
            balance = 0;
        }

        public abstract void HandleRequest(IRequest request);
        public void Withdrawal(double amount)
        {
            if (CheckForWithdrawal(amount))
            {
                ExtraForWithdrawal(amount);
                balance -= amount;
            }
            else
            {
                throw new AccountException("Cannot make a withdraw");
            }
        }

        public void Transfer(AAccount receiver, double amount)
        {
            if (CheckForTransfer(receiver, amount))
            {
                ExtraForTransfer(receiver, amount);
                this.balance -= amount;
                receiver.balance += amount;
            }
            else
            {
                throw new AccountException("Cannot make a transfer");
            }
        }

        public void Replenishment(double amount)
        {
            if (CheckForReplenishment(amount))
            {
                ExtraForReplenishment(amount);
                this.balance += amount;
            }
            else
            {
                throw new AccountException("Cannot make a replenishment");
            }
        }
        protected abstract bool CheckForWithdrawal(double amount);

        protected abstract bool CheckForTransfer(AAccount receiver, double amount);

        protected abstract bool CheckForReplenishment(double amount);

        protected abstract void ExtraForWithdrawal(double amount);

        protected abstract void ExtraForTransfer(AAccount receiver, double amount);

        protected abstract void ExtraForReplenishment(double amount);


    }
}
