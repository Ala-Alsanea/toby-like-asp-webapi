using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Topy_like_asp_webapi.Api.Dtos.CollectionDto;
using Topy_like_asp_webapi.Domain.Entities;
using Topy_like_asp_webapi.Infrastructure.Repositories.Interfaces;

namespace Topy_like_asp_webapi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CollectionController : ControllerBase
    {
        private readonly IRepository<Collection> _repository;
        private readonly IMapper _mapper;

        public CollectionController(IRepository<Collection> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCollections()
        {

            CollectionPaged collectionPaCollectionPaged = _mapper.Map<CollectionPaged>(await _repository.Paged(1, 10,a=>a.Space,a=>a.User,a=>a.Tabs));

            return Ok(collectionPaCollectionPaged);
        }

        [HttpGet("all")]

        public async Task<IActionResult> GetOneCollections()
        {
            var hold = await _repository.AllAsync(a=>a.User);
            CollectionRead col = _mapper.Map<CollectionRead>(hold.First());
            return Ok( col );
        }
    }
}