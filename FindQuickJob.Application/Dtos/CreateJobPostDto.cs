using FindQuickJob.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindQuickJob.Application.Dtos
{
    public class CreateJobPostDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Location  { get; set; } =string.Empty;
        public WorkType WorkType { get; set; } = WorkType.OnSite;
        public JopType JopType { get; set; } = JopType.Daily;
        public Guid CreatedByUserId { get; set; }

    }
}
