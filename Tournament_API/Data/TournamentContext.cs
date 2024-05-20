using Microsoft.EntityFrameworkCore;

namespace Tournament_API.Data
{
    public class TournamentContext : DbContext
    {
        public TournamentContext(DbContextOptions<TournamentContext> options)
            : base(options)
        {
        }

        public DbSet<Tournament_Core.Entities.Tournament> Tournament { get; set; }
        public DbSet<Tournament_Core.Entities.Game> Game { get; set; }
    }
}
