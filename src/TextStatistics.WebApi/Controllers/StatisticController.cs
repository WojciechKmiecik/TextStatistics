using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TextStatistics.Definition.Services;

namespace TextStatistics.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticController : ControllerBase
    {
        private readonly IStatisticsService _statisticsService;

        public StatisticController(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }
        [HttpGet("")]
        public async Task<IActionResult> Get(string guid)
        {
            if (string.IsNullOrWhiteSpace(guid))
            {
                return BadRequest();
            }
            try
            {
                var stat = await _statisticsService.GetStatistics(guid);
                return Ok(stat);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}