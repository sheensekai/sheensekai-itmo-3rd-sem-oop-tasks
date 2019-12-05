using System;
using System.Collections.Generic;
using System.Text;

namespace MyLabs.Lab3
{
    class IniParserException : Exception
    {
        public IniParserException()
        : base()
        { }
        public IniParserException(string message)
            : base(message)
        { }
    }

    class IniSectionException : IniParserException
    {
        public IniSectionException()
        : base()
        { }
        public IniSectionException(string message)
            : base(message)
        { }
    }

    class IniFieldException : IniParserException
    {
        public IniFieldException()
                : base()
        { }
        public IniFieldException(string message)
            : base(message)
        { }
    }
}
