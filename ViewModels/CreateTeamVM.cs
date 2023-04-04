using NetCoreApp.Data;
using System.ComponentModel.DataAnnotations;

namespace NetCoreApp.ViewModels
{
    public class CreateTeamVM
    {
        public int LeagueId { get; set; }
        public string LeagueName { get; set; } = string.Empty;
        [Required]
        public string TeamName { get; set; } = string.Empty;
    }
}
