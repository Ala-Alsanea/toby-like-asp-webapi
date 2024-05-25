using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Topy_like_asp_webapi.Domain.Commands;

namespace Topy_like_asp_webapi.Config
{
    public class CliRegistry
    {
        async public static void Boot(WebApplication app, string[] args)
        {
            if (args.Length == 1 && args[0].ToLower() == "truncate-db")
            {
                TruncateDatabase(app);
                // terminate the application and throw an exception
                app.StopAsync().GetAwaiter().GetResult();
            }

            if (args.Length == 1 && args[0].ToLower() == "seed-db")
            {
                await SeedData(app);
                // terminate the application and throw an exception
                app.StopAsync().GetAwaiter().GetResult();
            }

            if (args.Length == 1 && args[0].ToLower() == "seed-es")
            {
                await SeedDataToElasticsearch(app);
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

        async private static Task SeedData(IHost app)
        {
            var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
            using var scope = scopedFactory?.CreateScope();
            var service = scope?.ServiceProvider.GetService<Seeder>();
            await service?.SeedDataContext();
        }
        async private static Task SeedDataToElasticsearch(IHost app)
        {
            var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
            using var scope = scopedFactory?.CreateScope();
            var service = scope?.ServiceProvider.GetService<ElasticsearchSeedr>();
            await service?.SeedDataToElasticsearch();
        }



    }
}