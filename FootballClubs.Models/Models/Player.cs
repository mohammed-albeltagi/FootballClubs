using System;
using System.ComponentModel.DataAnnotations;

namespace FootballClubs.Models
{
    public class Player : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public DateTime DateofBirth { get; set; }
        [Required]
        public string Nationality { get; set; }
        public string Image { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; }
    }
}
