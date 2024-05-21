using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Topy_like_asp_webapi.Domain.Entities.Base;
using Topy_like_asp_webapi.Domain.Enums;

namespace Topy_like_asp_webapi.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string GoogleId { get; set; }
        public Role Role { get; set; } = Role.USER;

        public ICollection<Space> Spaces { get; set; }
        public ICollection<Collection> Collections { get; set; }
        public ICollection<Tab> Tabs { get; set; }

    }
}