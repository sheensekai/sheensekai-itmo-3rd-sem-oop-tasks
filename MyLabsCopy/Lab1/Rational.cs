using System;
using System.Collections.Generic;
using System.Text;

namespace MyLabs.Lab1
{
    class Rational
    {
        private readonly int den;
        private readonly int num;
        public Rational(int num = 0, int den = 1)
        {
            if (den == 0)
            {
                throw new ArgumentException("Denominator equals to zero.");
            }

            if (den < 0)
            {
                num = -num;
                den = -den;
            }

            this.den = den;
            this.num = num;

            int tmp = GCD(den, num);
            while (tmp > 1)
            {
                this.den /= tmp;
                this.num /= tmp;
                tmp = GCD(this.den, this.num);
            }

        }

        public Rational(Rational other)
        {
            den = other.den;
            num = other.num;
        }

        public static Rational operator +(Rational val)
            => new Rational(val);


        public static Rational operator -(Rational val)
            => new Rational(-val.num, val.den);

        public static Rational operator +(Rational lhs, Rational rhs)
        {
            if (lhs.num == 0)
            {
                return rhs;
            }

            if (rhs.num == 0)
            {
                return lhs;
            }

            return new Rational(lhs.num * rhs.den + rhs.num * lhs.den,
                lhs.den * rhs.den);
        }

        public static Rational operator -(Rational lhs, Rational rhs)
            => lhs + (-rhs);
        

        public static Rational operator *(Rational lhs, Rational rhs)
            => new Rational(lhs.num * rhs.num, lhs.den * rhs.den);

        public static Rational operator /(Rational lhs, Rational rhs)
        {
            if (rhs.num == 0)
                throw new DivideByZeroException();

            return new Rational(lhs.num * rhs.den, lhs.den * rhs.num);
        }

        public static bool operator ==(Rational lhs, Rational rhs)
        {
            if (object.ReferenceEquals(lhs, null))
            {
                return object.ReferenceEquals(rhs, null);
            }

            if (object.ReferenceEquals(rhs, null))
            {
                return object.ReferenceEquals(lhs, null);
            }

            if (lhs.num == 0)
            {
                return rhs.num == 0;
            }

            return (lhs.num == rhs.num && lhs.den == rhs.den);
        }

        public static bool operator !=(Rational lhs, Rational rhs)
            => !(lhs == rhs);

        public static bool operator >(Rational lhs, Rational rhs)
            => (lhs.num * rhs.den > rhs.num * lhs.den);

        public static bool operator <(Rational lhs, Rational rhs)
            => (lhs.num * rhs.den < rhs.num * lhs.den);

        public static bool operator <=(Rational lhs, Rational rhs)
            => (lhs.num * rhs.den <= rhs.num * lhs.den);

        public static bool operator >=(Rational lhs, Rational rhs)
            => (lhs.num * rhs.den >= rhs.num * lhs.den);

        public override string ToString()
            => $"{num} / {den}";

        public double value
        {
            get
            {
                return (double)num / den;
            }
        }

        private static int GCD(int a, int b)
        {
            a = Math.Abs(a);
            b = Math.Abs(b);
            while (a != 0 && b != 0)
            {
                Console.Write(".");
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }

            return a == 0 ? b : a;
        }

    }
}
