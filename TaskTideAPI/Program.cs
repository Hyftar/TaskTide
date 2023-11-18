using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NetCore.AutoRegisterDi;
using NodaTime;
using System.Text;
using TaskTideAPI.DataContexts;
using Newtonsoft.Json;
using NodaTime.Extensions;

namespace TaskTideAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var configuration = builder.Configuration;

            var jwtSecretKey = Environment.GetEnvironmentVariable("JWT_KEY") ?? configuration["JWT:Key"]!;
            // Add services to the container.

            builder.Services.AddDbContext<TaskTideContext>(
                options =>
                {
                    options.UseNpgsql(configuration.GetConnectionString("TaskTidePGLocal"));
                }
            );

            builder
                .Services
                .AddControllers()
                .AddNewtonsoftJson(
                    options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                );

            builder.Services
                   .RegisterAssemblyPublicNonGenericClasses()
                   .Where(x => !x.FullName?.EndsWith("Result") ?? false)
                   .AsPublicImplementedInterfaces();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
            {
                options.TokenValidationParameters =
                    new()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = false,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["JWT:Issuer"],
                        ValidAudience = configuration["JWT:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey)),
                    };

            });
            builder.Services.AddAuthorization();

            builder
                .Services
                .AddSwaggerGen(
                    options =>
                    {
                        options.SwaggerDoc("v1", new OpenApiInfo { Title = "TaskTide API", Version = "v1" });

                        options.AddSecurityDefinition(
                            "bearer",
                            new OpenApiSecurityScheme()
                            {
                                Name = "Authorization",
                                Type = SecuritySchemeType.Http,
                                Scheme = "bearer",
                                BearerFormat = "JWT",
                                In = ParameterLocation.Header,
                                Description = "JWT Authorization header using the Bearer scheme",
                            }
                        ); ;

                        options.AddSecurityRequirement(
                            new OpenApiSecurityRequirement
                            {
                                {
                                    new OpenApiSecurityScheme
                                    {
                                        Reference = new OpenApiReference
                                        {
                                            Id = "bearer",
                                            Type = ReferenceType.SecurityScheme,
                                        },
                                        In = ParameterLocation.Header,
                                        BearerFormat = "JWT"
                                    },
                                    Array.Empty<string>()
                                }
                            }
                        );
                    }
                );

            builder.Services.AddSingleton(SystemClock.Instance.InUtc());

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
