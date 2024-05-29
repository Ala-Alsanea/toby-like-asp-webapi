using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Topy_like_asp_webapi.Api.Dtos.CollectionDto;
using Topy_like_asp_webapi.Domain.Entities;
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
            CollectionPaged collectionPaCollectionPaged = _mapper.Map<CollectionPaged>(await _repository.Paged(1, 10, a => a.Space, a => a.User, a => a.Tabs));
            return collectionPaCollectionPaged;
        }
    }
}