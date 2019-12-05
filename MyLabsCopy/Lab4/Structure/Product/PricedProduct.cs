using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace MyLabs.Lab4.Structure
{
    class PricedProduct : CountProduct
    {
        public double Price { get; }

        public double Cost
        {
            get => Price * Amount;
        }

        public PricedProduct(CountProduct product, double price)
            : base(product)
        {
            this.Price = price;
        }

        public static bool operator ==(PricedProduct lhs, PricedProduct rhs)
            => object.ReferenceEquals(lhs, rhs)
                ? true
                : (CountProduct)lhs == (CountProduct)rhs &&
                  lhs.Price == rhs.Price;

        public static bool operator !=(PricedProduct lhs, PricedProduct rhs)
            => !(lhs == rhs);
    }
}
