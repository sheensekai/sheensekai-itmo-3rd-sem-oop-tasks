using System;
using System.Collections.Generic;
using System.Text;
using MyLabsCopy.Lab6.Requests;

namespace MyLabsCopy.Lab6.Account
{
    class CurrentAccount : AAccount
    {
        internal double percent;

        public CurrentAccount(double percent)
            : base()
        {
            this.percent = percent;
        }

        public override void HandleRequest(IRequest request)
        {
            request.CommitRequest(this);
        }

        protected override bool CheckForWithdrawal(double amount)
        {
            double after_withdraw = balance - amount;
            return after_withdraw > 0;
        }

        protected override bool CheckForTransfer(AAccount receiver, double amount)
        {
            double after_transfer = balance - amount;
            return after_transfer > 0;
        }

        protected override bool CheckForReplenishment(double amount)
        {
            return true;
        }

        protected override void ExtraForWithdrawal(double amount)
        { }

        protected override void ExtraForTransfer(AAccount receiver, double amount)
        { }

        protected override void ExtraForReplenishment(double amount)
        { }
    }
}
