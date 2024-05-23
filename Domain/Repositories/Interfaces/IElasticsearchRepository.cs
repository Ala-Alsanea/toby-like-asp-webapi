using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch;
using Topy_like_asp_webapi.Domain.Entities.Base;

namespace Topy_like_asp_webapi.Domain.Repositories.Interfaces
{
    public interface IElasticsearchRepository<T> where T : BaseEntity
    {
        public Task<IEnumerable<T>> SearchAsync(string query);
        public Task<Boolean> CreateBulkAsync(List<T> documents);
        public Task<Boolean> CreateOrUpdateAsync(T document);
        public Task<Boolean> DeleteAsync(T document);

    }
}