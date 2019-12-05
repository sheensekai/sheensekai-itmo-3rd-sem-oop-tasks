using System;
using System.Collections.Generic;
using System.Text;

namespace MyLabs.Lab4.Structure
{
    class AProduct
    {
        public string Type { get; }
        public string Name { get; }

        public AProduct(string type, string name)
        {
            this.Type = type;
            this.Name = name;
        }

        public AProduct(AProduct other)
            : this(other.Type, other.Name)
        {
        }
    }
}