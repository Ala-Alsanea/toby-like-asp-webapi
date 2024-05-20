using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Topt_like_asp_webapi.Domain.Enums;

namespace Topt_like_asp_webapi.Domain.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string name { get; set; }
        public string googleId { get; set; }
        public Role role { get; set; } = Role.USER;


    }
}