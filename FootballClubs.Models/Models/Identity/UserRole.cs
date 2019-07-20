using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballClubs.Models
{

    public class UserRole : BaseEntity
    {

        [Required, ForeignKey(nameof(User))]
        public int UserId { get; set; }

        [Required, ForeignKey(nameof(Role))]
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual User User { get; set; }
    }
}
