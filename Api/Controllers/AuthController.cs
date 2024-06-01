using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Topy_like_asp_webapi.Api.Dtos;
using Topy_like_asp_webapi.Domain.CQRS.Command;
using Topy_like_asp_webapi.Infrastructure.ErrorHandling;

namespace Topy_like_asp_webapi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator mediator = mediator;

        [HttpPost]
        async public Task<IActionResult> LoginWithGoogleId([FromBody] LoginWithJwt req)
        {
            ApiResponse<string> response = new();

            ResultReturn<string> result = await mediator.Send(req);

            if( result.IsFailure)
            {
                response.Success = false;
                response.Message = result.Error;

                return StatusCode(StatusCodes.Status404NotFound,response);
            }

            response.Success = true;
            response.Data = result.Value;

            return Ok(response);
        }
    }
}