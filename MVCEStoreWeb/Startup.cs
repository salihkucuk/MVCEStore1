using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MvcEStoreData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCEStoreWeb
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
            services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0);

            services.AddControllersWithViews();

            services.AddDbContext<AppDbContext>(options =>
            {
                switch (Configuration.GetValue<string>("Application:DatabaseProvider"))
                {
                    case "sqlserver":

                    default:
                        options.UseSqlServer(
                            Configuration.GetConnectionString("SqlServer"),
                            config =>
                            {
                                config.MigrationsAssembly("MVCEStoreMigrationSqlServer");
                            })
                        .UseLazyLoadingProxies();
                        break;
                }


            });

            services.AddIdentity<User, Role>(options => {
                options.Password.RequireDigit = Configuration.GetValue<bool>("Application:Security:Password:RequireDigit");
                options.Password.RequiredLength = Configuration.GetValue<int>("Application:Security:Password:RequiredLength");
                options.Password.RequireLowercase = Configuration.GetValue<bool>("Application:Security:Password:RequireLowercase");
                options.Password.RequireNonAlphanumeric = Configuration.GetValue<bool>("Application:Security:Password:RequireNonAlphanumeric");
                options.Password.RequireUppercase = Configuration.GetValue<bool>("Application:Security:Password:RequireUppercase");
                options.Password.RequiredUniqueChars = Configuration.GetValue<int>("Application:Security:Password:RequiredUniqueChars");

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
                options.Lockout.MaxFailedAccessAttempts = 3;
            })
                .AddEntityFrameworkStores<AppDbContext>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IWebHostEnvironment env,
            AppDbContext context,
            RoleManager<Role> roleManager,
            UserManager<User> userManager)
        {
            context.Database.Migrate();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseStatusCodePagesWithReExecute("/home/error/{0}");

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                   name: "areas",
                   pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            var roles = new[]
            {
                new Role {Name="Administrators",FriendlyName="Yöneticiler"},
                new Role {Name="ProductAdministrators",FriendlyName="Ürün Yöneticileri"},
                new Role {Name="OrderAdministrators",FriendlyName="Sipariþ Yöneticileri"},
                new Role {Name="Members",FriendlyName="Üyeler"}
            }
            .ToList();

            roles.ForEach(p =>
            {
                if (!roleManager.RoleExistsAsync(p.Name).Result)
                    roleManager.CreateAsync(p).Wait();
                
            });

            var user = new User
            {
                Name = Configuration.GetValue<string>("Application:Security:DefaultAdmin:Name"),
                UserName = Configuration.GetValue<string>("Application:Security:DefaultAdmin:UserName"),

            };
            if (userManager.FindByNameAsync(user.UserName).Result == null)
            {
              userManager.CreateAsync(user, Configuration.GetValue<string>("Application:Security:DefaultAdmin:Password")).Wait();
              userManager.AddToRoleAsync(user, roles.First().Name).Wait();
            }
            
        }
    }
}
