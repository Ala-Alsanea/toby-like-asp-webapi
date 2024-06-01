using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch.Snapshot;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Topy_like_asp_webapi.Domain.Entities;
using Topy_like_asp_webapi.Domain.Providers;
using Topy_like_asp_webapi.Infrastructure.ErrorHandling;
using Topy_like_asp_webapi.Infrastructure.Repositories.Interfaces;

namespace Topy_like_asp_webapi.Domain.CQRS.Command
{
    public class LoginWithJwt : IRequest<ResultReturn<string>>
    {
        public string GoogleId { get; set; }
    }


    public sealed class LoginWithJwtHandler
    (JwtProvider jwtProvider,
    IRepository<User> userRepository) : IRequestHandler<LoginWithJwt, ResultReturn<string>>
    {
        private readonly JwtProvider jwtProLoginWithJwtvider = jwtProvider;
        private readonly IRepository<User> userRepository = userRepository;

        async public Task<ResultReturn<string>> Handle(LoginWithJwt request, CancellationToken cancellationToken)
        {
            User? user = await userRepository.entity.FirstOrDefaultAsync(a=>a.GoogleId == request.GoogleId);
            
            if (user is null)
            {
                return ResultReturn.Fail<string>("invalide cred");
            }

            string token = jwtProvider.Genrate(user);
            return ResultReturn.Ok<string>(token);

        }
    }


}