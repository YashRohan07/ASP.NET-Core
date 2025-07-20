using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Data;
using UserManagement.Models;

namespace UserManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        // GET api/users?search=&status=&sort=
        [HttpGet]
        public async Task<IActionResult> GetUsers(
            [FromQuery] string? search,
            [FromQuery] string? status,
            [FromQuery] string? sort)
        {
            var users = _context.Users.AsQueryable();

            // Search by Name or Email
            if (!string.IsNullOrEmpty(search))
            {
                string lowerSearch = search.ToLower();
                users = users.Where(u => u.Name.ToLower().Contains(lowerSearch) || u.Email.ToLower().Contains(lowerSearch));
            }

            // Filter by IsActive status
            if (!string.IsNullOrEmpty(status))
            {
                if (status.ToLower() == "active")
                    users = users.Where(u => u.IsActive);
                else if (status.ToLower() == "inactive")
                    users = users.Where(u => !u.IsActive);
            }

            // Sort by Age ascending or descending
            if (!string.IsNullOrEmpty(sort))
            {
                if (sort.ToLower() == "age_asc")
                    users = users.OrderBy(u => u.Age);
                else if (sort.ToLower() == "age_desc")
                    users = users.OrderByDescending(u => u.Age);
            }
            else
            {
                users = users.OrderBy(u => u.Id);
            }

            var userList = await users.ToListAsync();

            return Ok(new
            {
                Message = "Users retrieved successfully.",
                Data = userList
            });
        }

        // GET api/users/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound(new { Message = $"User with ID {id} not found." });

            return Ok(new { Message = "User retrieved successfully.", Data = user });
        }

        // POST api/users
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, new
            {
                Message = "User created successfully.",
                Data = user
            });
        }

        // PUT api/users/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User updatedUser)
        {
            if (id != updatedUser.Id)
                return BadRequest(new { Message = "User ID mismatch." });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null)
                return NotFound(new { Message = $"User with ID {id} not found." });

            existingUser.Name = updatedUser.Name;
            existingUser.Email = updatedUser.Email;
            existingUser.PhoneNumber = updatedUser.PhoneNumber;
            existingUser.Address = updatedUser.Address;
            existingUser.IsActive = updatedUser.IsActive;
            existingUser.Age = updatedUser.Age;

            await _context.SaveChangesAsync();

            return Ok(new { Message = "User updated successfully." });
        }

        // DELETE api/users/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound(new { Message = $"User with ID {id} not found." });

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "User deleted successfully." });
        }
    }
}
