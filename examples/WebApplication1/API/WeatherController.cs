using Microsoft.AspNetCore.Mvc;
using Stower;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Domain;

namespace WebApplication1.API
{

    [ApiController]
    [Route("weather")]
    public class WeatherController : ControllerBase    
    {
        private readonly IStower _stower;

        public WeatherController(IStower stower)
        {
            _stower = stower;
        }
        [HttpPost]
        public async Task<IActionResult> AddNew(WeatherData item)
        {
            await _stower.Add<WeatherData>(item);
            return Ok();
        }
    }
}
