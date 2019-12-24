using System;
using System.Collections.Generic;
using System.Text;
using MyLabsCopy.Lab6.Account;

namespace MyLabsCopy.Lab6.Requests
{
    class CheckBalance : ARequest
    {
         public override void Invoke(CurrentAccount account)
         {
             if (account.balance < 0)
             {
                 throw new AccountException("Balance exception");
             }

             if (next != null)
             {
                 next.Invoke(account);
             }
         }

    }
}
