using System;
using System.Collections.Generic;
using System.Text;
using MyLabsCopy.Lab6.Requests;

namespace MyLabsCopy.Lab6.Account
{
    class CreditCardAccount : AAccount
    {
        internal double comission;
        internal double limit;
        public CreditCardAccount(double comission, double limit)
            : base()
        {
            this.comission = comission;
            this.limit = limit;
        }

        protected override bool CheckForWithdrawal(double amount)
        {
            double after_withdraw = balance - amount;
            return after_withdraw > 0 || after_withdraw - comission * amount > -limit;
        }

        protected override bool CheckForTransfer(AAccount receiver, double amount)
        {
            double after_transfer = balance - amount;
            return after_transfer > 0 || after_transfer - comission * amount > -limit;
        }

        protected override bool CheckForReplenishment(double amount)
        {
            return true;
        }

        protected override void ExtraForWithdrawal(double amount)
        {
            balance -= comission * amount;
        }

        protected override void ExtraForTransfer(AAccount receiver, double amount)
        {
            balance -= comission * amount;
        }

        protected override void ExtraForReplenishment(double amount)
        { }
    }
}
