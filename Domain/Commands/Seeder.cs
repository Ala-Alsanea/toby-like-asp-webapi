using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Topt_like_asp_webapi.Domain.DBContexts;
using Topt_like_asp_webapi.Domain.Entities;

namespace Topt_like_asp_webapi.Domain.Commands
{
    public class Seeder(DBContext context)
    {
        private readonly DBContext _context = context;

        public void SeedDataContext()
        {
            var guid1 = Guid.NewGuid();

            User user = new User()
            {
                Id = guid1,
                GoogleId = "122334",
                Name = "test",

            };

            if (!_context.Users.Any())
            {
                _context.Users.Add(user);
                _context.SaveChanges();

                Thread.Sleep(10000);

                User updateUsser = _context.Users.Find(user.Id);
                Console.WriteLine("log | updateUsser.UpdatedAt: {0}",updateUsser.UpdatedAt);
                updateUsser.Name = "ddd";
                _context.Users.Update(updateUsser);
                _context.SaveChanges();
            }


            if (!_context.Spaces.Any())
            {
                Space space = new Space()
                {
                    Title = "space1",
                    User = user

                };
                _context.Spaces.Add(space);
                _context.SaveChanges();

                Thread.Sleep(10000);

                Space updatedSpace = _context.Spaces.FirstOrDefault();
                Console.WriteLine("log | updateSpace.UpdatedAt: {0}",updatedSpace.UpdatedAt);
                updatedSpace.Title = "ddd";
                _context.Spaces.Update(updatedSpace);
                _context.SaveChanges();
            }

        }
    }
}