using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Topy_like_asp_webapi.Domain.Entities;
using Topy_like_asp_webapi.Infrastructure.Repositories.Interfaces;

namespace Topy_like_asp_webapi.Domain.CQRS.Command
{
    public class CreateCollectionCommand : IRequest<bool>
    {
        public string Title { get; set; }

    }


    public class CreateCollectionCommandHandler : IRequestHandler<CreateCollectionCommand, bool>
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

        public async Task<bool> Handle(CreateCollectionCommand request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("hi logger");
            Console.WriteLine("hi console");



            try
            {

                Collection collection = _mapper.Map<Collection>(request);
                
                var spaces = await _spaceRepository.Paged(1,1,a=>a);
                collection.Space = spaces.Items.First();

                var users = await _userRepository.Paged(1,1,a=>a);
                collection.User = users.Items.First();

                await _repository.CreateAsync(collection);

                return await Task.FromResult(true);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while writing "); // Log error with exception

                // Create a user-friendly error message
                var errorMessage = $"An error occurred while writing the data. Please try again later.";

                // Throw an exception with the error message
                throw new Exception(errorMessage);
                throw;
            }
        }
    }
}