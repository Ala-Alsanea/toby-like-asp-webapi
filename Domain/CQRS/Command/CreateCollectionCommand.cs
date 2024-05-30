using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Topy_like_asp_webapi.Domain.Entities;
using Topy_like_asp_webapi.Infrastructure.ErrorHandling;
using Topy_like_asp_webapi.Infrastructure.Repositories.Interfaces;

namespace Topy_like_asp_webapi.Domain.CQRS.Command
{
    public class CreateCollectionCommand : IRequest<ResultReturn<bool>>
    {
        public string Title { get; set; }

    }


    public class CreateCollectionCommandHandler : IRequestHandler<CreateCollectionCommand, ResultReturn<bool>>
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<Collection> _repository;
        private readonly IRepository<Space> _spaceRepository;
        private readonly IRepository<User> _userRepository;

        public CreateCollectionCommandHandler(
            ILogger<CreateCollectionCommandHandler> logger,
            IMapper mapper,
            IRepository<Collection> repository,
            IRepository<Space> spaceRepository,
            IRepository<User> userRepository

            )
        {
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
            _spaceRepository = spaceRepository;
            _userRepository = userRepository;
        }

        public async Task<ResultReturn<bool>> Handle(CreateCollectionCommand request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("hi logger");
            Console.WriteLine("hi console");

            try
            {

                Collection collection = _mapper.Map<Collection>(request);

                var spaces = await _spaceRepository.Paged(1, 1, a => a.User);
                collection.Space = spaces.Value.Items.First();

                var users = await _userRepository.Paged(1, 1, a => a.Spaces);
                collection.User = users.Value.Items.First();

                ResultReturn<int> res = await _repository.CreateAsync(collection);

                return res.IsFailure ? ResultReturn.Fail<bool>("") : ResultReturn.Ok<bool>(true);

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}