using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PostApi.Infrastructure.Pagination;
using Topy_like_asp_webapi.Domain.DBContexts;
using Topy_like_asp_webapi.Infrastructure.Entities;

namespace Topy_like_asp_webapi.Infrastructure.Repositories.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        public DbSet<T> entity { get; set; }


        public Task<PagedList<T>> Paged(int page, int pageSize, params Expression<Func<T, object>>[] includes);
        public Task<T>? GetAsync(Guid id);
        public Task<IEnumerable<T>> AllAsync(params Expression<Func<T, object>>[] includes);
        public Task<bool> CreateAsync(T model);
        public Task<bool> DeleteAsync(T model);
        public Task<bool> UpdateAsync(T model);





    }
}