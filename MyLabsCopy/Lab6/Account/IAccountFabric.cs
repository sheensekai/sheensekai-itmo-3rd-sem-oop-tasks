using System;
using System.Collections.Generic;
using System.Text;

namespace MyLabsCopy.Lab6.Account
{
    interface IAccountFabric
    {
        AAccount CreateAccount(double comission, double limit); // credit
        AAccount CreateAccount(double percent); // current
        AAccount CreateAccount(double percent, object date); // deposit

    }
}
