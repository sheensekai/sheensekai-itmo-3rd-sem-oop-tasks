using System;
using System.Collections.Generic;
using System.Text;

namespace MyLabs.Lab3_2
{
    enum ValueType
    {
        String,
        Integer,
        Float
    }

    static class TypeParser
    {
        public static ValueType ParseTypeБ(string value, out object value2)
        {

            double tmp; int tmp2;
            if (int.TryParse(value, out tmp2))
            {
                value2 = tmp2;
                return ValueType.Integer;
            }

            else if (double.TryParse(value, out tmp))
            {
                value2 = tmp;
                return ValueType.Float;
            }

            else
            {
                value2 = value;
                return ValueType.String;
            }
        }
    }
}
