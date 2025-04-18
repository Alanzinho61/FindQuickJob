using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindQuickJob.Application.Dtos
{
    public class CreateApplicationDto
    {
        public Guid UserId { get; set; }
        public Guid JobPostId { get; set; }
        public string? Message { get; set; }
    }
}
