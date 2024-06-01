using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Topy_like_asp_webapi.Domain.Enums;
using Topy_like_asp_webapi.Infrastructure.Entities;

namespace Topy_like_asp_webapi.Domain.Entities
{
    [Index(nameof(GoogleId), IsUnique = true)]
    public class User : BaseEntity
    {
        public string Name { get; set; }

        public string GoogleId { get; set; }
        public Role Role { get; set; } = Role.USER;

        public ICollection<Space> Spaces { get; set; }
        public ICollection<Collection> Collections { get; set; }
        public ICollection<Tab> Tabs { get; set; }

        public override string ToString()
        {
            return "\nId: " + Id + "\nName: " + Name + "\nGoogleId: " + GoogleId + "\nRole: " + Role;
        }

    }
}