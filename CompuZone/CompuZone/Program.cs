using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using CompuZone.API.Middelware;
using CompUZone.Models;
using CompuZone.Application;
using CompuZone.Infrastructure;
using Microsoft.AspNetCore.Identity;
using CompuZone.Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CompuZone.Domain.Interfaces;
using CompuZone.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// 1. Swagger Configuration (Kept your settings)
builder.Services.AddSwaggerGen(setupAction =>
{
    setupAction.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token.\r\n\r\nExample: \"Bearer 12345abcdef\""
    });

    setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// 2. Database Context
builder.Services.AddDbContext<CompuZoneContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// 3. Add Identity (User Management)
builder.Services.AddIdentity<ApplicaitonUser, IdentityRole>()
    .AddEntityFrameworkStores<CompuZoneContext>()
    .AddDefaultTokenProviders();

// 4. Add Authentication (JWT Security)
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false; // Set to true in Production!

    // Get key from appsettings.json
    var key = builder.Configuration["SecurityKey"];
    var keyBytes = Encoding.ASCII.GetBytes(key);

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        ValidateIssuer = false,   // Set to true if you want to validate Server URL
        ValidateAudience = false, // Set to true if you want to validate Client URL
        ClockSkew = TimeSpan.Zero
    };
});

// 5. Register Repositories (FIXES YOUR CRASH)
// Note: You can also move these into your AddInfrastructureServices() method
//builder.Services.AddScoped<IOrderRepository, OrderRepository>();

// 6. Add Custom Layer Services
builder.Services.AddApplicationServices()
                .AddInfrastructureServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 7. Enable Authentication Middleware (MUST be before Authorization)
app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandle>();

app.MapControllers();

app.Run();