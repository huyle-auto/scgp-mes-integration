
using Microsoft.EntityFrameworkCore;
using SCGP.MesIntegration.Service.Models;

namespace SCGP.MesIntegration.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.AddJsonFile("appsettings.Secrets.json", optional: true, reloadOnChange: true);

            #region Register Windows Service

            builder.Host.UseWindowsService();

            #endregion

            #region MSSQL
            var connString = builder.Configuration["MSSQL:connString"];

            builder.Services.AddDbContextFactory<ProductionDbContext>(options =>
            {
                options.UseSqlServer(connString);
            });
            #endregion

            var app = builder.Build();

            app.MapGet("/", () => "SCGP.MesIntegration.Service is running...");

            app.Run();
        }
    }
}
