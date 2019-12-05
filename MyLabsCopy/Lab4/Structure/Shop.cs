using System;
using System.Collections.Generic;
using System.Text;

namespace MyLabs.Lab4.Structure
{
    class Shop
    {
        public string Name { get; }

        public int Id { get; }

        public Shop(string name, int id = 0)
        {
            this.Name = name;
            this.Id = id;
        }

        public Shop(Shop other)
        {
            this.Name = other.Name;
            this.Id = other.Id;
        }
        static public bool operator==(Shop lhs, Shop rhs)
            => object.ReferenceEquals(lhs, rhs) ? true : lhs.Name == rhs.Name && lhs.Id == rhs.Id;

        static public bool operator !=(Shop lhs, Shop rhs)
            => !(lhs == rhs);

    }

    class ShopEqualityComparer : IEqualityComparer<Shop>
    {
        public bool Equals(Shop lhs, Shop rhs)
        {
            return lhs == rhs;
        }

        public int GetHashCode(Shop shop)
        {
            int hCode = shop.Id;
            return hCode.GetHashCode();
        }
    }
}