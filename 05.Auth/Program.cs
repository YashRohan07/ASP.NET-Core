using Microsoft.AspNetCore.Authentication.JwtBearer;  // For JWT authentication support
using Microsoft.AspNetCore.Identity;                  // For Identity framework (User & Role management)
using Microsoft.EntityFrameworkCore;                  // For EF Core DB context and configuration
using Microsoft.IdentityModel.Tokens;                 // For JWT token validation parameters
using System.Text;                                    // For Encoding (used in JWT key)
using UserManagement.Data;                            // For AppDbContext and SeedData
using UserManagement.Models;                          // For ApplicationUser
using UserManagement.Middleware;
using System.IdentityModel.Tokens.Jwt;                // For clearing claim type mapping


var builder = WebApplication.CreateBuilder(args);     // Create builder for WebApplication

// Add services to the dependency injection container
builder.Services.AddControllers();                    // Register controllers for API endpoints
builder.Services.AddEndpointsApiExplorer();           // For API endpoint exploration (Swagger)
builder.Services.AddSwaggerGen();                     // Add Swagger for API documentation

builder.Services.AddAutoMapper(typeof(Program));    // Register AutoMapper (this scans your assembly for MappingProfile)

// Configure CORS to allow requests from any origin (useful for frontend testing)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()                       // Allow requests from any domain
              .AllowAnyMethod()                       // Allow any HTTP method (GET, POST, etc.)
              .AllowAnyHeader();                      // Allow any HTTP headers
    });
});

// Get connection string from appsettings.json for database connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Configure EF Core with SQL Server provider and the connection string
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// Configure Identity system with ApplicationUser and IdentityRole types
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // Password settings (customize to your security needs)
    options.Password.RequireDigit = true;               // Password must contain at least one digit
    options.Password.RequiredLength = 6;                // Minimum password length is 6 characters
    options.Password.RequireNonAlphanumeric = false;    // No special chars required
    options.Password.RequireUppercase = false;          // No uppercase letter required
    options.Password.RequireLowercase = true;           // Password must contain lowercase letters
})
.AddEntityFrameworkStores<AppDbContext>()               // Use EF Core to store Identity data
.AddDefaultTokenProviders();                            // Enable token providers for password reset, email confirmation

// ** Clear default claim type mapping to keep JWT claim types as-is **
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // Add this line BEFORE AddAuthentication

// Configure JWT Bearer authentication scheme
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;  // Use JWT Bearer by default
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;     // Challenge with JWT Bearer
})
.AddJwtBearer(options =>
{
    // Setup token validation parameters to ensure JWT tokens are valid and secure
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,                                        // Validate the issuer of the token
        ValidateAudience = true,                                      // Validate audience (who the token is for)
        ValidateLifetime = true,                                      // Check token expiration
        ValidateIssuerSigningKey = true,                              // Verify the signing key (secret)
        ValidIssuer = builder.Configuration["Jwt:Issuer"],            // Get valid issuer from config
        ValidAudience = builder.Configuration["Jwt:Audience"],        // Get valid audience from config
        IssuerSigningKey = new SymmetricSecurityKey(                  // Symmetric security key for signing tokens
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

// Build the web application
var app = builder.Build();

// If running in development environment, enable Swagger UI for API testing and docs
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();              // Enable Swagger generator middleware
    app.UseSwaggerUI();            // Enable Swagger UI middleware
}

// Enforce HTTPS redirection (redirect HTTP requests to HTTPS)
app.UseHttpsRedirection();

// Enable serving static files (such as your frontend files inside wwwroot)
app.UseStaticFiles();

// Enable CORS policy named "AllowAll" to allow cross-origin requests
app.UseCors("AllowAll");

// Add Authentication middleware - must be before Authorization
app.UseAuthentication();

// Register custom middleware here
app.UseMiddleware<ActiveUserMiddleware>();

// Add Authorization middleware - must come after Authentication
app.UseAuthorization();

// Map controller endpoints (attribute routing)
app.MapControllers();

// Call SeedData to create roles & admin user at app startup
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedData.InitializeAsync(services);    // Run the seed logic
}

// Run the app (start listening for HTTP requests)
app.Run();
