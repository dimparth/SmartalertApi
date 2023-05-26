using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Smartalert.Api.Datacontracts;
using Smartalert.Api.Interfaces;


namespace Smartalert.Api.Controllers
{
    [Authorize]
    [Route("api/danger")]
    [ApiController]
    public class DangerController : ControllerBase
    {
        private readonly IDangerService _dangerService;

        public DangerController(IDangerService dangerService)
        {
            _dangerService = dangerService;
        }
        [HttpPost]
        public async Task<IActionResult> AddDanger([FromForm] DangerResponse dangerInformation)
        {
            return Ok(await _dangerService.Add(dangerInformation));
        }
        [HttpGet]
        public async Task<ActionResult<DangerListResponse>> GetAllDangers()
        {
            return Ok(await _dangerService.GetAll());
        }
        [HttpGet("critical")]
        public async Task<ActionResult<IList<HighAlertIncidentResponse>>> GetAllCriticals()
        {
            return Ok(await _dangerService.GetCriticalSitutations());
        }
        [HttpGet("statistics")]
        public async Task<ActionResult<StatisticsResponse>> GetStatistics()
        {
            return Ok(await _dangerService.IncidentsCategoryResponse());
        }
    }
}
