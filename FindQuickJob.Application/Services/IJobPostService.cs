using FindQuickJob.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindQuickJob.Application.Services
{
    public interface IJobPostService
    {
        Task<IEnumerable<JobPostDto>> GetAllPostAsync();
        Task<JobPostDto> GetJobPostByIdAsync (Guid Id);
        Task CreateJobPostAsync (CreateJobPostDto jobPostDto);
        Task UpdateJobPostAsync (Guid id, CreateJobPostDto createJobPostDto);
        Task DeleteJobPostAsync (Guid id);
    }
}
