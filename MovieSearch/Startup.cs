using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MovieSearch.Core.Services;
using MovieSearch.Filters;
using Newtonsoft.Json;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;

namespace MovieSearch
{
    public class Startup
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        public Startup(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration().WriteTo.File("log.txt").CreateLogger();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddCors()
                .AddMvc(options => options.Filters.Add(new CommonExceptionFilter()))
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
                });

            // I used Redis cache but i encountered with firewall
            // problems when Redis connection on windows.
            // For this reason i am using distributed memory caching for this project.
            services.AddDistributedMemoryCache();

            // Register the Swagger generator, defining one or more Swagger documents.
            services.AddSwaggerGen(options => 
                options.SwaggerDoc("moviesearch", new Info { Title = "Movie Search", Version = "v1" }));

            // Configure other custom services.
            ContainerSetup.Setup(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IServiceProvider serviceProvider, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddSerilog();

            app.UseHsts()
                .UseCors(options => options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin())
                .UseAuthentication()
                .UseMvc();

            // Enable middleware to serve generated Swagger as a JSON endpoint and swagger-ui (HTML, JS, CSS, etc.)
            // Specifying the Swagger JSON endpoint.
            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/moviesearch/swagger.json", "Movie Search API V1"));

            // We are starting a background process, will be running all the life of the program
            // and updates the movie database periodically every 10 minutes.
            serviceProvider.GetService<IMovieDbUpdateService>().StartBackgroundUpdateProcess();
        }
    }
}
