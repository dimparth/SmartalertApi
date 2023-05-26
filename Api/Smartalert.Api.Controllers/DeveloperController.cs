using Microsoft.AspNetCore.Mvc;
using Smartalert.Api.Interfaces;

namespace Smartalert.Api.Controllers
{
    [Route("api/dev")]
    [ApiController]
    public class DeveloperController : ControllerBase
    {
        private readonly IDeveloperService _developerService;
        public DeveloperController(IDeveloperService developerService)
        {
            _developerService = developerService;
        }
        [HttpPost("addusers")]
        public async Task<IActionResult> AddUsers()
        {
            return Ok(await _developerService.AddUsers());
        }
        [HttpPost("addincidents")]
        public async Task<IActionResult> AddIncidents()
        {
            return Ok(await _developerService.AddCriticals());
        }
    }
}
