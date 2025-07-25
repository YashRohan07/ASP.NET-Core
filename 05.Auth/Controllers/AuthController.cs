using Microsoft.AspNetCore.Identity;           // For UserManager and SignInManager
using Microsoft.AspNetCore.Mvc;                // For API Controller and routing
using Microsoft.Extensions.Configuration;      // For accessing appsettings.json
using Microsoft.IdentityModel.Tokens;          // For JWT building
using System;
using System.IdentityModel.Tokens.Jwt;         // For JWT generation
using System.Security.Claims;                  // For adding claims to token
using System.Text;                             // For Encoding
using System.Threading.Tasks;
using UserManagement.Models;                   // For ApplicationUser
using UserManagement.DTOs;                     // For RegisterDto

namespace UserManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]   // Route pattern: api/auth
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;   // To manage users
        private readonly SignInManager<ApplicationUser> _signInManager; // For password check
        private readonly IConfiguration _configuration;               // For JWT settings

        // Constructor injection
        public AuthController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        // POST api/auth/login - Login and get JWT
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            // Find user by email
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return Unauthorized(new { Message = "Invalid email or password. Please try again." });

            // Check if user is active and not deleted
            if (!user.IsActive || user.IsDeleted)
                return Unauthorized(new { Message = "Your account is inactive or deleted." });

            // Check the password
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded)
                return Unauthorized(new { Message = "Invalid email or password. Please try again." });

            // Get user roles
            var userRoles = await _userManager.GetRolesAsync(user);

            // Create claims for JWT
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email)
            };

            // Add roles to claims
            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // Create signing key and creds
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Generate token
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            // Return token to client
            return Ok(new
            {
                Message = "Login successful.",
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }

        // POST api/auth/register - Register a new user
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Check if email exists
            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
                return BadRequest(new { Message = "This email address is already registered." });

            // Create new ApplicationUser
            var user = new ApplicationUser
            {
                UserName = model.Email,  // UserName = Email
                Email = model.Email,
                Name = model.Name,
                Age = model.Age,
                Address = model.Address,
                IsActive = model.IsActive,
                EmailConfirmed = true
            };

            // Create user with password
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return BadRequest(new { Message = "User registration failed.", Errors = result.Errors });

            // Assign default role
            await _userManager.AddToRoleAsync(user, "User");

            return Ok(new { Message = "Registration successful." });
        }
    }

    // Login request model
    public class LoginModel
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
