using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace MyLabsCopy.Lab5.Serializer
{
    interface ISerializer<T>
    {
        void Serialize(T obj, Stream stream);
        T Deserialize(Stream stream);
    }
}
