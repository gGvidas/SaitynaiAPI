using System.Diagnostics;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SaitynaiAPI.DTOs;
using SaitynaiAPI.DTOs.UserDTOs;
using SaitynaiAPI.Entities;
using SaitynaiAPI.Repositories.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SaitynaiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly SaitynaiDbContext _context;
        private readonly IUserRepository _userRepository;
        private IConfiguration _config;

        public UserController(SaitynaiDbContext context, IUserRepository userRepository, IConfiguration config)
        {
            _context = context;
            _userRepository = userRepository;
            _config = config;
        }
        
        /// <summary>
        /// Logins an user
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/user/login
        ///     {
        ///         "email": "user1@user.com",
        ///         "password": "user1"
        ///     }
        /// </remarks>
        /// <param name="request"></param>
        /// <returns>Access token and a refresh token</returns>
        /// <response code="200">Returns an access token and a refresh token</response>
        /// <response code="401">If login credentials were wrong</response>
        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            User user = await _userRepository.GetUser(request);

            if (user != null)
            {
                user.RefreshToken = GenerateRefreshToken();
                _userRepository.Update(user);
                await _context.SaveChangesAsync();
                var response = new LoginResponse() { accessToken = GenerateJSONWebToken(user), refreshToken = user.RefreshToken };
                return Ok(response);
            }
            else return Unauthorized();
        }

        /// <summary>
        /// Registers an user
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/user/register
        ///     {
        ///         "email": "user1@user.com",
        ///         "password": "user1"
        ///     }
        /// </remarks>
        /// <param name="request"></param>
        /// <returns>Access token and a refresh token</returns>
        /// <response code="201">Returns an access token and a refresh token</response>
        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> Register([FromBody] LoginRequest request)
        {
            User user = ConvertFromDTO.FromCreateRequest(request);
            user.RefreshToken = GenerateRefreshToken();
            _userRepository.Create(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Login), new LoginResponse() { accessToken = GenerateJSONWebToken(user), refreshToken = user.RefreshToken });
        }

        /// <summary>
        /// Logouts an user
        /// </summary>
        /// <returns>No content</returns>
        /// <response code="204">Returns no content</response>
        [Authorize]
        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task Logout()
        {
            Claim emailClaim = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Email);
            if (emailClaim != null)
            {
                string email = emailClaim.Value;
                User user = await _userRepository.GetUser(email);

                if (user != null)
                {
                    user.RefreshToken = null;
                    _userRepository.Update(user);
                    await _context.SaveChangesAsync();
                }
            }
        }

        /// <summary>
        /// Refreshes the access token
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/user/refresh
        ///     {
        ///         "email": "user1@user.com",
        ///         "refreshToken": "abc"
        ///     }
        /// </remarks>
        /// <param name="request"></param>
        /// <returns>Access token and a refresh token</returns>
        /// <response code="200">Returns an access token and a refresh token</response>
        /// <response code="401">If refresh token and/or email was wrong</response>
        [HttpPost("refresh")]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequest request)
        {
            User user = await _userRepository.GetUser(request.Email);
            if (user.RefreshToken == request.RefreshToken)
            {
                user.RefreshToken = GenerateRefreshToken();
                _userRepository.Update(user);
                await _context.SaveChangesAsync();
                return Ok(new LoginResponse { accessToken = GenerateJSONWebToken(user), refreshToken = user.RefreshToken });
            }
            else return Unauthorized();
        }

        private string GenerateJSONWebToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("Admin", user.isAdmin.ToString()),
                new Claim("Id", user.Id.ToString()),
                new Claim("Email", user.Email)
            };

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
