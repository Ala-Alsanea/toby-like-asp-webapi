using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PostApi.Infrastructure.Pagination;
using Topy_like_asp_webapi.Domain.DBContexts;
using Topy_like_asp_webapi.Infrastructure.Entities;
using Topy_like_asp_webapi.Infrastructure.ErrorHandling;

namespace Topy_like_asp_webapi.Infrastructure.Repositories.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        public DbSet<T> entity { get; set; }


        public Task<ResultReturn<PagedList<T>>> Paged(int page, int pageSize, params Expression<Func<T, object>>[] includes);
        public Task<ResultReturn<T>>? GetAsync(Guid id);
        public Task<ResultReturn<ICollection<T>>> AllAsync(params Expression<Func<T, object>>[] includes);
        public Task<ResultReturn<int>> CreateAsync(T model);
        public Task<ResultReturn<int>> DeleteAsync(T model);
        public Task<ResultReturn<int>> UpdateAsync(T model);





    }
}