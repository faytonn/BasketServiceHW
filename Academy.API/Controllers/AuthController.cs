using Academy.AuthenticationService.Model;
using Core.Security.Jwt.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Academy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtAuthService _jwtAuthService;

        public AuthController(IJwtAuthService jwtAuthService)
        {
            _jwtAuthService = jwtAuthService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody]JwtTokenRequestModel model)
        {
            var tokenResponseModel = await _jwtAuthService.CreateToken(model);

            return Ok(tokenResponseModel);
        }
    }
}
