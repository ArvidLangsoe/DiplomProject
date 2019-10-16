using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Commands.AddProducts;
using Commands.DeleteProduct;
using Commands.UpdateProducts;
using Core.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Middleware.Authorization;
using Persistence;
using Persistence.Repositories;
using Queries;

namespace API
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
            services.AddDbContext<ProductDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ProductDb")));

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IEventRepository, EventRepository>();

            services.AddScoped<AddProductCommand>();
            services.AddScoped<QueryProducts>();
            services.AddScoped<UpdateProductCommand>();
            services.AddScoped<DeleteProductCommand>();

            services.AddScoped<QueryEvents>();
            

            string authority = Configuration["Auth0:Domain"];

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = authority;
                options.Audience = Configuration["Auth0:ApiIdentifier"];
            });


            services.AddAuthorization(options =>
            {
                options.AddPolicy("read:product", policy => policy.Requirements.Add(new HasScopeRequirement("read:product", authority)));
                options.AddPolicy("edit:product", policy => policy.Requirements.Add(new HasScopeRequirement("edit:product", authority)));
            });

            services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
