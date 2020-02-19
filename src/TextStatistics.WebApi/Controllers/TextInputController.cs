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
    public class TextInputController : ControllerBase
    {
        public TextInputController(IStatisticsService service)
        {
            _service = service;
        }

        public IStatisticsService _service { get; }

        // for simplicity I accept string. Input method (demanding) wasnt mentioned in the Specification
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(string paragraph)
        {
            if (string.IsNullOrWhiteSpace(paragraph))
            {
                return BadRequest("Please supply text");
            }

            try
            {
                var guid = await _service.GenerateNewStatistics(paragraph);
                return Ok(guid);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

    }
}
