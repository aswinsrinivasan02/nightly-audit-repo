using BallyTech.Infrastructure.Types;
using SG.NightlyAudit.Domain;
using System.Collections.Generic;
using System.Data;

namespace SG.NightlyAudit.Persistence.MSSql.ObjectBuilder
{
    public class AuditHistoryObjectBuilder : IObjectBuilder<IDataReader, AuditHistory>
    {
        public IList<AuditHistory> Fill(IDataReader reader)
        {
            var auditHistoryList = new List<AuditHistory>();
            int indexCount = 0;

            do
            {
                switch (indexCount)
                {
                    case 0:
                        this.FillAuditHistory(reader, auditHistoryList);
                        break;

                    default:
                        break;
                }

                indexCount++;
            } while (reader.NextResult());

            return auditHistoryList;
        }

        private void FillAuditHistory(IDataReader reader, List<AuditHistory> auditHistoryList)
        {
            while (reader.Read())
            {
                AuditHistory auditHistory = new AuditHistory();
                auditHistory.AuditId = reader.GetInt64(0);
                auditHistory.AuditType = reader.GetInt32(1);
                auditHistory.AuditStartDateTime = reader.GetDateTime(2);
                auditHistory.AuditParameters = reader.GetString(3);
                auditHistoryList.Add(auditHistory);
            }
        }
    }
}
