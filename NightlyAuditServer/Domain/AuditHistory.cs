using BallyTech.Infrastructure.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG.NightlyAudit.Domain
{
    [Serializable]
    public class AuditHistory : Persistable, IAggregate<Int64>
    {
        public Int64 AuditId { get; set; }

        public Int32 AuditType { get; set; }

        public DateTime AuditStartDateTime { get; set; }

        public String AuditParameters { get; set; }

        public DateTime? AuditEndDateTime { get; set; }

        public long Id
        {
            get { return AuditId; }
        }
    }
}
