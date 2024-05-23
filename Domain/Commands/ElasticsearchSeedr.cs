using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch;
using Topy_like_asp_webapi.Domain.Entities;
using Topy_like_asp_webapi.Domain.Repositories.Interfaces;

namespace Topy_like_asp_webapi.Domain.Commands
{
    public class ElasticsearchSeedr(IElasticsearchRepository<User> userElasticsearchRepository, ILogger<ElasticsearchSeedr> logger)
    {
        private readonly IElasticsearchRepository<User> userElasticsearchRepository = userElasticsearchRepository;
        private readonly ILogger<ElasticsearchSeedr> _logger = logger;

        async public Task SeedDataToElasticsearch()
        {

            if (userElasticsearchRepository.SearchAsync("").Result.Count() == 0)
            {

                for (int i = 0; i < 1000; i++)
                {
                    var guid = Guid.NewGuid();
                    User? user = new User()
                    {
                        Id = guid,
                        GoogleId = "122334",
                        Name = "test",

                    };

                    bool res = await userElasticsearchRepository.CreateBulkAsync(new List<User> { user });
                    _logger.LogInformation("from SeedDataToElasticsearch: " + res.ToString());
                    user = null;
                }
                _logger.LogInformation("from SeedDataToElasticsearch: create");

            }



            // IEnumerable<User> users = await userElasticsearchRepository.SearchAsync("");

            // _logger.LogInformation("from SeedDataToElasticsearch: count:" + users.Count().ToString());

            // foreach (User user in users)
            // {
            //     _logger.LogInformation("from SeedDataToElasticsearch: user " + user.ToString());

            // }


            // foreach (User user in users)
            // {
            //     user.Suffix("test");
            //     user.GoogleId = "1";
            //     bool res = await userElasticsearchRepository.CreateOrUpdateAsync(user);
            //     _logger.LogInformation("from SeedDataToElasticsearch: update " + res.ToString());
            // }


            // foreach (User user in users)
            // {
            //     bool res = await userElasticsearchRepository.DeleteAsync(user);
            //     _logger.LogInformation("from SeedDataToElasticsearch: delete " + res.ToString());
            // }

            _logger.LogInformation("from SeedDataToElasticsearch: out");
        }



    }
}