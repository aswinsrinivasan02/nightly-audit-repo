using BallyTech.Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PlatformConfigurator
{
    [Serializable]
    [XmlRoot("EventMessageConfigs")]
    public class EventMessageConfigCollection
    {
        [XmlElement("EventMessageConfig")]
        public List<EventMessageConfig> EventMessageCollection
        {
            get;
            set;
        }
    }
}