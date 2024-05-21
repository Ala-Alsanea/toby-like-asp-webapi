using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Topt_like_asp_webapi.Domain.Entities.Base
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
       
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
