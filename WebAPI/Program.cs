using DataAccess.Data;
using DataAccess.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repositories.Implementations;
using Repositories.Interfaces;
using Services.Implementations;
using Services.Interfaces;
using Stock_Trading_System.Config;
using Stock_Trading_System.Profiles;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<ApplicationDBContext>()
.AddDefaultTokenProviders();

// Add IdentityServer
builder.Services.AddIdentityServer()
    .AddDeveloperSigningCredential() 
    .AddInMemoryClients(IdentityServerConfig.Clients)
    .AddInMemoryIdentityResources(IdentityServerConfig.IdentityResources)
    .AddInMemoryApiScopes(IdentityServerConfig.ApiScopes)
    .AddInMemoryApiResources(IdentityServerConfig.ApiResources)
    .AddAspNetIdentity<ApplicationUser>();

// Add authentication services
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://localhost:5445";
        options.Audience = "protectedApi";
        options.RequireHttpsMetadata = false; 
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidAudience = "protectedApi",
            ValidateIssuer = true,
            ValidIssuer = "https://localhost:5445",
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            NameClaimType = ClaimTypes.NameIdentifier
        };
    });

// Add authorization policy
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AccessPolicy", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "protectedApi");
        policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
    });
});

// Add controllers
builder.Services.AddControllers();

// Database
builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IStockRepository, StockRepository>();
builder.Services.AddScoped<ITradeRepository, TradeRepository>();

// Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IStockService, StockService>();
builder.Services.AddScoped<ITradeService, TradeService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(StockProfile));

var app = builder.Build();

// Middleware
app.UseRouting();
app.UseIdentityServer();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();