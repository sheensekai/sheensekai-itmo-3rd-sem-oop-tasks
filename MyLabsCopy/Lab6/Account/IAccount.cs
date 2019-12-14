using System;
using System.Collections.Generic;
using System.Text;

namespace MyLabsCopy.Lab6.Account
{
    interface IAccount : IRequestHandler
    {
        void Withdrawal(double amount);

        void Transfer(AAccount receiver, double amount);

        void Replenishment(double amount);

    }
}
