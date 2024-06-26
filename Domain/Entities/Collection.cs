using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Topy_like_asp_webapi.Infrastructure.Entities;

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

        public override string ToString()
        {
            return $"\ntitle: {Title} \nuser: {User} \nspace {Space}";
        }

    }
}