using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.IO;
using HR_EmployeeInfo.Data;

namespace HR_EmployeeInfo
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddEntityFramework()
                .AddDbContext<EmployeeContext>(config =>
                {
                    config.UseSqlServer(Configuration["ConnectionStrings:HR_Database"]);
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
            app.UseMvc();
            using (var context = app.ApplicationServices.GetService<EmployeeContext>())
            {
                //context.Database.Migrate();
            }
        }

        public Startup(IHostingEnvironment evn)
        {
            var builder = new ConfigurationBuilder();
            var config = builder
                .AddJsonFile(Path.Combine(evn.ContentRootPath, "appsettings.json"), optional: false)
                .AddEnvironmentVariables();
            Configuration = config.Build();
        }
        public IConfigurationRoot Configuration { get; private set; }
    }
}
