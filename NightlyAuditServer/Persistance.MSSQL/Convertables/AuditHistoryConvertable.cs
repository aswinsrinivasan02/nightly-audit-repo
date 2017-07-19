using BallyTech.Infrastructure.Types;
using SG.NightlyAudit.Domain;
using System.Collections.Generic;

namespace SG.NightlyAudit.Persistence.MSSql.Convertables
{
    public class AuditHistoryConvertable : IConvertable<AuditHistory>
    {
        public System.Collections.Generic.IEnumerable<IPersistable> ToPersistables(AuditHistory aggregate)
        {
            List<AuditHistory> auditHistory = new List<AuditHistory>() { aggregate };
            return auditHistory;
        }
    }
}
