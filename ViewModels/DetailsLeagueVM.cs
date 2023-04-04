using NetCoreApp.Models;

namespace NetCoreApp.ViewModels
{
    public class DetailsLeagueVM
    {
        public League league { get; set; }

        public List<Team> teams { get; set; }
    }
}
