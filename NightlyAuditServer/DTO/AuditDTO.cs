using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SG.NightlyAudit.DTO
{
    [DataContract]
    public class AuditDTO
    {
        [DataMember]
        public int AuditId { get; set; }

        [DataMember]
        public String AuditName { get; set; }

        [DataMember]
        public bool IsEnabled { get; set; }

        [DataMember]
        public List<AuditParametersDTO> AuditParameters { get; set; }

        public AuditDTO()
        {
            this.AuditParameters = new List<AuditParametersDTO>();
        }
    }
    
    [DataContract]
    public class AuditParametersDTO
    {
        [DataMember]
        public Int32 ParameterId { get; private set; }

        [DataMember]
        public String ParameterName { get; private set; }

        [DataMember]
        public String ParameterType { get; private set; }

        [DataMember]
        public List<String> ParameterValues { get; set; }

        public AuditParametersDTO(int parameterId, string parameterType, string parameterName)
        {
            this.ParameterName = parameterName;
            this.ParameterId = parameterId;
            this.ParameterType = parameterType;
            this.ParameterValues = new List<string>();
        }

        public void AddParameterVal(string val)
        {
            this.ParameterValues.Add(val);
        }

    }
}
