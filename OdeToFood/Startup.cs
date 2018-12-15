using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OdeToFood.Services;

namespace OdeToFood
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IGreeter, Greeter>(); // Dependency Injection
            services.AddScoped<IRestaurantData, InMemoryRestaurantData>(); // scoped per http request 
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app, 
            IHostingEnvironment env,
            IGreeter greeter,
            ILogger<Startup> logger
            )
        {
            #region old 

            //app.Use(next =>
            //{
            //    return async context =>
            //    {
            //        logger.LogInformation("Request incoming.");
            //        if (context.Request.Path.StartsWithSegments("/mym"))
            //        {
            //            await context.Response.WriteAsync("Hit");
            //            logger.LogInformation("Request handled.");
            //        }
            //        else
            //        {
            //            await next(context);
            //            logger.LogInformation("Request outgoing.");
            //        }

            //    };
            //});
            //app.UseWelcomePage(new WelcomePageOptions
            //{
            //    Path = "/"
            //}); //it fits all request and leaves no chance to code to continue
            #endregion

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); // catches exceptions
            }
            //app.UseDefaultFiles();// index.html already defined, must be before use static  files
            //app.UseStaticFiles();
            //OR
            app.UseStaticFiles();

            app.UseMvc(ConfigureRoutes);
            app.Run(async (context) =>
            {
                var greeting = greeter.GetMessageOfTheDay();
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync($"Not found");
            });
        }

        private void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            //Home/Index 
            routeBuilder.MapRoute("Default", "{controller=Home}/{action=Index}");
        }
    }
}
