using System;
using System.Collections.Generic;
using System.Text;

namespace MyLabs.Lab3_2
{

    abstract class AIniField
    {
        public ValueType type { get; }
        public string name { get; }

        public virtual object get_value() { return null; }

        public AIniField(string name, ValueType type)
        {
            this.type = type;
            this.name = name;
        }
    }
    class IniField<T> : AIniField
    {
        private T value;
    

            public IniField(string name, ValueType type, T value)
                : base(name, type)
        {
            this.value = value;
        }

        public override object get_value()
        {
            return value;
        }

    }
}
