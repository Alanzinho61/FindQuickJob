using FindQuickJob.Domain.BaseClass;
using FindQuickJob.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindQuickJob.Domain.Entities
{
    public class JobPost : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Location { get; set; } = string.Empty;

        public WorkType WorkType { get; set; } = WorkType.OnSite;
        public JopType JopType { get; set; } = JopType.Daily;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid CreatedByUserId { get; set; }
        public User CreatedByUser { get; set; } = null;

        public ICollection<Application> Applications { get; set; } =new List<Application>();
    }
}
