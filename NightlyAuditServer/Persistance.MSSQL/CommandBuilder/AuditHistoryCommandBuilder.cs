using BallyTech.Infrastructure.Types;
using SG.NightlyAudit.Domain;
using System;
using System.Data.SqlClient;

namespace SG.NightlyAudit.Persistence.MSSql.CommandBuilder
{
    public class AuditHistoryCommandBuilder : ICommandBuilder<AuditHistory,SqlCommand>
    {
        public SqlCommand CreateDeleteCommand(AuditHistory aggregate)
        {
            throw new NotImplementedException();
        }

        public SqlCommand CreateInsertCommand(AuditHistory aggregate)
        {
            throw new NotImplementedException();
        }

        public SqlCommand CreateUpdateCommand(AuditHistory aggregate)
        {
            throw new NotImplementedException();
        }

        public short Rank
        {
            get { throw new NotImplementedException(); }
        }
    }
}
