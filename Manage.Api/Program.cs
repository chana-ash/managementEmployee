using Manage.Core.Repositories;
using Manage.Core.Services;
using Manage.Core;
using Manage.Data.Repositories;
using Manage.Data;
using Manage.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using System.Text;
using Manage.Api.MiddleWares;
using NLog;
using NLog.Web;

// Set up NLog for Dependency injection
var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
try
{
    logger.Debug("init main");

    var builder = WebApplication.CreateBuilder(args);

    // Clear default providers and use NLog
    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
    
    builder.Host.UseNLog();
    


    // Add services to the container.
    builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });

    // Swagger/OpenAPI Configuration
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Name = "Authorization",
            Description = "Bearer Authentication with JWT Token",
            Type = SecuritySchemeType.Http
        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    }
                },
                new List<string>()
            }
        });
    });

    // Add scoped services
    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddScoped<IReportService, ReportService>();
    builder.Services.AddScoped<IVacationService, VacationService>();
    builder.Services.AddScoped<IManagerRepository, ManagerRepository>();
    builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

    builder.Services.AddDbContext<DataContext>();
    builder.Services.AddAutoMapper(typeof(MappingProfile));

    // Add authentication configuration
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
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidAudience = builder.Configuration["JWT:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
        };
    });

    var app = builder.Build();

    // Configure the HTTP request pipeline
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseShabbat();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex, "Stopped program because of exception");
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}
