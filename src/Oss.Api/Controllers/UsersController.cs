using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Oss.Client.Database.Repositories;

namespace Oss.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<UsersController> _logger;
        private readonly IUserRepository userRepository;

        public UsersController(
            ILogger<UsersController> logger,
            IUserRepository userRepository
            )
        {
            _logger = logger;
            this.userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Dictionary<string, object>>> Get()
        {
            var rng = new Random();

            return await userRepository.GetAllActive();

            // return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            // {
            //     Date = DateTime.Now.AddDays(index),
            //     TemperatureC = rng.Next(-20, 55),
            //     Summary = Summaries[rng.Next(Summaries.Length)]
            // })
            // .ToArray();
        }
    }
}
