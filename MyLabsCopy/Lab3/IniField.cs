using System;
using System.Collections.Generic;
using System.Text;

namespace MyLabs.Lab3
{
    class IniField
    {
        string field_name;
        string field_value;
        public string name
        {
            get { return field_name; }
        }

        public object value
        {
            get { return field_value; }
        }

        public int ToInt()
        {

            if (int.TryParse(field_value, out int tmp))
            {
                return tmp;
            }
            else
            { 
                throw new IniFieldException("Cannot converse this field to int: " + field_value);
            }

        }

        public double ToDouble()
        {
            if (double.TryParse(field_value.Replace(".", ","), out double tmp))
            {
                return tmp;
            }
            else
            {
                throw new IniFieldException("Cannot converse this field to double: " + field_value);
            }
        }

        public new string ToString()
            => field_value;
            public IniField(string name, string fvalue)
        {
            this.field_name = name;
            this.field_value = fvalue;
        }

    }
}
