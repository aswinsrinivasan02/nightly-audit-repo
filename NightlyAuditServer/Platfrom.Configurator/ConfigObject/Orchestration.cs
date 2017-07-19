using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using BallyTech.Infrastructure.Workflow;

namespace PlatformConfigurator
{
    [Serializable]
    public class Orchestration : ISerializable
    {
        [XmlElement("WorkflowConfiguration")]
        public List<WorkflowConfiguration> WorkflowConfigurations
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