using FindQuickJob.Domain.BaseClass;
using FindQuickJob.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace FindQuickJob.Domain.Entities
{
    public class Application : BaseEntity
    {

        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid JopPostId { get; set; }
        public JobPost JopPost { get; set; }

        public string? Message { get; set; }
        public ApplicationStatus Status { get; set; } = ApplicationStatus.Pending;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
