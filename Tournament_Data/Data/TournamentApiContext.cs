using Microsoft.EntityFrameworkCore;

namespace Tournament_Data.Data
{
    public class TournamentApiContext : DbContext
    {
        public TournamentApiContext(DbContextOptions<TournamentApiContext> options)
            : base(options) { }

        public DbSet<Tournament_Core.Entities.Tournament> Tournament { get; set; }
        public DbSet<Tournament_Core.Entities.Game> Game { get; set; }

        public TournamentApiContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<TournamentApiContext>();
            options.UseSqlServer("TournamentApiContext");

            return new TournamentApiContext(options.Options);
        }
    }
}
