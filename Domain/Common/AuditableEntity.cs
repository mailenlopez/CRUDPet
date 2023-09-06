using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public abstract class AuditableEntity
    {
        public DateTime DateCreated { get; protected set; } = DateTime.Now;

        public string CreatedBy { get; set; } = string.Empty;

        public DateTime? LastModified { get; set; }

        public string LastModifiedBy { get; set; } = string.Empty;
    }
}
