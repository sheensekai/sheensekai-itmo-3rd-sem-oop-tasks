using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace MyLabs.Lab4.Structure
{
    class Product : AProduct
    {
        public int Id { get; set; }

        public int Amount { get; }

        public double Price { get; }


        public double Cost
        {
            get => Price * Amount;
        }

        public Product(string type, string name, int id = 0, int amount = 0, double price = 0)
            : base(type, name)
        {
            this.Id = id;
            this.Amount = amount;
            this.Price = price;
        }

        public Product(Product other)
            : this(other.Type, other.Name, other.Id, other.Amount, other.Price)
        {
        }

        public Product(Product other, int new_amount)
            : this(other)
        {
            this.Amount = new_amount;
        }

        public Product(Product other, double new_price)
            : this(other)
        {
            this.Price = new_price;
        }

        public Product(Product other, int new_amount, double new_price)
            : this(other)
        {
            this.Amount = new_amount;
            this.Price = new_price;
        }
    }
}