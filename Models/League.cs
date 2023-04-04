using System.ComponentModel.DataAnnotations;

namespace NetCoreApp.Models
{
    public class League
    {
        [Key]
        public int Id { get; set; }

        public DateTime CreatedDateTime { get; set; } = DateTime.Now;

        [Required]
        public string Name { get; set; }

        [Required]
        public int numTeamsToStart { get; set; } = 4;
        
    }
}
