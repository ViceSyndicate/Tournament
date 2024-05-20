using System.Globalization;
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

                /*
                var games = GenerateGames();
                var savedTournaments = context.Tournament;
                foreach (var tournament in savedTournaments) { 
                    foreach (var game in games)
                    {
                        game.TournamentId = tournament.Id;
                    }
                }
                await context.AddRangeAsync(games);
                */
            }
        }

        private static IEnumerable<Tournament> GenerateTournaments()
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

        private static IEnumerable<Game> GenerateGames()
        {
            List<Game> games = new List<Game>();

            for (int j = 0; j < 3; j++)
            {
                Game game = new Game();
                game.Title = "Game Title" + j.ToString();
                game.Time = DateTime.Now;
                games.Add(game);
            }
            return games;
        }
    }
}
