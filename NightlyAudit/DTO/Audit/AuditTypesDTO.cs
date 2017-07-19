using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Audit
{

    public class AuditTypesDTO
    {
        public int AuditId { get; set; }
        public string AuditName { get; set; }
        public bool IsEnabled { get; set; }
    }
}
