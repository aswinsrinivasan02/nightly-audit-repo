using BallyTech.Infrastructure.Data;
using BallyTech.Infrastructure.ExternalData;
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
    [XmlRoot("ExternalStorageProviderConfig")]
    public class ExternalStorageProviderConfiguration
    {
        [XmlElement("Provider")]
        public List<ExternalStorageProviderConfig> Providers
        {
            get;
            set;
        }
    }
}
