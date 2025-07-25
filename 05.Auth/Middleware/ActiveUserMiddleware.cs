using Microsoft.AspNetCore.Http;           // Provides HttpContext and HTTP status codes
using Microsoft.AspNetCore.Identity;       // Provides UserManager for Identity operations
using System.Linq;                         // For LINQ operations
using System.Security.Claims;              // For accessing user claims from JWT
using System.Text.Json;                    // For serializing JSON responses
using System.Threading.Tasks;              // For async execution
using UserManagement.Models;               // ApplicationUser model with custom fields

namespace UserManagement.Middleware
{
    public class ActiveUserMiddleware
    {
        private readonly RequestDelegate _next;  // Holds the next middleware in the pipeline

        public ActiveUserMiddleware(RequestDelegate next)
        {
            _next = next;  // Store the next middleware delegate for later execution
        }

        public async Task InvokeAsync(HttpContext context, UserManager<ApplicationUser> userManager)
        {
            // Check if the request is authenticated; if not, [Authorize] attribute will handle it
            if (context.User.Identity?.IsAuthenticated == true)
            {
                // Extract the user's unique ID (GUID) from the JWT claims
                var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (userId != null)
                {
                    // Look up the user in the database using the ID from the JWT
                    var user = await userManager.FindByIdAsync(userId);

                    if (user != null)
                    {
                        // If the user is inactive or marked as soft deleted, block all access immediately
                        if (!user.IsActive || user.IsDeleted)
                        {
                            context.Response.StatusCode = StatusCodes.Status403Forbidden; // Return HTTP 403 Forbidden
                            context.Response.ContentType = "application/json";
                            var response = new
                            {
                                Message = "Access denied. Your account is inactive or deleted.",
                                Data = (object?)null
                            };
                            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                            return; // Stop processing this request
                        }

                        // Get the list of roles assigned to the current user
                        var roles = await userManager.GetRolesAsync(user);
                        bool isAdmin = roles.Contains("Admin");

                        // Get the current request path for route-specific checks
                        var path = context.Request.Path.Value?.ToLower();

                        // If the user is NOT an Admin and the request is not a GET request:
                        if (!isAdmin && context.Request.Method != HttpMethods.Get)
                        {
                            // Special case: Allow non-Admin users to update their own profile using PUT on /api/users/me
                            if (context.Request.Method == HttpMethods.Put && path == "/api/users/me")
                            {
                                // Allow PUT /api/users/me — no blocking needed
                            }
                            else
                            {
                                // Block all other non-Admin requests that are not GET or allowed PUT
                                context.Response.StatusCode = StatusCodes.Status403Forbidden; // Return HTTP 403 Forbidden
                                context.Response.ContentType = "application/json";
                                var response = new
                                {
                                    Message = "Access denied. Only Admins are permitted to perform this action.",
                                    Data = (object?)null
                                };
                                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                                return; // Stop processing this request
                            }
                        }
                    }
                }
            }

            // If no blocking conditions matched, allow the request to continue to the next middleware
            await _next(context);
        }
    }
}
