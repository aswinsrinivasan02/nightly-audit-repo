using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SG.NightlyAudit.Utilities
{
    [Serializable]
    public class OperationSecurityConfiguration : ISerializable
    {
        [XmlElement("OperationSecurity")]
        public List<OperationSecurity> OperationSecurity
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
    [XmlRoot("OperationSecurityConfiguration")]
    public class OperationSecurity : ISerializable
    {
        public OperationSecurity()
        {
        }

        public OperationSecurity(SerializationInfo info, StreamingContext context)
        {
            this.APIName = (string)info.GetValue("APIName", typeof(string));
            this.TaskId = (int)info.GetValue("TaskId", typeof(int));
            this.OperationId = (int)info.GetValue("OperationId", typeof(int));
            this.IsSessionRequired = (bool)info.GetValue("IsSessionRequired", typeof(bool));
        }

        public OperationSecurity(string APIName, int TaskId, int OperationId, bool IsSessionRequired)
        {
            this.APIName = APIName;
            this.TaskId = TaskId;
            this.OperationId = OperationId;
            this.IsSessionRequired = IsSessionRequired;
        }

        [XmlAttribute]
        public string APIName { get; set; }

        [XmlAttribute]
        public int TaskId { get; set; }

        [XmlAttribute]
        public int OperationId { get; set; }

        [XmlAttribute]
        public bool IsSessionRequired { get; set; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("APIName", this.APIName, typeof(string));
            info.AddValue("TaskId", this.TaskId, typeof(int));
            info.AddValue("OperationId", this.OperationId, typeof(int));
            info.AddValue("IsSessionRequired", this.IsSessionRequired, typeof(bool));
        }
    }
}
