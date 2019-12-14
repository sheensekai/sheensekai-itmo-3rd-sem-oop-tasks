using System;
using System.Collections.Generic;
using System.Text;

namespace MyLabsCopy.Lab5
{
    [Serializable]
    public class Triangle
    {
        public Point a;
        public Point b;
        public Point c;
        public Triangle()
        { }
        public Triangle(Point a, Point b, Point c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }

        public override string ToString()
        {
            return $"{nameof(a)}: {a}\n, {nameof(b)}: {b}\n, {nameof(c)}: {c}\n";
        }

    }
}
