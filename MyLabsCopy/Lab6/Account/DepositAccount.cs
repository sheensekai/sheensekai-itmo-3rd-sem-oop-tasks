using System;
using System.Collections.Generic;
using System.Text;
using MyLabsCopy.Lab6.Requests;

namespace MyLabsCopy.Lab6.Account
{
    class DepositAccount : AAccount
    {
        internal double percent;
        internal object date;
        internal bool expired;

        public DepositAccount(double percent, object date)
            : base()
        {
            this.percent = percent;
            this.date = date;
        }

        public override void HandleRequest(IRequest request)
        {
            request.CommitRequest(this);
        }

        protected override bool CheckForWithdrawal(double amount)
        {
            double after_withdraw = balance - amount;
            return after_withdraw > 0 && expired;
        }

        protected override bool CheckForTransfer(AAccount receiver, double amount)
        {
            double after_transfer = balance - amount;
            return after_transfer > 0 && expired;
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
