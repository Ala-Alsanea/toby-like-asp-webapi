using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Topy_like_asp_webapi.Domain.Entities;
using Topy_like_asp_webapi.Domain.Repositories.Interfaces;

namespace Topy_like_asp_webapi.Domain.Services
{
    public class SpaceService(IRepository<Space> repository, ILogger logger)
    {
        private readonly IRepository<Space> repository = repository;
        private readonly ILogger logger = logger;

        // public 

    }
}