using FindQuickJob.Application.Dtos;
using FindQuickJob.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FindQuickJob.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationsController : ControllerBase
    {
        private readonly IApplicationService _applicationService;
        public ApplicationsController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllApplications()
        {
            var applications = await _applicationService.GetAllApplicationsAsync();
            return Ok(applications);
        }

        [HttpGet("jobpost/{jobPostId}")]
        public async Task<IActionResult> GetApplicationById(Guid JobPostId)
        {
            var application = await _applicationService.GetApplicationByIdAsync(JobPostId);
            if (application == null) return NotFound();
            return Ok(application);
        }
        [HttpPost]
        public async Task<IActionResult> CreateApplication([FromBody] CreateApplicationDto createApplicationDto)
        {
            if (createApplicationDto == null) return BadRequest("Invalid application data");
            var application = await _applicationService.CreateApplicationAsync(createApplicationDto);
            //return CreatedAtAction(nameof(GetApplicationById), new { id = application.Id }, application);
            return Created("", application);
        }
    }
}
