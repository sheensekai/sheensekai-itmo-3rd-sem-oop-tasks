using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace MyLabsCopy.Lab5.Serializer
{
    class XMLSerializer<T> : ISerializer<T>
    {
        public void Serialize(T obj, Stream stream)
        {
            new XmlSerializer(typeof(T)).Serialize(stream, obj);
        }

        public T Deserialize(Stream stream)
            => (T)new XmlSerializer(typeof(T)).Deserialize(stream);
    }
}
