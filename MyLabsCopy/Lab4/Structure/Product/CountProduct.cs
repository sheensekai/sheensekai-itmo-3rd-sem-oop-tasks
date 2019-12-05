using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualBasic.CompilerServices;

namespace MyLabs.Lab4.Structure
{
    class CountProduct : BaseProduct
    {
        public int Amount { get; }
       

        public CountProduct(BaseProduct product, int amount)
            : base(product)
        {
            this.Amount = amount;
        }

        public CountProduct(CountProduct other)
            : base( (BaseProduct)other )
        {
            this.Amount = other.Amount;
        }

        public static bool operator ==(CountProduct lhs, CountProduct rhs)
            => object.ReferenceEquals(lhs, rhs)
                ? true
                : (BaseProduct) lhs == (BaseProduct) rhs &&
                  lhs.Amount == rhs.Amount;

        public static bool operator !=(CountProduct lhs, CountProduct rhs)
            => !(lhs == rhs);
    }
}
