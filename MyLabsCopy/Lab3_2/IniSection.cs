using System;
using System.Collections.Generic;
using System.Text;

namespace MyLabs.Lab3_2
{
    class IniSection
    {
        string section_name;
        List<AIniField> fields;

        public object this[string index]
        {
            get
            { 
                foreach(AIniField field in fields)
                {
                    if (field.name == index)
                    {
                        return field.get_value();
                    }
                }

                throw new IniSectionException("Couldn't find needed field");

                return null;
            }

        }

        public string name
        {
            get { return section_name;  }
        }
                
        public void AddField(string name, string value)
        {
            AIniField field;
            if (int.TryParse(value, out int tmp2))
            {
                field = new IniField<int>(name, ValueType.Integer, tmp2);
            }

            else if (double.TryParse(value, out double tmp))
            {
                field = new IniField<double>(name, ValueType.Float, tmp);
            }

            else
            {
                field = new IniField<string>(name, ValueType.String, value);
            }
            fields.Add(field);
        }

        public void RemoveField(string name)
        {
            AIniField tmp = fields.Find(field => field.name == name);
            if (tmp == null)
            {
                throw new IniFieldException("Cound't find a field to remove");
            }

            else
            {
                fields.Remove(tmp);
            }
        }

        public IniSection(string name)
        {
            this.section_name = name;
            this.fields = new List<AIniField>();
        }

        
    }
}
