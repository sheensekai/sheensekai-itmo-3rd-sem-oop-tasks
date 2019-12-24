using System;
using System.Collections.Generic;
using System.Text;
using MyLabsCopy.Lab6.Account;

namespace MyLabsCopy.Lab6.Requests
{
    static class PercentRequest
    {
        static public void GetInterest(CurrentAccount account)
        {
            List<ARequest> list = new List<ARequest>();
            list.Add(new CheckBalance());
            list.Add(new ExecuteOperation());
            list[0].next = list[1];
            list[0].Invoke(account);
        }
    }
}
