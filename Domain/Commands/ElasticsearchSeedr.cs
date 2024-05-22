using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

            for (int i = 0; i < 1000; i++)
            {
                var guid = Guid.NewGuid();
                User? user = new User()
                {
                    Id = guid,
                    GoogleId = "122334",
                    Name = "test",

                };

                bool res = await userElasticsearchRepository.CreateAsync(new List<User> { user });
                _logger.LogInformation("from SeedDataToElasticsearch: " + res.ToString());
                user = null;
            }

                _logger.LogInformation("from SeedDataToElasticsearch: out" );
        }



    }
}