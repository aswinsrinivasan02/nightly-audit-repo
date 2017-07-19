using BallyTech.Infrastructure.Data;
using SG.NightlyAudit.Domain;
using SG.NightlyAudit.Domain.Repository;
using System;

namespace SG.NightlyAudit.Persistance
{
    public class AuditHistoryRepository : Repository<AuditHistory,Int64>,IAuditHistoryRepository
    {
        public AuditHistoryRepository(BallyTech.Infrastructure.Provider.IServiceProvider serviceProvider)
            : base(serviceProvider.Data.GetStorage())
        {

        }
        public override long NextID()
        {
            throw new NotImplementedException();
        }
    }
}
