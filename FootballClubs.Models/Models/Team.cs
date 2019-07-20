using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FootballClubs.Models
{
    public class Team : BaseEntity
    {
        public Team()
        {
            Players = new HashSet<Player>();
        }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Country { get; set; }
        public DateTime FoundationDate { get; set; }
        [Required]
        public string CoachName { get; set; }
        public string LogoImage { get; set; }
        public virtual ICollection<Player> Players { get; set; }
    }
}
