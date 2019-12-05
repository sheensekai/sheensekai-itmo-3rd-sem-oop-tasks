using System;
using System.Collections.Generic;
using System.Text;

namespace MyLabs.Lab3
{
    class IniFile
    {
        private Dictionary<string, IniSection> sections;

        public IniFile()
        {
            this.sections = new Dictionary<string, IniSection>();
        }

        public IniSection this[string index]
        {
            get
            {
                IniSection tmp = null;
                sections.TryGetValue(index, out tmp);
                if (tmp == null)
                {
                    throw new IniSectionException("Couldn't find needed section");
                }
                return tmp;
            }
        }

        public void Add(IniSection section)
        {
            IniSection tmp;
            sections.TryGetValue(section.name, out tmp);
            if (tmp != null)
            {
                throw new IniParserException("Repeating section in ini file with name: " + section.name);
            }
            else
            {
                sections.Add(section.name, section);
            }
        }
    }
}
