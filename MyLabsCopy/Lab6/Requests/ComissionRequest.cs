using System;
using System.Collections.Generic;
using System.Text;
using MyLabsCopy.Lab6.Account;

namespace MyLabsCopy.Lab6.Requests
{
    class ComissionRequest : IRequest
    {
        public void CommitRequest(CreditCardAccount account)
        {
            account.balance -= account.comission;
        }
        public void CommitRequest(CurrentAccount account)
        { }
        public void CommitRequest(DepositAccount account)
        { }
    }
}
