using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Topy_like_asp_webapi.Api.Dtos.CollectionDto
{
    public class CollectionPaged
    {
        public ICollection<CollectionRead> Items { get; set; }
        public MetadataPagedDto MetaData { get; set; }
    }
}