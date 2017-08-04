using BallyTech.Infrastructure.Types;
using SG.NightlyAudit.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG.NightlyAudit.Persistence.MSSql.QueryBuilder
{
    public class AuditJobQueryBuilder : IQueryBuilder<SqlCommand, AuditJob, Int32>
    {
        public SqlCommand Create(IQueryCriteria criteria)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            switch (criteria.QueryKey)
            {
                case "GetAllAuditJobs":
                default:
                    sqlCommand.CommandText = "spGetAudits";
                    break;
            }
            return sqlCommand;
        }

        public SqlCommand Create(int aggregateId)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "spGetAuditsById";
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("@AuditId", aggregateId));
            return sqlCommand;
        }
    }
}
