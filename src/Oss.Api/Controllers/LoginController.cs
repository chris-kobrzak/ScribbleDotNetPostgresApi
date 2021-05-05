using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Oss.Client.Database.Repositories;
using Oss.Core.Models;

namespace Oss.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly AuthConfig _authConfig;
        private readonly IUserRepository _userRepository;
        private readonly ITokenBuilder _tokenBuilder;

        public LoginController(
            ILogger<LoginController> logger,
            IOptions<AuthConfig> authConfig,
            IUserRepository userRepository,
            ITokenBuilder tokenBuilder
            )
        {
            _logger = logger;
            _authConfig = authConfig.Value;
            _userRepository = userRepository;
            _tokenBuilder = tokenBuilder;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<String> Login([FromBody] LoginModel model)
        {
            var user = await _userRepository.GetByCredentials(model.Login, model.Password);

            if (user == null)
            {
                return null;
            }

            return _tokenBuilder.Build(user, _authConfig.Secret);
        }
    }
}
