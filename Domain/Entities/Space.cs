using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Topt_like_asp_webapi.Domain.Entities.Base;

namespace Topt_like_asp_webapi.Domain.Entities
{
    public class Space : BaseEntity
    {
        public string Title { get; set; }

        [DeleteBehavior(DeleteBehavior.ClientCascade)]
        public User User { get; set; }

        public ICollection<Collection> Collections { get; set; }


    }
}