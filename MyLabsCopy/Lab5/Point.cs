using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualBasic.CompilerServices;

namespace MyLabsCopy.Lab5
{
    [Serializable]
    public class Point
    {
        public double x;
        public double y;
        public double z;

        public Point()
        { }
        public Point(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static bool operator==(Point lhs, Point rhs)
            => object.ReferenceEquals(lhs, rhs) ? true : lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z;

        public static bool operator !=(Point lhs, Point rhs)
            => !(lhs == rhs);

        public override string ToString()
        {
            return $"{nameof(x)}: {x}, {nameof(y)}: {y}, {nameof(z)}: {z}";
        }
    }
}
