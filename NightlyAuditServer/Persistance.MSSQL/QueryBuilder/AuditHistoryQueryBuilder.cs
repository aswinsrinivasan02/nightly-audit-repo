using BallyTech.Infrastructure.Types;
using SG.NightlyAudit.Domain;
using System;
using System.Data;
using System.Data.SqlClient;

namespace SG.NightlyAudit.Persistence.MSSql.QueryBuilder
{
    public class AuditHistoryQueryBuilder : IQueryBuilder<SqlCommand, AuditHistory, Int64>
    {
        public SqlCommand Create(IQueryCriteria criteria)
        {
            throw new NotImplementedException();
        }

        public SqlCommand Create(long aggregateId)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "spSelectAuditHistoryById";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("@AuditId", aggregateId));
            return sqlCommand;
        }
    }
}
