using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Topy_like_asp_webapi.Api.Dtos.CollectionDto;
using Topy_like_asp_webapi.Domain.CQRS.Command;
using Topy_like_asp_webapi.Domain.CQRS.Query;
using Topy_like_asp_webapi.Domain.Entities;
using Topy_like_asp_webapi.Infrastructure.Repositories.Interfaces;

namespace Topy_like_asp_webapi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CollectionController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ISender _sender;

        public CollectionController(IMediator mediator, ISender sender)
        {
            _mediator = mediator;
            _sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> GetPagedCollections()
        {

            try
            {
                GetPagedCollectionQuery query = new();
                CollectionPaged collectionPaCollectionPaged = await _sender.Send(query);

                return Ok(collectionPaCollectionPaged);
            }
            catch (System.Exception ex)
            {

                return new ObjectResult(
                    new { error = ex.Message })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateCollection([FromBody] CreateCollectionCommand create)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                  await _mediator.Send(create);

                // await Task.Delay(1);
                return Ok();
            }
            catch (System.Exception ex)
            {

                return new ObjectResult(
                    new { error = ex.Message })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

    }
}