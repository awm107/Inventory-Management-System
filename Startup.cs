using InventoryManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagement
{
    public class Startup
    {
        private IConfiguration _config;
        public Startup(IConfiguration configuration)
        {
            _config = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddMvc(config => {
                var policy = new AuthorizationPolicyBuilder()
                                .RequireAuthenticatedUser()
                                .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });
            services.AddScoped<ISalesOfficerRepository, SQLSalesOfficerRepository>();
            services.AddScoped<ISupervisorRepository, SQLSupervisorRepository>();
            services.AddScoped<IProductRepository, SQLProductRepository>();
            services.AddScoped<IProductTypeDetailsRepository, SQLProductTypeDetailsRepository>();


            services.AddHttpContextAccessor();

            services.AddDbContextPool<AppDbContext>(options =>
                    options.UseSqlServer(_config.GetConnectionString("SalesOfficerDBConnection")));

            services.AddDbContextPool<AppDbContext>(options =>
                    options.UseSqlServer(_config.GetConnectionString("SupervisorDBConnection")));

            services.AddDbContextPool<AppDbContext>(options =>
                 options.UseSqlServer(_config.GetConnectionString("ProductDBConnection")));

            services.AddDbContextPool<AppDbContext>(options =>
                options.UseSqlServer(_config.GetConnectionString("ProductTypeDetailsDBConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;       //changing defaults
                options.Password.RequiredUniqueChars = 3;
                options.SignIn.RequireConfirmedEmail = true;

            }).AddEntityFrameworkStores<AppDbContext>()
             .AddDefaultTokenProviders();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("DeleteRolePolicy",
                    policy => policy.RequireClaim("Delete Role")
                    );

                options.AddPolicy("EditRolePolicy",
                    policy => policy.RequireClaim("Edit Role", "true"));
               
                options.AddPolicy("EditProductRolePolicy",
                    policy => policy.RequireClaim("Edit Product", "true"));

                options.AddPolicy("CreateProductRolePolicy",
                    policy => policy.RequireClaim("Add Product", "true"));

                options.AddPolicy("DeleteProductRolePolicy",
                   policy => policy.RequireClaim("Delete Product", "true"));

                options.AddPolicy("AdminRolePolicy",
                    policy => policy.RequireRole("Admin"));

                options.AddPolicy("AdminEditRolePolicy",
                    policy => policy.RequireRole("Admin").RequireClaim("Edit Role", "true"));

                options.AddPolicy("AdminCreateRolePolicy",
                    policy => policy.RequireRole("Admin").RequireClaim("Create Role", "true"));

                options.AddPolicy("AdminDeleteRolePolicy",
                   policy => policy.RequireRole("Admin").RequireClaim("Delete Role", "true"));

                options.AddPolicy("SalesOfficerAddProductRolePolicy",
                    policy => policy.RequireRole("SalesOfficer").RequireClaim("Add Product", "true"));

                options.AddPolicy("SupervisorAddProductRolePolicy",
                    policy => policy.RequireRole("Supervisor").RequireClaim("Add Product", "true"));

                options.AddPolicy("SupervisorEditProductRolePolicy",
                    policy => policy.RequireRole("Supervisor").RequireClaim("Edit Product", "true"));
            });

            //to change default path of access denied from account to admin
            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = new PathString("/Administration/AccessDenied");
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseStatusCodePagesWithRedirects("/Error/{0}");
                app.UseExceptionHandler("/Error");
            }
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
