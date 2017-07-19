using BallyTech.Infrastructure.Types;
using System;
using System.Collections.Generic;

namespace SG.NightlyAudit.Domain
{
    [Serializable]
    public class AuditJob : Persistable, IAggregate<Int32>
    {
        public Int32 AuditId { get; set; }

        public String AuditName { get; set; }

        public bool IsEnabled { get; set; }

        public List<AuditParameters> AuditParameters { get; set; }

        public int Id
        {
            get { return AuditId; }
        }

        public AuditJob()
        {
            this.AuditParameters = new List<AuditParameters>();
        }
    }

    public class AuditParameters
    {
        public Int32 ParameterId { get; private set; }

        public String ParameterName { get; private set; }

        public String ParameterType { get; private set; }

        public List<String> ParameterValues { get; private set; }

        public AuditParameters(int parameterId, string parameterType, string parameterName)
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
