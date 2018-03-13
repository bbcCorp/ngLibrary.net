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

using AutoMapper;

using NLog.Web;
using NLog.Extensions.Logging;

using ngLibrary.Model;
using ngLibrary.Core;
using ngLibrary.Data;
using ngLibrary.Web.Models.ViewModels;

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
            // Use Postgres Repos
            services.AddScoped<IBookRepository, ngLibrary.Data.Postgres.PgBookRepository>();


            services.AddAutoMapper(cfg =>
            {
                // Application entity model - view model mappings
                cfg.CreateMap<Book, BookViewModel>().ReverseMap();
            });

            // Add application services.
            services.AddTransient<IEmailSender, EmailService>();

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
