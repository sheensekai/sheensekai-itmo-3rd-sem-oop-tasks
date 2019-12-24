using System;
using System.Collections.Generic;
using System.Text;
using MyLabsCopy.Lab6.Requests;

namespace MyLabsCopy.Lab6.Account
{
    interface IAccount
    {
        void Withdrawal(double amount);

        void Transfer(AAccount receiver, double amount);

        void Replenishment(double amount);

        double GetBalance();
    }
}
