using BallyTech.Infrastructure.Types;
using SG.NightlyAudit.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG.NightlyAudit.Persistence.MSSql.ObjectBuilder
{
    public class AuditJobObjectBuilder : IObjectBuilder<IDataReader, AuditJob>
    {
        public IList<AuditJob> Fill(IDataReader reader)
        {
            List<AuditJob> auditJobs = new List<AuditJob>();

            int indexCount = 0;
            do
            {
                switch (indexCount)
                {
                    case 0:
                        this.FillAuditJobData(reader, auditJobs);
                        break;
                    case 1:
                        this.FillAuditParameters(reader, auditJobs);
                        break;
                    case 2:
                        this.FillParameterValues(reader, auditJobs[0]);
                        break;
                    default:
                        break;
                }

                indexCount++;
            } while (reader.NextResult());

            return auditJobs;
        }

        private void FillParameterValues(IDataReader reader, AuditJob auditJob)
        {
            while (reader.Read())
            {
                AuditParameters auditParameter = auditJob.AuditParameters.Where(o => o.ParameterId == reader.GetInt32(0)).FirstOrDefault();
                if (auditParameter != null)
                {
                    auditParameter.AddParameterVal(reader.GetString(1));
                }
                else
                {
                    throw new Exception("Invalid ParameterValue Configured");
                }
            }
        }

        private void FillAuditParameters(IDataReader reader, List<AuditJob> auditJobs)
        {
            while (reader.Read())
            {
                AuditParameters auditParameters = new AuditParameters(reader.GetInt32(1), reader.GetString(2), reader.GetString(3));
                int auditId = reader.GetInt32(0);
                AuditJob auditJob = auditJobs.Where(o => o.AuditId == auditId).FirstOrDefault();
                if (auditJob != null)
                {
                    auditJob.AuditParameters.Add(auditParameters);
                }
                else
                {
                    throw new Exception("Invalid Parameter Configured");
                }
            }
        }

        private void FillAuditJobData(IDataReader reader, List<AuditJob> auditJobs)
        {
            while (reader.Read())
            {
                AuditJob auditJob = new AuditJob();
                auditJob.AuditId = reader.GetInt32(0);
                auditJob.AuditName = reader.GetString(1);
                auditJob.IsEnabled = reader.GetBoolean(2);
                auditJobs.Add(auditJob);
            }
        }
    }
}
