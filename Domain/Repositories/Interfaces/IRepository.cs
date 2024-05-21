using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PostApi.Infrastructure.Pagination;
using Topt_like_asp_webapi.Domain.DBContexts;
using Topt_like_asp_webapi.Domain.Entities.Base;

namespace Topt_like_asp_webapi.Domain.Repositories.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        public DbSet<T> entity { get; set; }


        // PagedList<T> All(int page, int pageSize);
        public PagedList<T> All(int page, int pageSize);

        public void Create(T model);

        public void Delete(T model);

        public T? Get(Guid id);

        public IEnumerable<T> Index();

        public void Update(T model);



    }
}