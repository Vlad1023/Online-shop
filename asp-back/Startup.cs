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
using asp_back.Models;
using Microsoft.AspNetCore.Http;

namespace asp_back
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
            services.AddCors(options =>
            {
                options.AddPolicy(name: "CORS POLICY",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:8080")
                            .AllowCredentials()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                            //.SetIsOriginAllowedToAllowWildcardSubdomains();
                    });
            });


            string connectionString = "Server=DESKTOP-VKNS1L5;Database=ProjectDb;Integrated Security=True";
            services.AddTransient<IMarket1Repository, Market1Repo>(provider => new Market1Repo(connectionString));
            services.AddTransient<IMarket2Repository, Market2Repo>(provider => new Market2Repo(connectionString));
            services.AddTransient<IUserRepository, UserRepo>(provider => new UserRepo(connectionString));
            services.AddTransient<ICartItemRepo, CartItemRepo>(provider => new CartItemRepo(connectionString));

            services.AddCors();


            services.AddDistributedMemoryCache();
            services.AddSession(opt =>
            {
                opt.Cookie.IsEssential = true;
                opt.Cookie.HttpOnly = true;
                opt.Cookie.Name = ".Remontka.Session";
                opt.IdleTimeout = TimeSpan.FromMinutes(15);
                opt.Cookie.SameSite = SameSiteMode.None;
            });
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseAuthorization();
            app.UseCors("CORS POLICY");
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
