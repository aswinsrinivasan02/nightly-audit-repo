using BallyTech.Infrastructure.IDGeneration;
using BallyTech.Infrastructure.Types;
using System;
using System.Data;
using System.Data.SqlClient;

namespace SG.NightlyAudit.Persistence.MSSql
{
    public partial class IDRangeQueryBuilder : IQueryBuilder<SqlCommand, IDRange, IDRangeId>
    {
        public SqlCommand Create(IQueryCriteria criteia)
        {
            throw new NotImplementedException();
        }

        public SqlCommand Create(IDRangeId aggregateId)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "spGetNumberMaster";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@NumberCode", aggregateId.Key));

            return command;
        }
    }
}
