using System.ComponentModel.DataAnnotations;

namespace NetCoreApp.Models
{
    public class Team
    {
        [Key]
        public int Id { get; set; }

        public DateTime CreatedDateTime { get; set; } = DateTime.Now;

        [Required]
        public int LeagueID { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
