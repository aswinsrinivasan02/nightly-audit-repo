using BallyTech.Infrastructure.Types;
using SG.NightlyAudit.Domain;
using System;
using System.Data.SqlClient;

namespace SG.NightlyAudit.Persistence.MSSql.QueryBuilder
{
    public class ProductQueryBuilder : IQueryBuilder<SqlCommand, Products, Int32>
    {
        public SqlCommand Create(IQueryCriteria criteria)
        {
            SqlCommand command = new SqlCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            switch (criteria.QueryKey)
            {
                case "GetAllProducts":
                    command.CommandText = "spGetAllProducts";
                    break;
                default:
                    break;
            }

            return command;
        }

        public SqlCommand Create(int aggregateId)
        {
            throw new NotImplementedException();
        }
    }
}
