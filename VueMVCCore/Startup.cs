namespace VueMVCCore
{
    using AutoMapper;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Server.IISIntegration;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using System;
    using System.Net;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;
    using VueMVCCore.Models;

    public class Startup
    {
        // Very important to not have a setter here!
        public IConfiguration Configuration { get; }

        public IWebHostEnvironment HostingEnvironment { get; }

        public Startup(IWebHostEnvironment env, IConfiguration configuration)
        {
            Configuration = configuration;
            HostingEnvironment = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Central connection string
            var cstr = Configuration.GetConnectionString(Constants.DefaultConnectionString);

            // CreateDefaultBuilder in Program.cs adds the logging!

            // All the classes in the pipeline added after will get the AppSettings
            services.Configure<AppSettings>(Configuration.GetSection(Constants.AppSettings));

            // AddDbContext adds the context as Scoped!

            // Add database context for Entity Framework Core
            services.AddDbContext<TestDataContext>(options =>
            {
                options.UseSqlServer(cstr);
            }
            );

            services.AddScoped<UnitOfWork>(provider => new UnitOfWork(provider.GetRequiredService<TestDataContext>()));

            // Windows authentication
            services.AddAuthentication(IISDefaults.AuthenticationScheme);

            // This will auto search for automapper profiles and add them
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // We want authorization, the way to do this is with policies
            services.AddAuthorizationCore(options =>
            {
                options.AddPolicy(Constants.LoggedInPolicyName,
                          policy =>
                          {
                              policy.RequireAuthenticatedUser();
                          });
            });

            // Add MVC
            services.AddControllersWithViews()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null; // properties will appear as in class
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.IgnoreNullValues = false;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStatusCodePages(async context =>
            {
                await Task.Delay(1);
                var response = context.HttpContext.Response;
                var request = context.HttpContext.Request;
                if (!request.Path.Value.StartsWith("/api") &&
                      (response.StatusCode == (int)HttpStatusCode.Unauthorized ||
                          response.StatusCode == (int)HttpStatusCode.Forbidden))
                    response.Redirect($"{request.PathBase}/Home/UnauthorizedRequest");
            });

            // To be able to use static files (css, js, etc...) located in wwwroot directory
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // enable attribute routing
                endpoints.MapDefaultControllerRoute(); // /home/index/id?

                endpoints.MapControllerRoute(
                          name: "apierror",
                          pattern: "api/{*url}",
                          defaults: new { controller = "Home", action = "APINotFound" });
                endpoints.MapFallbackToController("Index", "Home");

            });
        }
    }
}