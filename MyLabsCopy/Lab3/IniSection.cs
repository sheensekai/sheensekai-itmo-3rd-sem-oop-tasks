using System;
using System.Collections.Generic;
using System.Text;

namespace MyLabs.Lab3
{
    class IniSection
    {
        string section_name;
        List<IniField> fields;

        public IniField this[string index]
        {
            get
            { 
                foreach(IniField field in fields)
                {
                    if (field.name == index)
                    {
                        return field;
                    }
                }

                return null;
            }

        }

        public string name
        {
            get { return section_name;  }
        }
                
        public void AddField(string name, string value)
        {
            fields.Add(new IniField(name, value));
        }

        public void RemoveField(string name)
        {
            IniField tmp = fields.Find(field => field.name == name);
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
            this.fields = new List<IniField>();
        }

        
    }
}
