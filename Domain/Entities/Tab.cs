using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Topy_like_asp_webapi.Domain.Entities.Base;

namespace Topy_like_asp_webapi.Domain.Entities
{
    public class Tab : BaseEntity
    {
        public string Title { get; set; }
        public string Url { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set; }

        [DeleteBehavior(DeleteBehavior.ClientCascade)]
        public User User { get; set; }
        [DeleteBehavior(DeleteBehavior.ClientCascade)]
        public Collection Collection { get; set; }
    }
}