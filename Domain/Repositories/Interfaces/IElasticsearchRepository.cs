using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch;
using Topy_like_asp_webapi.Domain.Entities.Base;

namespace Topy_like_asp_webapi.Domain.Repositories.Interfaces
{
    public interface IElasticsearchRepository <T> where T : BaseEntity
    {
        Task<T> GetAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> SearchAsync(string query);
        Task<Boolean> CreateAsync(List<T> documents);
        Task<Boolean> UpdateAsync(T document);
        Task<Boolean> DeleteAsync(Guid id);

    }
}