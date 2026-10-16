using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RaceDayApplicationApi.Data;
using RaceDayApplicationApi.Models;
using AppOrganizer = RaceDayApplicationApi.Models.Organizer;

var builder = WebApplication.CreateBuilder(args);

// ---------- Services ----------
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// One DbContext registration, using the "Default" connection string
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// Password hashing (the API never stores plain-text passwords)
builder.Services.AddScoped<IPasswordHasher<AppOrganizer>, PasswordHasher<AppOrganizer>>();
builder.Services.AddScoped<IPasswordHasher<Participant>, PasswordHasher<Participant>>();

// Server-side session: stores the logged-in user's ID and role
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".RaceDay.Session";
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});

var app = builder.Build();

// ---------- Pipeline ----------
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseSession();   // must come before MapControllers

app.MapControllers();

app.Run();

// Makes Program visible to the test project (WebApplicationFactory<Program>)
public partial class Program { }
