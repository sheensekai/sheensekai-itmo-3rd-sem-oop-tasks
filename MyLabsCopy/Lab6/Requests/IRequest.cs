using System;
using System.Collections.Generic;
using System.Text;
using MyLabsCopy.Lab6.Account;

namespace MyLabsCopy.Lab6.Requests
{
    interface IRequest
    {
        void CommitRequest(CreditCardAccount account);
        void CommitRequest(CurrentAccount account);
        void CommitRequest(DepositAccount account);

    }
}
