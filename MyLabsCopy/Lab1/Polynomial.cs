using System;
using System.Collections.Generic;
using System.Text;

namespace MyLabs.Lab1
{
    class Polynomial
    {
        private readonly FractionList mults;

        public Polynomial(FractionList mults)
        {
            this.mults = mults;
        }

        public Polynomial()
            : this(new FractionList())
        {}

        public static Polynomial operator +(Polynomial lhs, Polynomial rhs)
            => new Polynomial(lhs.mults + rhs.mults);

        public static Polynomial operator -(Polynomial lhs, Polynomial rhs)
            => new Polynomial(lhs.mults - rhs.mults);
        public double Sum(double exp)
        {
            double result = 0;
            for (int i = 0; i < this.mults.Count(); ++i)
            {
                // explicit conversion vs. get what's better?
                //result += Math.Pow(exp, i) * (double)this.mults[i];
                result += Math.Pow(exp, i) * this.mults[i].value;
            }

            return result;
        }
    }
}
