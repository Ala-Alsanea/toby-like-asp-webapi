using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Topy_like_asp_webapi.Api.Dtos
{
    public class MetadataPagedDto
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public bool HasNext { get; set; }
        public bool HasPrevious { get; set; }
    }
}