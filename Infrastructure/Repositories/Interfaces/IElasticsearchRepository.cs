using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Aggregations;
using Elastic.Clients.Elasticsearch.Fluent;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Topy_like_asp_webapi.Infrastructure.Entities;

namespace Topy_like_asp_webapi.Infrastructure.Repositories.Interfaces
{
    public interface IElasticsearchRepository<T> where T : BaseEntity
    {
        public Task<IEnumerable<T>> SearchAsync(
            Query query= null,
            int size = 10
            );
        public Task<long> GetTotalAsync();
        public Task<Boolean> CreateBulkAsync(List<T> documents);
        public Task<Boolean> CreateOrUpdateAsync(T document);
        public Task<Boolean> DeleteAsync(T document);

        public string GetIndexName();


    }
}