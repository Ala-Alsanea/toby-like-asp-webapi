using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using PostApi.Infrastructure.Pagination;
using Topy_like_asp_webapi.Api.Dtos.CollectionDto;
using Topy_like_asp_webapi.Domain.Entities;
using Topy_like_asp_webapi.Infrastructure.ErrorHandling;
using Topy_like_asp_webapi.Infrastructure.Repositories.Interfaces;

namespace Topy_like_asp_webapi.Domain.CQRS.Query
{
    public class GetPagedCollectionQuery : IRequest<CollectionPaged>
    {

    }


    public class GetPagedCollectionQueryHandler : IRequestHandler<GetPagedCollectionQuery, CollectionPaged>
    {

        private readonly IRepository<Collection> _repository;
        private readonly IMapper _mapper;

        public GetPagedCollectionQueryHandler(IRepository<Collection> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CollectionPaged> Handle(GetPagedCollectionQuery request, CancellationToken cancellationToken)
        {
            try
            {
                ResultReturn<PagedList<Collection>> result = await _repository.Paged(1, 10, a => a.Space, a => a.User, a => a.Tabs);

                if (result.IsFailure)
                    throw new Exception();

                CollectionPaged collectionPaCollectionPaged = _mapper.Map<CollectionPaged>(result.Value);
                return collectionPaCollectionPaged;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}