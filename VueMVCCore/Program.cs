namespace VueMVCCore
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureAppConfiguration((hostContext, config) =>
                {
                    // delete all default configuration providers
                    config.Sources.Clear();
                    config.SetBasePath(hostContext.HostingEnvironment.ContentRootPath); //path of the physical files
                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true); //common appsettings
                    config.AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", optional: true); //separate Development/Staging/Production appsettings
                    config.AddEnvironmentVariables(); //environment variables rewriting config values from jsons
                });
    }
}