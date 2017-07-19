using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BallyTech.Infrastructure.Types;
using SG.NightlyAudit.Domain;
using System.Data.SqlClient;

namespace SG.NightlyAudit.Persistence.MSSql.CommandBuilder
{
    public class ProductsCommandBuilder : ICommandBuilder<Products,SqlCommand>
    {
        public SqlCommand CreateDeleteCommand(Products aggregate)
        {
            SqlCommand command = new SqlCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "spDeleteProducts";
            command.Parameters.Add(new SqlParameter("@ProductId", aggregate.ProductId));
            return command;
        }

        public SqlCommand CreateInsertCommand(Products aggregate)
        {
            SqlCommand command = new SqlCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "spInsertProducts";
            command.Parameters.Add(new SqlParameter("@ProductId", aggregate.ProductId));
            command.Parameters.Add(new SqlParameter("@ProductCode", aggregate.ProductCode));
            command.Parameters.Add(new SqlParameter("@ProductType", aggregate.ProductType));
            command.Parameters.Add(new SqlParameter("@IPInfo", aggregate.IPInfo));
            return command;
        }

        public SqlCommand CreateUpdateCommand(Products aggregate)
        {
            SqlCommand command = new SqlCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "spUpdateProducts";
            command.Parameters.Add(new SqlParameter("@ProductId", aggregate.ProductId));
            command.Parameters.Add(new SqlParameter("@ProductCode", aggregate.ProductCode));
            command.Parameters.Add(new SqlParameter("@ProductType", aggregate.ProductType));
            command.Parameters.Add(new SqlParameter("@IPInfo", aggregate.IPInfo));
            return command;
        }

        public short Rank
        {
            get { return 1; }
        }
    }
}
