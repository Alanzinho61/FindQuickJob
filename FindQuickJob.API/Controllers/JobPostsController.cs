using FindQuickJob.Application.Dtos;
using FindQuickJob.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FindQuickJob.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobPostsController : ControllerBase
    {
        private readonly IJobPostService _jobPostService;
        public JobPostsController(IJobPostService jobPostService)
        {
            _jobPostService = jobPostService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var jobPosts = await _jobPostService.GetAllPostAsync();
            return Ok(jobPosts);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var jobPost= await _jobPostService.GetJobPostByIdAsync(id);
            return Ok(jobPost);    
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateJobPostDto createJobPostDto)
        {
            await _jobPostService.CreateJobPostAsync(createJobPostDto);
            return Ok();
        }

    }
}
