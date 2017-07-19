using BallyTech.Infrastructure.Data;
using System;
using System.Collections.Generic;

namespace SG.NightlyAudit.Domain.Repository
{
    public interface IAuditJobRepository : IRepository<AuditJob,Int32>
    {
        List<AuditJob> GetAllAuditJobs();
    }
}
