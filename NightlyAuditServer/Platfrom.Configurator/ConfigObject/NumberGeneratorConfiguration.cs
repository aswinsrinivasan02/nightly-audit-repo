using BallyTech.Infrastructure.ExternalData.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PlatformConfigurator
{
    [Serializable]
    [XmlRoot("NumberGeneratorConfiguration")]
    public class NumberGeneratorConfiguration
    {
        [XmlElement("NumberGenerator")]
        public List<NumberGeneratorConfig> NumberGeneratorConfigurations
        {
            get;
            set;
        }
    }
}
