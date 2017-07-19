using BallyTech.Infrastructure.IDGeneration;
using BallyTech.Infrastructure.Types;
using System.Collections.Generic;
using System.Data;

namespace SG.NightlyAudit.Persistence.MSSql
{
    public partial class IDRangeObjectBuilder : IObjectBuilder<IDataReader, IDRange>
    {
        public IList<IDRange> Fill(IDataReader reader)
        {
            List<IDRange> idRanges = new List<IDRange>();

            while (reader.Read())
            {
                IDRange idRange = new IDRange(new IDRangeId(reader.GetString(0)));

                idRange.StartRange = reader.GetInt64(1);
                idRange.EndRange = reader.GetInt64(2);

                idRanges.Add(idRange);
            }

            return idRanges;
        }
    }
}
