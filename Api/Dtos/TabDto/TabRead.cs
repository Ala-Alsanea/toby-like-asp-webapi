using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Topy_like_asp_webapi.Api.Dtos.CollectionDto;

namespace Topy_like_asp_webapi.Api.Dtos.TabDto
{
    public class TabRead
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }

        public string Description { get; set; }

        // public User User { get; set; }
        public CollectionRead Collection { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}