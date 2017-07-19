using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using BallyTech.Infrastructure.Data;

namespace PlatformConfigurator
{
    [Serializable]
    [XmlRoot("TypeShardConfigurationSection")]
    public class TypeShardConfigurationSection : ISerializable
    {
        [XmlElement("TypeShardElement")]
        public List<TypeShardElement> TypeShardElement
        {
            get;
            set;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }
    }
}