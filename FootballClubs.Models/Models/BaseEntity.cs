using System.ComponentModel.DataAnnotations;

namespace FootballClubs.Models
{
    public class BaseEntity
    {
        [Key, Required]
        public int Id { get; set; }
    }
}
