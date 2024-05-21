using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Topt_like_asp_webapi.Domain.DBContexts;
using Topt_like_asp_webapi.Domain.Entities;
using Topt_like_asp_webapi.Domain.Repositories.Interfaces;

namespace Topt_like_asp_webapi.Domain.Commands
{
    public class Seeder(
        IRepository<User> UserRepository,
        IRepository<Space> SpaceRepository
        )
    {
        private readonly IRepository<User> _userRepository = UserRepository;
        private readonly IRepository<Space> _spaceRepository = SpaceRepository;

        // private readonly DBContext _context = context;

        async public void SeedDataContext()
        {
            var guid1 = Guid.NewGuid();

            User user = new User()
            {
                Id = guid1,
                GoogleId = "122334",
                Name = "test",

            };


            if (!_userRepository.entity.Any())
            {
                _userRepository.Create(user);

                Thread.Sleep(10000);

                User updateUsser = _userRepository.Get(user.Id);
                Console.WriteLine("log | updateUsser.UpdatedAt: {0}", updateUsser.UpdatedAt);
                updateUsser.Name = "ddd";
                _userRepository.Update(user);

            }


            if (!_spaceRepository.entity.Any())
            {
                Space space = new Space()
                {
                    Title = "space1",
                    User = user

                };
                _spaceRepository.Create(space);

                Thread.Sleep(10000);

                Space updatedSpace = _spaceRepository.entity.FirstOrDefault();
                Console.WriteLine("log | updateSpace.UpdatedAt: {0}", updatedSpace.UpdatedAt);
                updatedSpace.Title = "ddd";

                _spaceRepository.Update(updatedSpace);
            }



            // User userLoop = new User()
            // {
            //     GoogleId = "122334",
            //     Name = "test",

            // };


            // for (int i = 0; i < 12; i++)
            // {
            //     _context.Users.Add(userLoop);
            //     await _context.SaveChangesAsync();
            // }

        }
    }
}