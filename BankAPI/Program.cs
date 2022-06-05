using Domain.Services;
using Infra.DataBase;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BankAPI
{
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
                    //STARTING SERVICE
                    new Task(async () =>
                    {
                        while (true)
                        {
                            Console.WriteLine("Starting Service Conneck Bank");
                            await IntegrationService.Startntegration();
                            Thread.Sleep(3600000);//1 Hora
                        }
                    }).Start();

                    webBuilder.UseStartup<Startup>();
                });
    }
}
