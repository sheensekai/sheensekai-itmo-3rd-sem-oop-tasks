using System;
using System.Collections.Generic;
using System.Text;
using MyLabsCopy.Lab6.Account;

namespace MyLabsCopy.Lab6.Client
{
    class ClientBuilder
    {
        private Client client;
        public ClientBuilder(string name, string surname)
        {
            client = new Client();
            client.name = name;
            client.surname = surname;
            client.suspicious = true;
            client.susp_limit = 1000;
            client.accounts = new List<IAccount>();
        }

        public ClientBuilder Address(string address)
        {
            client.address = address;
            CheckIfSuspicious();
            return this;
        }

        public ClientBuilder Passport(int id)
        {
            client.passport = id;
            CheckIfSuspicious();
            return this;
        }

        private void CheckIfSuspicious()
        {
            if (client.passport != null && !string.IsNullOrEmpty(client.address))
            {
                client.suspicious = false;
            }
        }
    }
}
