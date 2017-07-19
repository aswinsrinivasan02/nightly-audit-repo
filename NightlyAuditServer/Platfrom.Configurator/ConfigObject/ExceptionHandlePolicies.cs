using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using BallyTech.Infrastructure.ExceptionHandling;

namespace PlatformConfigurator
{
    [Serializable]
    [XmlRoot("Policies")]
    public class ExceptionHandlePolicies : ISerializable
    {
        [XmlElement("ExceptionHandlePolicy")]
        public List<ExceptionHandlePolicy> ExceptionHandlePolicy
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