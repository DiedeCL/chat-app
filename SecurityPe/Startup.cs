using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SecurityPe.Data;

namespace SecurityPe
{
    public class Startup
    {
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(); //voegt MVC services toe zo dat ge een web api kunt maken
            services.AddDbContext<ChatAppContext>(
                options =>
                {
                    var connectionString =
                        @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ChatAppDB;Integrated Security=True;"; //Configuration["ConnectionString"];
                    options.UseSqlServer(connectionString);
                });
        }

        

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            /*app.UseDefaultFiles();  // zo dat de / naar /index.html verwijst
            app.UseStaticFiles(); // zo dat de files van wwwroot ingelezen kunnen worden dus url/naamvandefile.html*/
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllerRoute("Default", "{controller=Home}/{action=Index}/{id?}"); 
            });
        }
    }
}