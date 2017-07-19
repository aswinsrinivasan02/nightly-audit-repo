using BallyTech.Infrastructure.Data;
using SG.NightlyAudit.Domain;
using System;

namespace SG.NightlyAudit.Domain.Repository
{
    public interface IAuditHistoryRepository : IRepository<AuditHistory,Int64>
    {

    }
}
