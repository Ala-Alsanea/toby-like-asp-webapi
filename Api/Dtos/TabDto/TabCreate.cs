using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Topy_like_asp_webapi.Api.Dtos.TabDto
{
    public class TabCreate
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }

    }
}