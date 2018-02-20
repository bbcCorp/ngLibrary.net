using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ngLibrary.Web
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


            // Enable CORS so other client application can consume the APIs
            var clientHost = Configuration["ClientApp:host"];
            var clientPort = Configuration["ClientApp:port"];
            services.AddCors( options => {
                 options.AddPolicy("allow-nglibrary-client-access", 
                    policy => policy.WithOrigins($"http://{clientHost}:{clientPort}")
                    //.AllowAnyMethod()
                );
            });



            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("allow-nglibrary-client-access");

            app.UseMvc();
        }
    }
}
