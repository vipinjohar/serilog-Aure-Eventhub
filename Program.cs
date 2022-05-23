using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.EventHubs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var connectionString = "DefaultEndpointsProtocol=https;AccountName=serilogblobloggingname;AccountKey=blablablakey;EndpointSuffix=core.windows.net";

            var connectionString = "DefaultEndpointsProtocol=https;AccountName=serilogdatatest;AccountKey=izu+p70rc6WgSEicmT+dY7s1M+qlXDwx8SJNCcEunOrxi9d+wxaN3oSi9OMpamBiCH/VUUffx/1t397Rsn41Ww==;EndpointSuffix=core.windows.net";
            Log.Logger = new LoggerConfiguration()
                             .WriteTo.AzureBlobStorage(connectionString, storageContainerName: "test", storageFileName: "{yyyy}/{MM}/{dd}/appLogs.log")  
                             .WriteTo.Console()                            
                             .MinimumLevel.Information()
                             .MinimumLevel.Debug()                             
                             .CreateLogger();

            //connectionString, storageContainerName: "app-logs", storageFileName: "{yyyy}/{MM}/{dd}/appLogs.log", 
               // writeInBatches: true, period: TimeSpan.FromSeconds(15), batchPostingLimit: 10)


            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
