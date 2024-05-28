using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Topy_like_asp_webapi.Api.Dtos.SpaceDto;
using Topy_like_asp_webapi.Api.Dtos.TabDto;
using Topy_like_asp_webapi.Domain.Entities;

namespace Topy_like_asp_webapi.Api.Dtos.CollectionDto
{
    public class CollectionRead
    {

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string User { get; set; }
        public string Space { get; set; }
        public ICollection<TabRead> Tabs { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
 
    }
}