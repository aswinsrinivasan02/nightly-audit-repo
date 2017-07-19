using BallyTech.Infrastructure.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Configurator
{
    [Serializable]
    public class ServerHostConfig : ISerializable
    {
        [XmlElement("EndPoint")]
        public List<ServertHost> Endpoints
        {
            get;
            set;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }
    }

    [Serializable]
    public class ServertHost
    {
        [XmlAttribute("ConfigName")]
        public string ConfigName
        { get; set; }

        [XmlElement("HostConfig")]
        public HostConfig HostConfig
        {
            get;
            set;
        }
    }
}