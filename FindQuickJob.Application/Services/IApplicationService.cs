using FindQuickJob.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindQuickJob.Application.Services
{
    public interface IApplicationService
    {
        Task<ApplicationDto> CreateApplicationAsync(CreateApplicationDto createApplicationDto);
        //Task<bool> UpdateApplicationAsync(ApplicationDto applicationDto);
        //Task<bool> DeleteApplicationAsync(int id);
        Task<ApplicationDto> GetApplicationByIdAsync(Guid JobPostId);
        Task<IEnumerable<ApplicationDto>> GetAllApplicationsAsync();
    }
}
