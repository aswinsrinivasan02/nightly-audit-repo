using BallyTech.Infrastructure.Data;
using BallyTech.Infrastructure.Types;
using SG.NightlyAudit.Domain;
using SG.NightlyAudit.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG.NightlyAudit.Persistance
{
    public class AuditJobRepository : Repository<AuditJob, Int32>, IAuditJobRepository
    {
        BallyTech.Infrastructure.Provider.IServiceProvider provider;

        public AuditJobRepository(BallyTech.Infrastructure.Provider.IServiceProvider provider)
            : base(provider.Data.GetStorage())
        {
            this.provider = provider;
        }

        public override int NextID()
        {
            throw new NotImplementedException();
        }

        public List<AuditJob> GetAllAuditJobs()
        {
            QueryCriteria queryCriteria = new QueryCriteria();
            queryCriteria.QueryKey = "GetAllAuditJobs";
            return base.FindAll(queryCriteria).ToList();
        }
    }
}
