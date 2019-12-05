using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace MyLabs.Lab1
{
    enum Operation
    {
        Plus,
        Minus,
        FindGreater,
        FindLess
    };

    class FractionList
    {
        private readonly List<Rational> vals;

        private Rational maxVal;
        private Rational minVal;
        private readonly List<Request> reqs;
        private static readonly int reqs_size = 5;

        private class Request
        {
            public Request(Operation op, Rational key, int result)
            {
                this.op = op;
                this.key = key;
                this.result = result;
            }
            public Operation op { get; set; }
            public Rational key { get; set; }
            public int result { get; set; }
        }


        public FractionList()
        {
            maxVal = null;
            minVal = null;
            vals = new List<Rational>();
            reqs = new List<Request>();
        }

        public Rational this[int i]
        {
            get { return vals[i]; }
        } 

        public void Add(Rational val)
        {
            vals.Add(val);

            if (maxVal == null || maxVal < val)
            {
                maxVal = val;
            }

            if (minVal == null || minVal > val)
            {
                minVal = val;
            }

        }

        private int CountImpl(Rational key, Operation op)
        {
            // before counting
            // I check whether there is the same
            // request in request list
                Request check = reqs.Find(delegate (Request req)
                {
                    return req.op == op &&
                        req.key == key;
                });

                //reqs.Where(req => req.op == op && req.key == key);

                // update request
                if (!object.ReferenceEquals(check, null))
                {
                    reqs.Remove(check);
                    reqs.Add(check);
                    return check.result;
                }

            int result = 0;
            if (op == Operation.FindGreater)
            {
                foreach (var val in vals)
                {
                    if (val > key)
                    {
                        ++result;
                    }
                }
            }
            else if (op == Operation.FindLess)
            {
                foreach (var val in vals)
                {
                    if (val < key)
                    {
                        ++result;
                    }
                }
            }

            if (reqs.Count >= reqs_size)
            {
                reqs.RemoveAt(0);
            }
            reqs.Add(new Request(op, key, result));

            return result;
        }

        public int CountGreaterThan(Rational key)
            => CountImpl(key, Operation.FindGreater);

        public int CountLessThan(Rational key)
            => CountImpl(key, Operation.FindLess);

        public Rational Max()
            => maxVal;

        public Rational Min()
            => minVal;

        private static FractionList PlusMinusImpl(FractionList lhs, FractionList rhs, Operation op)
        {
            if (lhs == null)
            {
                return rhs;
            }

            if (rhs == null)
            {
                return lhs;
            }

            FractionList shorter = (lhs.vals.Count <= rhs.vals.Count ? lhs : rhs);
            FractionList longer = (lhs.vals.Count > rhs.vals.Count ? lhs : rhs);
            FractionList result = new FractionList();
            
            if (op == Operation.Plus)
            {
                for (int i = 0; i < shorter.vals.Count; ++i)
                {
                    result.Add(lhs.vals[i] + rhs.vals[i]);
                }
            }
            else
            {
                for (int i = 0; i < shorter.vals.Count; ++i)
                {
                    result.Add(lhs.vals[i] - rhs.vals[i]);
                }
            }


            if (shorter.vals.Count < longer.vals.Count)
            {
                for (int i = shorter.vals.Count; i < longer.vals.Count; ++i)
                {
                    result.Add(longer.vals[i]);
                }
            }

            return result;
        }

        public static FractionList operator +(FractionList lhs, FractionList rhs)
            => PlusMinusImpl(lhs, rhs, Operation.Plus);

        public static FractionList operator -(FractionList lhs, FractionList rhs)
            => PlusMinusImpl(lhs, rhs, Operation.Minus);

        public override string ToString()
            => vals.ToString();

        public int Count()
            => vals.Count;

        public static FractionList ReadFromFile(string name)
        {
            string[] ints;
            FractionList result = new FractionList();

            foreach(string str in File.ReadAllLines(name))
            {
                ints = str.Split();
                int num = int.Parse(ints[0]);
                int den = int.Parse(ints[1]);

                result.Add(new Rational(num, den));

            }

            return result;
        }
    }
}
