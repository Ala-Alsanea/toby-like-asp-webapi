using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Topt_like_asp_webapi.Domain.Commands;

namespace Topt_like_asp_webapi.Config
{
    public class CliRegistry
    {
        public static void Boot(WebApplication app, string[] args)
        {
            if (args.Length == 1 && args[0].ToLower() == "truncate-database")
            {
                TruncateDatabase(app);
                // terminate the application and throw an exception
                app.StopAsync().GetAwaiter().GetResult();
            }

            if (args.Length == 1 && args[0].ToLower() == "seed-data")
            {
                SeedData(app);
                // terminate the application and throw an exception
                app.StopAsync().GetAwaiter().GetResult();
            }
        }

        private static void TruncateDatabase(IHost app)
        {
            var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
            using var scope = scopedFactory?.CreateScope();
            var service = scope?.ServiceProvider.GetService<Truncate>();
            service?.TruncateDatabase();
        }

        private static void SeedData(IHost app)
        {
            var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
            using var scope = scopedFactory?.CreateScope();
            var service = scope?.ServiceProvider.GetService<Seeder>();
            service?.SeedDataContext();
        }
    }
}