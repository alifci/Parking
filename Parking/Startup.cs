using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Parking.Domain.Mapping;
using Parking.Entity.Context;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using Parking.Entity.Repository;
using Parking.Middlewares;
using Parking.Entity.Entity;
using Microsoft.AspNetCore.Identity;
using Parking.Entity.UnitOfWork;
using System.Reflection;
using System.IO;
using Parking.Entity.Repository.CarRep;
using Parking.Domain.Service.CarServ;
using Parking.Entity.Repository.EmployeeRep;
using Parking.Domain.Service.EmployeeServ;
using Parking.Entity.Repository.CardRep;
using Parking.Domain.Service.CardServ;
using Parking.Domain.Service.HighwayGatePassingServ;
using Parking.Entity.Repository.HighwayGatePassingRep;

namespace Parking
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

            Log.Information("Startup::ConfigureServices");

            try
            {
                services.AddDbContext<ParkingContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:ParkingContext"]));
                services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
                services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

                #region "Swagger"  	    
                //Swagger API documentation
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new Info { Title = "Parking API", Version = "v1" });

                    //c.DocumentFilter<api.infrastructure.filters.SwaggerSecurityRequirementsDocumentFilter>();
                    
                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    c.IncludeXmlComments(xmlPath);
                });
                #endregion

                #region DI
                services.AddTransient<IUnitOfWork, UnitOfWork>();
                services.AddTransient<IEmployeeRepository, EmployeeRepository>();
                services.AddTransient<IEmployeeService, EmployeeService>();
                services.AddTransient<ICarRepository, CarRepository>();
                services.AddTransient<ICarService, CarService>();
                services.AddTransient<ICardRepository, CardRepository>();
                services.AddTransient<ICardService, CardService>();
                services.AddTransient<IHighwayGatePassingRepository, HighwayGatePassingRepository>();
                services.AddTransient<IHighwayGatePassingService, HighwayGatePassingService>();
                #endregion


                //data mapper profiler setting
                Mapper.Initialize((config) =>
                {
                    config.AddProfile<MappingProfile>();
                });

                services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

                services.Configure<ApiBehaviorOptions>(options =>
                {
                    options.SuppressModelStateInvalidFilter = true;
                });
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            Log.Information("Startup::Configure");

            try
            {
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }
                else
                {
                    app.UseMiddleware<ExceptionHandler>();
                }

                app.UseHttpsRedirection();
                app.UseStaticFiles();
                app.UseMvc(routes =>
                {
                    routes.MapRoute(
                      name: "areas",
                      template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                    );
                });

                //Swagger API documentation
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Parking API V1");
                });
                //Seeder
                using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    serviceScope.ServiceProvider.GetService<ParkingContext>().EnsureMigrated();
                    serviceScope.ServiceProvider.GetService<ParkingContext>().EnsureSeeded();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }
    }
}
