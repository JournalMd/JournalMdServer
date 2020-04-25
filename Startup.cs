using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using JournalMdServer.Models;
using JournalMdServer.Services;
using JournalMdServer.Repositories;
using JournalMdServer.Interfaces;
using JournalMdServer.Helpers;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using AutoMapper;
using Task = System.Threading.Tasks.Task;

namespace JournalMdServer
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;

        public Startup(IWebHostEnvironment env, IConfiguration configuration)
        {
            _env = env;
            _configuration = configuration;
        }

        public IConfiguration Configuration => _configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            var appSettings = appSettingsSection.Get<AppSettings>();

            // Environment specific servers
            ConfigureDatabase(services, appSettings);

            // services.AddCors();
            services.AddControllers();

            // Auto Mapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Use jwt authentication
            ConfigureJwt(services, appSettings);

            // Add Swagger and define documents
            if (_env.IsDevelopment())
            {
                ConfigureSwagger(services);
            }

            // Add all Repositories
            services.AddScoped(typeof(IRepository<>), typeof(EntityRepository<>));

            // Add scoped (per client connection) services https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-3.1#register-additional-services-with-extension-methods
            services.AddScoped<UsersService>(); // or Singleton as in https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mongo-app?view=aspnetcore-3.1&tabs=visual-studio ?
            services.AddScoped<NoteTypesService>();
            services.AddScoped<NoteFieldsService>();
            services.AddScoped<TagsService>();
            services.AddScoped<CategoriesService>();
            services.AddScoped<NotesService>();
        }

        private static void ConfigureJwt(IServiceCollection services, AppSettings appSettings)
        {
            if (appSettings.Secret == string.Empty)
            {
                throw new ArgumentException("Secret not set.", "AppSettings.Secret");
            }
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // Bearer
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var usersService = context.HttpContext.RequestServices.GetRequiredService<UsersService>();
                        var userId = int.Parse(context.Principal.Identity.Name);
                        var user = usersService.GetById(userId);
                        if (user == null)
                        {
                            // User does not exist anymore // TODO: add/check for deleted?!
                            context.Fail("Unauthorized");
                        }
                        return Task.CompletedTask;
                    }
                };
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        private static void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "JournalMd Server",
                    Description = "API documentation for JournalMd.",
                    TermsOfService = new Uri("https://www.spech.de/impressum"),
                    Contact = new OpenApiContact
                    {
                        Name = "Sebastian Pech",
                        Email = string.Empty,
                        Url = new Uri("https://www.spech.de"),
                    },
                    /*License = new OpenApiLicense
                    {
                        Name = "Private",
                        Url = new Uri("https://example.com/license"),
                    }*/
                });
                // Add Authentication Button
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                // Add Authorize Filter
                c.OperationFilter<AuthResponsesOperationFilter>();
                // Show locks
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                    {
                         {
                            new OpenApiSecurityScheme{
                                Reference = new OpenApiReference{
                                    Id = "Bearer", //The name of the previously defined security scheme.
                                    Type = ReferenceType.SecurityScheme
                                }
                            }, new List<string>()
                        }
                    });
            });
        }

        private void ConfigureDatabase(IServiceCollection services, AppSettings appSettings)
        {
            string dbType = appSettings.DbType;
            switch (dbType)
            {
                case "mssql":
                    {
                        services.AddDbContext<JournalMdServerContext>(options => options.UseSqlServer(_configuration.GetConnectionString("mssqlDatabase")));
                        break;
                    }
                case "mysql":
                    {
                        services.AddDbContextPool<JournalMdServerContext>(options => options.UseMySql(_configuration.GetConnectionString("mysqlDatabase")));
                        //, mySqlOptions => mySqlOptions.ServerVersion(new Version(8, 0, 18), ServerType.MySql);
                        break;
                    }
                case "sqlite":
                    {
                        if (_env.IsProduction())
                        {
                            Console.WriteLine("SQLite should not be used on production environment!");
                        }
                        services.AddDbContext<JournalMdServerContext>(options => options.UseSqlite(_configuration.GetConnectionString("sqliteDatabase")));
                        break;
                    }
                default:
                    {
                        throw new ArgumentException("DbType not supported.", "AppSettings.DbType");
                    }
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // Enable middleware to serve generated Swagger as a JSON endpoint.
                app.UseSwagger();

                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
                // specifying the Swagger JSON endpoint.
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // TODO optimize
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
