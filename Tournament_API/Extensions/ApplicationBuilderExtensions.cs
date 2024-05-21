using Microsoft.EntityFrameworkCore;
using Tournament_Core.Entities;
using Tournament_Data.Data;

namespace Tournament_API.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static async void SeedDataAsync(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var context = serviceProvider.GetRequiredService<TournamentApiContext>();

                var tournaments = GenerateTournaments();
                await context.AddRangeAsync(tournaments);
                await context.SaveChangesAsync();

                var savedTournaments = context.Tournament.ToListAsync();

                List<Game> games = GenerateGames();

                foreach (Tournament tournament in tournaments)
                {
                    foreach (Game game in games)
                    {
                        // add FK TournamentId to the game.
                        game.TournamentId = tournament.Id;
                    }
                    await context.AddRangeAsync(games);
                }
                await context.SaveChangesAsync();
            }
        }

        private static List<Tournament> GenerateTournaments()
        {
            var tournaments = new List<Tournament>();
            for (int i = 0; i < 5; i++)
            {
                Tournament tournament = new Tournament();
                tournament.Title = "Tournament Title " + i.ToString();
                tournament.StartDate = DateTime.Now;
                tournaments.Add(tournament);
            }
            return tournaments;
        }

        private static List<Game> GenerateGames()
        {
            List<Game> games = new List<Game>();

            for (int j = 0; j < 3; j++)
            {
                Game game = new Game();
                game.Title = "Game Title " + j.ToString();
                game.Time = DateTime.Now;
                games.Add(game);
            }
            return games;
        }
    }
}
