using System;
using System.Collections.Generic;
using System.Text;

namespace MyLabsCopy.Lab6.Account
{
    class AccountException : Exception
    {
        public AccountException()
            : base()
        { }
        public AccountException(string message)
            : base(message)
        { }
    }

}
