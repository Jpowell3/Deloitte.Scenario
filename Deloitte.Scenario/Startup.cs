using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Deloitte.Scenario.BusinessLogic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Deloitte.Scenario.Models;
using Deloitte.Scenario.Data;
using Microsoft.EntityFrameworkCore;
using Deloitte.Scenario.BusinesLogic.Servcies.WeatherService;
using AutoMapper;
using System.Net.Http;
using Deloitte.Scenario.Services.Servcies.CountryService;

namespace Deloitte.Scenario
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Deloitte Scenario API", Version = "v1" });
            });

            services.AddAutoMapper(typeof(Startup));

            services.AddTransient<IServiceCore, ServiceCore>();
            services.AddTransient<ICityContext, CityContext>();
            services.AddTransient<ICityRepository, CityRepository>();
            services.AddTransient<IWeatherService, WeatherService>();
            services.AddTransient<ICountryService, CountryService>();

            services.AddHttpClient();
            services.AddSingleton(Configuration);

            services.AddDbContext<CityContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Deloitte.Scenario.Api");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
