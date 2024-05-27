using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Topy_like_asp_webapi.Domain.Entities;
using Topy_like_asp_webapi.Infrastructure.Repositories.Interfaces;

namespace Topy_like_asp_webapi.Domain.Commands
{
    public class ElasticsearchSeedr(
        IElasticsearchRepository<User> userElasticsearchRepository,
        IElasticsearchRepository<Space> spaceElasticsearchRepository,
        ILogger<ElasticsearchSeedr> logger
    )
    {
        private readonly IElasticsearchRepository<User> userElasticsearchRepository = userElasticsearchRepository;
        private readonly IElasticsearchRepository<Space> spaceElasticsearchRepository = spaceElasticsearchRepository;
        private readonly ILogger<ElasticsearchSeedr> _logger = logger;

        async public Task SeedDataToElasticsearch()
        {


            if (await userElasticsearchRepository.GetTotalAsync() == 0)
            {

                for (int i = 0; i < 10001; i++)
                {
                    var guid = Guid.NewGuid();
                    User? user = new User();

                    if (i % 2 == 0)
                    {
                        guid = Guid.NewGuid();
                        user = new User()
                        {
                            Id = guid,
                            GoogleId = new Random().Next(i, i * i).ToString(),
                            Name = Path.GetRandomFileName(),

                        };
                    }
                    else
                    {

                        user = new User()
                        {
                            Id = guid,
                            GoogleId = "122334",
                            Name = "test",

                        };
                    }

                    bool res = await userElasticsearchRepository.CreateBulkAsync(new List<User> { user });
                    _logger.LogInformation("from SeedDataToElasticsearch: " + res.ToString());
                    _logger.LogInformation("from SeedDataToElasticsearch: create");
                    user = null;

                }


            }


            if (await spaceElasticsearchRepository.GetTotalAsync() == 0)
            {

                for (int i = 0; i < 10001; i++)
                {
                    var guid = Guid.NewGuid();
                    Space? space = new Space();

                    guid = Guid.NewGuid();
                    space = new Space()
                    {
                        Id = guid,
                        Title = Path.GetRandomFileName(),
                    };


                    bool res = await spaceElasticsearchRepository.CreateBulkAsync(new List<Space> { space });
                    _logger.LogInformation("from SeedDataToElasticsearch: " + res.ToString());
                    _logger.LogInformation("from SeedDataToElasticsearch: create");
                    space = null;

                }


            }



            // var query = new WildcardQuery(nameof(Space.Title).ToLower());

            // query.Wildcard = "*";
            // query.QueryName = "wildcard";

            // _logger.LogInformation(query.QueryName);
            // _logger.LogInformation(query.Value);
            // _logger.LogInformation(query.Field.ToString());

            // IEnumerable<User> users = await userElasticsearchRepository.SearchAsync(query );
            IEnumerable<Space> spaces = await spaceElasticsearchRepository.SearchAsync();


            foreach (Space s in spaces)
            {
                _logger.LogInformation("from SeedDataToElasticsearch: space: " + s.ToString());
            }

            _logger.LogInformation("from SeedDataToElasticsearch: count:" + spaces.Count().ToString());

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