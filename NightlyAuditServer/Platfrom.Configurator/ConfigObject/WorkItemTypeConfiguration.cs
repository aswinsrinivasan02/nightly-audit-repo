using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using BallyTech.Infrastructure.Workflow;

namespace PlatformConfigurator
{
    [Serializable]
    public class WorkItemTypeConfiguration : ISerializable
    {
        [XmlElement("WorkItemConfig")]
        public List<WorkItemConfig> WorkItemConfig
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