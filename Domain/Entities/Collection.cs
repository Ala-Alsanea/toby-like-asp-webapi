using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Topy_like_asp_webapi.Domain.Entities.Base;

namespace Topy_like_asp_webapi.Domain.Entities
{
    public class Collection: BaseEntity
    {
        public string Title { get; set; }

        [DeleteBehavior(DeleteBehavior.ClientCascade)]

        public User User { get; set; }

        [DeleteBehavior(DeleteBehavior.ClientCascade)]

        public Space Space { get; set; }
        public ICollection<Tab> Tabs { get; set; }

    }
}