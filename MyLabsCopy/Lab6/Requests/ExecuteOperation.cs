using System;
using System.Collections.Generic;
using System.Text;
using MyLabsCopy.Lab6.Account;

namespace MyLabsCopy.Lab6.Requests
{
    class ExecuteOperation : ARequest
    {
        public override void Invoke(CurrentAccount account)
        {
            account.balance *= (account.percent + 1);

            if (next != null)
            {
                next.Invoke(account);
            }
        }
    }
}
