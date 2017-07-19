using BallyTech.Infrastructure.Data.Caching;
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
    [XmlRoot("CacheConfiguration")]
    public class CacheConfigCollection
    {
        #region Public Properties

        [XmlElement("CacheConfig")]
        public List<CacheConfig> CacheCollection
        {
            get;
            set;
        }

        #endregion Public Properties
    }
}