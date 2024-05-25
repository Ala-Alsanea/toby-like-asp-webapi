using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PostApi.Infrastructure.Pagination;
using Topy_like_asp_webapi.Domain.DBContexts;
using Topy_like_asp_webapi.Domain.Entities.Base;

namespace Topy_like_asp_webapi.Domain.Repositories.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        public DbSet<T> entity { get; set; }


        public PagedList<T> Paged(int page, int pageSize);
        public Task<T>? GetAsync(Guid id);
        public Task<IEnumerable<T>> AllAsync();
        public Task<bool> CreateAsync(T model);
        public Task<bool> DeleteAsync(T model);
        public Task<bool> UpdateAsync(T model);





    }
}