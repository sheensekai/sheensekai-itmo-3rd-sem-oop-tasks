using System;
using System.Collections.Generic;
using System.Text;
using MyLabsCopy.Lab6.Account;

namespace MyLabsCopy.Lab6.Requests
{
    abstract class ARequest
    {
        public ARequest next = null;
        abstract public void Invoke(CurrentAccount account);

    }
}
