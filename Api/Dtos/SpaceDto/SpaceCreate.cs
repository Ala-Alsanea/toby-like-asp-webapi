using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Topy_like_asp_webapi.Api.Dtos.CollectionDto;

namespace Topy_like_asp_webapi.Api.Dtos.SpaceDto
{
    public class SpaceCreate
    {
                public string Title { get; set; }

        // public User User { get; set; }

        public ICollection<CollectionRead> Collections { get; set; }
    }
}