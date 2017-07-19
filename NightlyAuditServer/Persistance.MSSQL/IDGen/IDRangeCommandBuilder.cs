using BallyTech.Infrastructure.IDGeneration;
using BallyTech.Infrastructure.Types;
using System;
using System.Data.SqlClient;

namespace SG.NightlyAudit.Persistence.MSSql
{
    public partial class IDRangeObjectBuilder : ICommandBuilder<IDRange, SqlCommand>
    {
        private const short IDRangeRank = 4;

        public short Rank
        {
            get { return IDRangeObjectBuilder.IDRangeRank; }
        }

        public SqlCommand CreateDeleteCommand(IDRange aggregate)
        {
            throw new NotImplementedException();
        }

        public SqlCommand CreateInsertCommand(IDRange aggregate)
        {
            throw new NotImplementedException();
        }

        public SqlCommand CreateUpdateCommand(IDRange aggregate)
        {
            throw new NotImplementedException();
        }
    }
}
