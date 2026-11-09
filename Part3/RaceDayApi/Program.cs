using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RaceDayApi.Data;
using RaceDayApi.Models;
using Microsoft.OpenApi.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore.Sqlite;

SQLitePCL.Batteries.Init(); // Add this line at the top

var builder = WebApplication.CreateBuilder(args);

// 1. Add Database Context (using SQLite for simplicity)
builder.Services.AddDbContext<RaceDayApi.Data.AppDbContext>(options =>
    options.UseSqlite("Data Source=app.db"));

// 2. Configure JWT Authentication
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"] ?? throw new InvalidOperationException("JWT Key not found"));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

builder.Services.AddAuthorization();
builder.Services.AddControllers();

// 3. Configure Swagger to accept the Bearer token
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement{
    {
        new OpenApiSecurityScheme{
            Reference = new OpenApiReference{
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
        new string[]{}
    }});
});

var app = builder.Build();

// Seed a default admin and user for testing
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<RaceDayApi.Data.AppDbContext>();
    //db.Database.EnsureCreated();
    if (!db.Users.Any())
    {
        db.Users.Add(new RaceDayApi.Models.User { Username = "admin", PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"), Role = "Organizer" });
        db.Users.Add(new RaceDayApi.Models.User { Username = "john", PasswordHash = BCrypt.Net.BCrypt.HashPassword("User123!"), Role = "Participant" });
        db.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseStaticFiles();

app.UseAuthentication(); // Must be before UseAuthorization
app.UseAuthorization();

app.MapControllers();

app.Run();