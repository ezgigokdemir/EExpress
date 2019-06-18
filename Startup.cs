using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EExpress.Interface;
using EExpress.Models;
using EExpress.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace EExpress
{
    public class Startup
    {
        private IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1);

            services.AddDbContext<ApplicationIdentityContext>(x => x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationIdentityContext>().AddDefaultTokenProviders();

        //    services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        //.AddCookie(options =>
        //{
        //    options.LoginPath = "/Areas/Admin/Views/Home/dminLogin/";
        //});

            services.AddTransient<IProduct, EFProductRepository>();
            services.AddTransient<IBrand, EFBrandRepository>();
            services.AddTransient<ICategory, EFCategoryRepository>();
            services.AddTransient<IBasket, EFBasketRepository>();

            var connection = "Server=WISSEN\\SQLEXPRESS;Database=EExpress;User Id=sa; password=123456;Trusted_Connection=True;MultipleActiveResultSets=true";

            var connection2 = "Server=UUR;Database=EExpress;User Id=sa; password=123456;Trusted_Connection=True;MultipleActiveResultSets=true";

            services.AddDbContext<Context>(x => x.UseSqlServer(connection2));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "node_modules")),
                RequestPath = "/modules"
            });

            //app.UseMvcWithDefaultRoute();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: "areas",
                  template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

                routes.MapRoute(
                  name: "default",
                  template: "{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}
