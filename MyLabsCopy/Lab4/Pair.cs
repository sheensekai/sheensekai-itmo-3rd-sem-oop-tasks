using System;
using System.Collections.Generic;
using System.Text;

namespace MyLabs.Lab4
{
    public class Pair<T, K>
    {
        public T First { get; set; }
        public K Second { get; set; }

        public Pair(T first, K second)
        {
            this.First = first;
            this.Second = second;
        }
    }
}