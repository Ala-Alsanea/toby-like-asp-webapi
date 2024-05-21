using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PostApi.Infrastructure.Pagination;
using Topt_like_asp_webapi.Domain.DBContexts;

namespace Topt_like_asp_webapi.Domain.Repositories.Interfaces
{
    public interface IRepository<T>
    {

        // PagedList<T> All(int page, int pageSize);
        public PagedList<T> All(int page, int pageSize)
        {
            throw new NotImplementedException();
        }
        public void Create(T model)
        {
            throw new NotImplementedException();
        }

        public void Delete(T model)
        {
            throw new NotImplementedException();
        }

        public T? Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Index()
        {
            throw new NotImplementedException();
        }

        public void Update(T model)
        {
            throw new NotImplementedException();
        }


        
    }
}