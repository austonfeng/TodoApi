using DemoProject.Service.Models;
using Domain.Microsoft_Identity.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Response = DemoProject.Service.Models.Response;

namespace DemoProject.Api.Controllers
{
    /// <summary>
    /// Handles the user login and registration
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public AccountController(
           UserManager<ApplicationUser> userManager,
           RoleManager<IdentityRole> roleManager,
           IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }


        /// <summary>
        /// logs in the user with correct username and password
        /// </summary>
        /// <param name="model"></param>
        /// <returns>a token with expiration date</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /login
        ///     {
        ///        "username": "example@example.com",
        ///        "password": "XXXXXXXXXX"
        ///     }
        ///
        /// </remarks>
        [HttpPost]
        [Route("login")]
        [Produces("application/json")]
        [ProducesResponseType(401)]
        [ProducesResponseType(200, Type = typeof(object))]
        public async Task<IActionResult> Login([FromBody] ValidateUserReq model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {

                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.UserData,user.Id),
                    new Claim(ClaimTypes.Expiration,DateTime.Now.ToString())
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    user.Email,
                    roles = userRoles
                });
            }
            return Unauthorized();
        }


        /// <summary>
        /// Creates a user with role "User"
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /register
        ///     {
        ///        "username": "example@example.com",
        ///        "email": "example@example.com",
        ///        "password": "XXXXXXXXXX"
        ///     }
        ///
        /// </remarks>
        [HttpPost]
        [Route("register")]
        [Produces("application/json")]
        [ProducesResponseType(500)]
        [ProducesResponseType(200, Type = typeof(Response))]
        public async Task<IActionResult> Register([FromBody] CreateUserReq model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            ApplicationUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Email
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (await _roleManager.RoleExistsAsync("User"))
            {
                await _userManager.AddToRoleAsync(user, "User");
            }
            else
            {
                var createdRoleResult = await _roleManager.CreateAsync(new IdentityRole { Id = "User", Name = "User", NormalizedName = "User" });
                if (createdRoleResult.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                }
            }
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });
            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }


        /// <summary>
        /// Generates a jwt token
        /// </summary>
        /// <param name="authClaims"></param>
        /// <returns></returns>
        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(15),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
