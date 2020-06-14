using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Products;
using Services;
using DatabaseSettings;
using Domain.Services;
using Domain.Settings;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace CatalogService
{
    public class Startup
    {

        private readonly IConfiguration configuration; 
        public Startup(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<DBSettings> (
                configuration.GetSection (nameof (DBSettings)));

            services.Configure<AuthSettings> (
                configuration.GetSection (nameof (AuthSettings)));
           
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "http://localhost:5001";
                    options.RequireHttpsMetadata = false;

                    options.Audience = "Catalog-Api";
                });



            services.AddTransient<IProductService<Book>,ProductServiceBase<Book>>();

            services.AddSingleton<IDatabaseSettings> (sp =>
                sp.GetRequiredService<IOptions<DBSettings>> ().Value);
            services.AddControllers();

             services.AddSwaggerGen (c => {
                c.SwaggerDoc ("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Product Catalog API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger ();
            app.UseSwaggerUI (c => {
                c.SwaggerEndpoint ("/swagger/v1/swagger.json", "Book Catalog Api");
            });
        }
    }
}
