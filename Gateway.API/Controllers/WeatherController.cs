using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Sunny", "Cloudy", "Rainy", "Stormy", "Snowy"
        };

        [HttpGet("today")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Get()
        {

            var name = "";
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                var claim = claims.FirstOrDefault();
                if (claim is not null)
                {
                    name = claim?.Value.ToString();
                }
            }

            var rng = new Random();
            var result = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-10, 35),
                Summary = "This user's name is " + name,
            });

            return Ok(result);
        }

        [HttpGet("weather")]
        public async Task<IActionResult> GetWeatherE()
        {
            // Simulate async delay (e.g., calling a weather API)
            await Task.Delay(500);

            var weather = new
            {
                Temperature = "22°C",
                Condition = "Partly Cloudy",
                Location = "Toronto",
                Time = DateTime.UtcNow
            };

            return Ok(weather);
        }

        [HttpGet("weather2")]
        public async Task<IActionResult> GetWeather2()
        {
            // Simulate async delay (e.g., calling a weather API)
            await Task.Delay(500);
            return BadRequest("you suck");
        }
    }



    public class WeatherForecast
    {
        public DateTime Date { get; set; }
        public int TemperatureC { get; set; }
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        public string? Summary { get; set; }
    }
}
