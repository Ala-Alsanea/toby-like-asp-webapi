using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Elastic.Clients.Elasticsearch;
using Topy_like_asp_webapi.Domain.Entities;
using Topy_like_asp_webapi.Infrastructure.Repositories.Interfaces;

namespace Topy_like_asp_webapi.Domain.Services
{
    public class SpaceService(IRepository<Space> repository, ILogger<SpaceService> logger)
    {
        private readonly IRepository<Space> repository = repository;
        private readonly ILogger logger = logger;

        public Boolean CreateWithElasticsearch()
        {

            throw new NotImplementedException();
        }


        public Boolean UpdateWithElasticsearch()
        {

            throw new NotImplementedException();
        }

        public Boolean DeleteWithElasticsearch()
        {

            throw new NotImplementedException();
        }

        public IEnumerable<Space> FetchAllWithElasticsearch()
        {

            throw new NotImplementedException();
        }

    }
}