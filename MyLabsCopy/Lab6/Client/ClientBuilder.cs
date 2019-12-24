using System;
using System.Collections.Generic;
using System.Text;
using MyLabsCopy.Lab6.Account;

namespace MyLabsCopy.Lab6.Client
{
    class ClientBuilder
    {
        private string name = null;
        private string surname = null;
        private string address = null;
        private int id = 0;
        public ClientBuilder(string name, string surname)
        {
            this.name = name;
            this.surname = surname;
        }

        public ClientBuilder Address(string address)
        {
            this.address = address;
            return this;
        }

        public ClientBuilder Passport(int id)
        {
            this.id = id;
            return this;
        }

        public Client Build()
        {
            return new Client(name, surname, address, id);
        }

    }
}
