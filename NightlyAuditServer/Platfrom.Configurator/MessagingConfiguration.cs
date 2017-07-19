using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using BallyTech.Infrastructure.Messaging;

namespace PlatformConfigurator
{
    [Serializable]
    public class MessagingConfiguration : ISerializable
    {
        [XmlElement("Destination")]
        public List<Destination> Destination
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