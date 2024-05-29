using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Topy_like_asp_webapi.Api.Dtos;
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

            ApiResponse<CollectionPaged> response = new();

            try
            {
                GetPagedCollectionQuery query = new();
                CollectionPaged collectionPaCollectionPaged = await _sender.Send(query);

                response.Data = collectionPaCollectionPaged;
                response.Success = true;


                return Ok(response);
            }
            catch (System.Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCollection([FromBody] CreateCollectionCommand create)
        {
            ApiResponse<bool> response = new();
            
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _mediator.Send(create);

                response.Data = result.Value;
                response.Success = true;
                response.Message = "collection has been created";
                return Ok(response);
            }
            catch (Exception ex)
            {

                response.Message = ex.Message;
                response.Success = false;
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

    }
}