using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PostApi.Infrastructure.Pagination;
using Topy_like_asp_webapi.Domain.DBContexts;
using Topy_like_asp_webapi.Domain.Entities;
using Topy_like_asp_webapi.Domain.Repositories.Interfaces;

namespace Topy_like_asp_webapi.Domain.Commands
{
    public class Seeder(
        IRepository<User> UserRepository,
        IRepository<Space> SpaceRepository,
        IRepository<Collection> CollectionRepository,
        ILogger<Seeder> logger
        )
    {
        private readonly IRepository<User> _userRepository = UserRepository;
        private readonly IRepository<Space> _spaceRepository = SpaceRepository;
        private readonly IRepository<Collection> _collectionRepository = CollectionRepository;
        private readonly ILogger<Seeder> _logger = logger;

        // private readonly DBContext _context = context;

        async public Task SeedDataContext()
        {

            // for (int i = 0; i < 12; i++)
            // {
            //     User userLoop = new User()
            //     {
            //         GoogleId = "122334",
            //         Name = "Loop " + i.ToString(),

            //     };

            //     if (i == 0)
            //         userLoop.Role = Enums.Role.ADMIN;

            //     _userRepository.CreateAsync(userLoop).Wait();
            //     _logger.LogInformation(userLoop.ToString());


            //     Space space = new Space()
            //     {
            //         Title = "space " + userLoop.Name,
            //         User = userLoop

            //     };
            //     await _spaceRepository.CreateAsync(space);


            //     Collection collection = new Collection()
            //     {
            //         Title = "collection " + userLoop.Name,
            //         Space = space,
            //         User = userLoop

            //     };
            //     await _collectionRepository.CreateAsync(collection);

            // }



            PagedList<Collection> page = _collectionRepository.Paged(1, 7);
            _logger.LogInformation(page.ToString());
            foreach (Collection item in page.Items)
            {
                _logger.LogInformation(item.ToString());

            }

            PagedList<Collection> page2 = _collectionRepository.Paged(2, 7);
            _logger.LogInformation(page2.ToString());
            foreach (Collection item in page2.Items)
            {
                _logger.LogInformation(item.ToString());

            }



        }
    }
}