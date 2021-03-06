using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Authentication.Helpers.WebApi.Helpers;
using Authentication.Services;
using AutoMapper;
using Domain.Services;
using Domain.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenTelemetry.Trace.Configuration;
using NServiceBus.Extensions.Diagnostics.OpenTelemetry;

namespace AuthService
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddIdentityServer()
            .AddDeveloperSigningCredential()
            .AddInMemoryApiResources(Config.Apis)
            .AddInMemoryClients(Config.Clients);
            // use sql server db in production and sqlite db in development
            if (_env.IsProduction())
                services.AddDbContext<DataContext>();
            else
                services.AddDbContext<DataContext, SqliteDataContext>();

            services.AddCors();
            services.AddControllers();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddOpenTelemetry(builder => builder
                .UseZipkin(o =>
                {
                    o.Endpoint = new Uri("http://localhost:9411/api/v2/spans");
                    o.ServiceName = Program.EndPointName;
                })
                .UseJaeger(c =>
                {
                    c.AgentHost = "localhost";
                    c.AgentPort = 6831;
                    c.ServiceName = Program.EndPointName;
                })
                .AddNServiceBusAdapter()
                .AddRequestAdapter()
                .AddDependencyAdapter(configureSqlAdapterOptions: 
                    opt => opt.CaptureTextCommandContent = true));


            // configure strongly typed settings objects
            var appSettingsSection = _configuration.GetSection("AuthSettings");
            services.Configure<AuthSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AuthSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
           
            services.AddScoped<IUserService, UserService>();

              services.AddSwaggerGen (c => {
                c.SwaggerDoc ("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "User Management and Authentication API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataContext dataContext)
        {
            
             app.UseIdentityServer();
            
            // migrate any database changes on startup (includes initial db creation)            
            dataContext.Database.Migrate();

            app.UseRouting();

            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());

            app.UseSwagger ();
            app.UseSwaggerUI (c => {
                c.SwaggerEndpoint ("/swagger/v1/swagger.json", "User Management Api");
            });
        }
    }
}
