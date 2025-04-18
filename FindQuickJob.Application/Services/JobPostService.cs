using AutoMapper;
using FindQuickJob.Application.Dtos;
using FindQuickJob.Domain.Entities;
using FindQuickJob.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindQuickJob.Application.Services
{
    public class JobPostService : IJobPostService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public JobPostService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task CreateJobPostAsync (CreateJobPostDto jobPostDto)
        {
            var jobPost=_mapper.Map<JobPost>(jobPostDto);
            await _context.JopPosts.AddAsync(jobPost);
            _context.SaveChanges();
        }

        public async Task DeleteJobPostAsync (Guid id)
        {
            var jobPost = await _context.JopPosts.FindAsync(id);
            if(jobPost == null) throw new Exception("JobPost Bulunmadi");
            _context.Remove(jobPost);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<JobPostDto>> GetAllPostAsync()
        {
            var jobPost = await _context.JopPosts.Include(
                j=>j.CreatedByUser)
                .ToListAsync();
            return _mapper.Map<IEnumerable<JobPostDto>>(jobPost);
        }

        public async Task<JobPostDto> GetJobPostByIdAsync(Guid Id)
        {
            var JobPostById = await _context.JopPosts.Include
                (j=>j.CreatedByUser)
                .FirstOrDefaultAsync(j=>j.Id == Id);
            if (JobPostById == null) throw new Exception("JobPost Not Found");
            return _mapper.Map<JobPostDto>(JobPostById);
        }

        public async Task UpdateJobPostAsync(Guid id, CreateJobPostDto createJobPostDto)
        {
            var UserById = await _context.JopPosts.FindAsync(id);
            if (UserById == null) throw new Exception("JobPost Not Found");
            _mapper.Map(createJobPostDto, UserById);
            _context.JopPosts.Update(UserById);
            await _context.SaveChangesAsync();
        }
    }
}
