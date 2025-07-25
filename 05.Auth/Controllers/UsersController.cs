using AutoMapper;                                  // For mapping between entity and DTO objects
using Microsoft.AspNetCore.Authorization;          // For authorization attributes
using Microsoft.AspNetCore.Identity;               // For UserManager and Identity features
using Microsoft.AspNetCore.Mvc;                    // For ControllerBase and routing
using Microsoft.EntityFrameworkCore;               // For async EF Core database queries
using System.Linq;                                 // For LINQ queries on collections
using System.Security.Claims;                      // For retrieving logged-in user info
using System.Threading.Tasks;                      // For async/await support
using UserManagement.Models;                       // For ApplicationUser model
using UserManagement.DTOs;                         // For User-related DTO classes
using System.IdentityModel.Tokens.Jwt;             // For JwtRegisteredClaimNames

namespace UserManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]     // All endpoints require authentication 
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;  // Service to manage users
        private readonly IMapper _mapper;  // AutoMapper instance for DTO mappings

        // Constructor inject UserManager and AutoMapper
        public UsersController(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;    // Assign injected UserManager to field
            _mapper = mapper;              // Assign injected IMapper to field
        }


        // GET api/users - Admin can view all users
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsers(
            [FromQuery] string? search,     // Optional query param to filter by search string
            [FromQuery] string? status,     // Optional query param to filter by status (active/inactive)
            [FromQuery] string? sort)       // Optional query param to sort results
        {
            var users = _userManager.Users     // Get IQueryable of all users from UserManager
                .Where(u => !u.IsDeleted)      // Only users who are NOT soft-deleted
                .AsQueryable();                // Ensure queryable for further filtering

            if (!string.IsNullOrEmpty(search))
            {
                string lower = search.ToLower();  // Convert search string to lowercase for case-insensitive match
                users = users.Where(u =>
                    u.Name.ToLower().Contains(lower) ||
                    u.Email.ToLower().Contains(lower));
            }

            if (!string.IsNullOrEmpty(status))
            {
                users = status.ToLower() == "active"
                    ? users.Where(u => u.IsActive)
                    : users.Where(u => !u.IsActive);
            }

            if (!string.IsNullOrEmpty(sort))
            {
                users = sort.ToLower() == "age_asc"
                    ? users.OrderBy(u => u.Age)
                    : users.OrderByDescending(u => u.Age);
            }

            var list = await users.ToListAsync();  // Execute the query and get list asynchronously
            var listDto = list.Select(u => _mapper.Map<UserDto>(u)).ToList();  // Map each ApplicationUser to UserDto

            return Ok(new { Message = "Users retrieved successfully.", Data = listDto });
        }


        // GET api/users/{id} - Admin can get a specific user by ID
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);
            if (user == null)
                return NotFound(new { Message = $"User with ID {id} was not found.", Data = (object?)null });

            var userDto = _mapper.Map<UserDto>(user);     // Map user to UserDto
            return Ok(new { Message = "User retrieved successfully.", Data = userDto });
        }


        // GET api/users/me - Logged-in active user can view only their own profile details
        [HttpGet("me")]
        public async Task<IActionResult> GetOwnProfile()
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            // Check if user is null or soft deleted
            if (user == null || user.IsDeleted)
                return NotFound(new { Message = "User not found.", Data = (object?)null });

            var userDto = _mapper.Map<UserDto>(user);
            return Ok(new { Message = "Profile retrieved successfully.", Data = userDto });
        }



        // POST api/users - Admin can create a new user account
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateUser([FromBody] RegisterDto dto)
        {
            if (!ModelState.IsValid)        // Validate incoming model
                return BadRequest(ModelState);

            var existingUser = await _userManager.FindByEmailAsync(dto.Email);
            if (existingUser != null)
                return BadRequest(new { Message = "This email address is already registered.", Data = (object?)null });

            var user = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                Name = dto.Name,
                Age = dto.Age,
                Address = dto.Address,
                IsActive = dto.IsActive
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
                return BadRequest(new { Message = "Failed to create user.", Data = result.Errors });

            await _userManager.AddToRoleAsync(user, "User");

            var userDto = _mapper.Map<UserDto>(user);     // Map to UserDto
            return Ok(new { Message = "User created successfully.", Data = userDto });
        }


        // PUT api/users/{id} - Admin updates user details by ID
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UserUpdateDto dto)
        {
            if (!ModelState.IsValid)    // Validate input DTO
                return BadRequest(ModelState);

            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);
            if (user == null)
                return NotFound(new { Message = $"User with ID {id} was not found.", Data = (object?)null });

            // Update user fields
            user.Name = dto.Name;
            user.Age = dto.Age;
            user.Address = dto.Address;
            user.Email = dto.Email;
            user.IsActive = dto.IsActive;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                return BadRequest(new { Message = "Failed to update user.", Data = result.Errors });

            var userDto = _mapper.Map<UserDto>(user);
            return Ok(new { Message = "User updated successfully.", Data = userDto });
        }


        // PUT api/users/me - Logged-in active user can update their own profile information
        [HttpPut("me")]
        public async Task<IActionResult> UpdateOwnProfile([FromBody] UserUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.Id == userId && !u.IsDeleted);
            if (user == null)
                return NotFound(new { Message = "User not found.", Data = (object?)null });

            // Update own profile fields
            user.Name = dto.Name;
            user.Age = dto.Age;
            user.Address = dto.Address;
            user.Email = dto.Email;

            var result = await _userManager.UpdateAsync(user);            

            if (!result.Succeeded)                                       
                return BadRequest(new { Message = "Failed to update profile.", Data = result.Errors });

            var userDto = _mapper.Map<UserDto>(user);  // Map updated user
            return Ok(new { Message = "Profile updated successfully.", Data = userDto }); 
        }


        // DELETE api/users/{id} - Admin can soft delete any user by setting IsDeleted = true
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);
            if (user == null)
                return NotFound(new { Message = $"User with ID {id} was not found.", Data = (object?)null });

            user.IsDeleted = true;   // Soft delete by setting IsDeleted flag
            await _userManager.UpdateAsync(user);

            return Ok(new { Message = "User deleted successfully.", Data = (object?)null });
        }


        // POST api/users/{id}/restore - Admin can restores soft-deleted user
        [HttpPost("{id}/restore")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RestoreUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound(new { Message = $"User with ID {id} was not found.", Data = (object?)null });

            if (!user.IsDeleted)
                return BadRequest(new { Message = "User is not deleted and cannot be restored.", Data = (object?)null });

            user.IsDeleted = false;   // Reset IsDeleted flag to restore
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                return BadRequest(new { Message = "Failed to restore user.", Data = result.Errors });

            var userDto = _mapper.Map<UserDto>(user);
            return Ok(new { Message = "User restored successfully.", Data = userDto });
        }
    }
}
