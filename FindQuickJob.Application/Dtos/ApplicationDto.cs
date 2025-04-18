using FindQuickJob.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindQuickJob.Application.Dtos
{
    public class ApplicationDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string UserFullName { get; set; } = string.Empty;
        public Guid JobPostId { get; set; }
        public string JobPostTitle { get; set; } = string.Empty;
        public string? Message { get; set; } = string.Empty;
        public ApplicationStatus Status { get; set; }
        public DateTime AppliedAt { get; set; }
    }
}
