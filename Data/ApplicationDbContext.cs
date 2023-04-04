using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetCoreApp.Models;

namespace NetCoreApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        // This will add a table called matches
        // It will add columns to the tables based on the variables declared in the Match model
        public DbSet<Match> Matches { get; set; }

        // This will add a table called matches
        // It will add columns to the tables based on the variables declared in the Match model
        public DbSet<NetCoreApp.Models.League>? League { get; set; }

        // This will add a table called matches
        // It will add columns to the tables based on the variables declared in the Match model
        public DbSet<NetCoreApp.Models.Team>? Team { get; set; }
    }
}