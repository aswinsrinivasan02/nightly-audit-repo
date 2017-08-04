using System;
using SG.NightlyAudit.Domain.Repository;
using SG.NightlyAudit.Domain;
using BallyTech.Infrastructure.Data;

namespace SG.NightlyAudit.Persistance
{
    public class JobRepository : Repository<Job, Int64>, IJobRepository
    {
        public JobRepository(BallyTech.Infrastructure.Provider.IServiceProvider serviceProvider)
            : base(serviceProvider.Data.GetStorage())
        {

        }

        public override long NextID()
        {
            throw new NotImplementedException();
        }
    }
}
