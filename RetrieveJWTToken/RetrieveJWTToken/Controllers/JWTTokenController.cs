using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RetrieveJWTToken.Models;
using RetrieveJWTToken.Services;

namespace RetrieveJWTToken.Controllers
{
    [Route("api/ucts/retrievejwttoken/")]
    [ApiController]
    public class JWTTokenController : ControllerBase
    {
        private readonly IJWTTokenService tokenService;

        public JWTTokenController(IJWTTokenService tokenService)
        {
            this.tokenService = tokenService;
        }

        [HttpPost("getJWTToken")]
        public async Task<IActionResult> getToken([FromForm] JWTTokenRequest tokenRequest)
        {
            //JWTTokenRequest request = JsonConvert.DeserializeObject<JWTTokenRequest>(keyValues);


            if (string.IsNullOrEmpty(tokenRequest.key) || string.IsNullOrEmpty(tokenRequest.id) || tokenRequest.grant_type.ToUpper() != "PASSWORD")
            {
                var errorMessage = new
                {
                    Error = "Invalid grant or unsupported type"
                };

                return BadRequest(JsonConvert.SerializeObject(errorMessage));
            }
            else
            {
                var apiResponse = await tokenService.JWTTokenRequestAsync(tokenRequest);

                if (apiResponse == null)
                    return NotFound();
                else
                    return Ok(apiResponse);
            }
        }
    }
}
