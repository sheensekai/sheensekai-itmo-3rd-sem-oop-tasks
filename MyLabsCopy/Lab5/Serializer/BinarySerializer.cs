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
    class BinarySerializer<T> : ISerializer<T>
    {

        public void Serialize(T obj, Stream stream)
        {
            new BinaryFormatter().Serialize(stream, obj);
        }
        public T Deserialize(Stream stream)
            => (T) new BinaryFormatter().Deserialize(stream);
    }
}