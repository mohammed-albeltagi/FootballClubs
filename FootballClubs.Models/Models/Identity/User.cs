using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FootballClubs.Models
{
    public class User : BaseEntity
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
