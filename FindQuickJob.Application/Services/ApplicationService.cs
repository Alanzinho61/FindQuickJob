using FindQuickJob.Infrastructure.Data;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindQuickJob.Application.Dtos;
using FindQuickJob.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace FindQuickJob.Application.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ApplicationService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;  
        }
        public async Task<ApplicationDto> CreateApplicationAsync(CreateApplicationDto createApplicationDto)
        {
            var application = new Domain.Entities.Application
            {
                UserId = createApplicationDto.UserId,
                JopPostId = createApplicationDto.JobPostId,
                Message = createApplicationDto.Message
            };
            _context.Applications.AddAsync(application);
            await _context.SaveChangesAsync();

            return  _mapper.Map<ApplicationDto>(application);
        }
        public async Task<IEnumerable<ApplicationDto>> GetAllApplicationsAsync()
        {
            var applications = await _context.Applications
                .Include(a => a.User)
                .Include(a => a.JopPost)
                .ToListAsync();
            return _mapper.Map<IEnumerable<ApplicationDto>>(applications);
        }
        public async Task<ApplicationDto> GetApplicationByIdAsync(Guid JobPostId)
        {
            var application = await _context.Applications
                .Include(a => a.User)
                .Include(a => a.JopPost)
                .FirstOrDefaultAsync(a => a.Id == JobPostId);
            if (application == null) throw new Exception("Application Not Found");
            return _mapper.Map<ApplicationDto>(application);
        }
    }
    
}
