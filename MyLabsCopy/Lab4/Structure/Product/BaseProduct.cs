using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;

namespace MyLabs.Lab4.Structure
{
    class BaseProduct
    {
        public string Name { get; }

        public string Type { get; }


        public BaseProduct(string name, string type)
        {
            this.Name = name;
            this.Type = type;
        }

        public BaseProduct(BaseProduct other)
        {
            this.Name = other.Name;
            this.Type = other.Type;
        }

        public static bool operator ==(BaseProduct lhs, BaseProduct rhs)
            => object.ReferenceEquals(lhs, rhs) ? true : lhs.Name == rhs.Name && lhs.Type == rhs.Type;

        public static bool operator !=(BaseProduct lhs, BaseProduct rhs)
            => !(lhs == rhs);
    }
}
