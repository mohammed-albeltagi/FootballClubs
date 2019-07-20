using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FootballClubs.Models
{
    public class Role : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
