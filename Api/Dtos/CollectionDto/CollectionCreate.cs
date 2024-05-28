using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Topy_like_asp_webapi.Api.Dtos.SpaceDto;

namespace Topy_like_asp_webapi.Api.Dtos.CollectionDto
{
    public class CollectionCreate
    {
                public string Title { get; set; }
        // public User User { get; set; }
        public SpaceRead Space { get; set; }
        // public ICollection<TabRead> Tabs { get; set; }
    }
}