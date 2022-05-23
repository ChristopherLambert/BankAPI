using Domain.Services;
using Infra.DataBase;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
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
                    Console.WriteLine("SECONDS");
                    Console.WriteLine(DateTime.Now.Second.ToString());

                    //STARTING ORACLE
                    //OracleDB oracleDB = new OracleDB();
                    //OracleDB.GetFINTitulo();

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
