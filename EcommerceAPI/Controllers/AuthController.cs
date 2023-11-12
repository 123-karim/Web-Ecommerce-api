using EcommerceAPI.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServise _authmodel;
        public AuthController(IAuthServise _authmodel)
        {
            this._authmodel = _authmodel;
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegiserModel model)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var result = await _authmodel.RegisterAsync(model);
            if (!result.IsAuthenticated == true) { return BadRequest(result.Message); }
            return Ok(result);

        }  [HttpPost]
        public async Task<IActionResult> GetUserAsync([FromBody] RequestSigningmodel model)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var result = await _authmodel.ReuestSigningAsync(model);
            if (!result.IsAuthenticated == true) { return BadRequest(result.Message); }
            return Ok(result);

        }
        [HttpPost("Addrole")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> AddingRoleToUser([FromBody] RoleModel model)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var result = await _authmodel.AddRoleToUser(model);
            if (!string.IsNullOrEmpty(result)) { return BadRequest(result); }
            return Ok(model);
        }

    }
}
