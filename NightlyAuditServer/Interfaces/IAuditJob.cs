using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG.NightlyAudit.Interfaces
{
    public interface IAuditJob
    {
        bool Run();
    }
}
