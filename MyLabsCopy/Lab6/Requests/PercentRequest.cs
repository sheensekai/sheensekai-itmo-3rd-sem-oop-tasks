using System;
using System.Collections.Generic;
using System.Text;
using MyLabsCopy.Lab6.Account;

namespace MyLabsCopy.Lab6.Requests
{
    class PercentRequest : IRequest
    {
        public void CommitRequest(CreditCardAccount account)
        { }

        public void CommitRequest(CurrentAccount account)
        {
            account.balance *= account.percent;
        }
        public void CommitRequest(DepositAccount account)
        {
            account.balance *= account.percent;
        }
    }
}
