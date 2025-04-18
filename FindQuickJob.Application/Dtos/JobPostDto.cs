using FindQuickJob.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindQuickJob.Application.Dtos
{
    public class JobPostDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Location { get; set; } = string.Empty;
        public WorkType WorkType { get; set; }
        public JopType JopType { get; set; }
        public Guid CreatedByUserId { get; set; }
        public string CreatedByUserFullName { get; set; } = string.Empty;
        public ICollection<ApplicationDto> Applications { get; set; } = new List<ApplicationDto>();

    }
}
