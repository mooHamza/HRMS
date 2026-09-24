using HRMS.API.Data;
using HRMS.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using HRMS.API.Features.Auth.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using HRMS.API.Mappers;
using HRMS.API.common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddOpenApi();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddAutoMapper(
    cfg => { },
    typeof(Program)
);
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services.AddIdentity<Employee, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<EmployeeMapper>();
    // builder.Services.AddScoped<SchaduleMapper>();


builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme =
            JwtBearerDefaults.AuthenticationScheme;

        options.DefaultChallengeScheme =
            JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    builder.Configuration["Jwt:Key"]!))
        };
    });      

    builder.Services.AddScoped<ITokenService, TokenService>();
    builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped(typeof(BaseRepository<>));

var app = builder.Build();

app.MapGet("/", () => "HRMS API is running!");
// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.MapOpenApi();
// }
app.MapOpenApi();

// Global Security & Network
app.UseHttpsRedirection();

// CORS must run BEFORE Routing/Auth if you use domain headers or credentials
app.UseCors(policy =>
{
    policy.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
});

// Routing matches incoming requests to endpoint definitions
app.UseRouting();

// Authentication identifies WHO the user is
app.UseAuthentication();

// Authorization checks WHAT the user can do
app.UseAuthorization();

// Execute the endpoint handler
app.MapControllers();

app.Run();
