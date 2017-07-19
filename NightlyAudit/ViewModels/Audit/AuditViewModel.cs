using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Audit
{
    public class AuditViewModel
    {
        public List<AuditType> AuditTypes { get; set; }
    }

    public class AuditType
    {
        public int AuditId { get; set; }
        public string AuditName { get; set; }
    }
}
