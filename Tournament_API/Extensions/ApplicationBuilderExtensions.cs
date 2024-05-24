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


                List<Tournament> tournaments = GenerateTournaments();
                await context.AddRangeAsync(tournaments);
                await context.SaveChangesAsync();
                // I have to save the tournaments first in order to set their Id's
                // The Id's are then used by the games as a Foreign Key.
                List<Game> games = GenerateGames();
                List<Tournament> createdTournaments = await context.Tournament.ToListAsync();

                foreach (var tournament in tournaments)
                {
                    foreach (var game in games)
                    {
                        game.TournamentId = tournament.Id;
                    }
                }

                //for (int i = 0; i < tournaments.Count(); i++)
                //{
                //    // I think i am getting a pointer error here.
                //    tournaments[i].Games = games;
                //}

                await context.AddRangeAsync(games);
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
                tournament.StartDate = GetRandomDateTime();
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
                game.Time = GetRandomDateTime();
                games.Add(game);
            }
            return games;
        }
        public static DateTime GetRandomDateTime()
        {
            // Define the start date and end date for the range
            DateTime startDate = new DateTime(1900, 1, 1);
            DateTime endDate = new DateTime(2100, 12, 31);

            // Calculate the range of days
            int range = (endDate - startDate).Days;

            // Generate a random number of days within the range
            Random random = new Random();
            int randomDays = random.Next(range + 1);

            // Add the random number of days to the start date
            DateTime randomDate = startDate.AddDays(randomDays);

            // Generate a random time component (hours, minutes, seconds)
            int randomHours = random.Next(0, 24);
            int randomMinutes = random.Next(0, 60);
            int randomSeconds = random.Next(0, 60);

            // Combine the random date and time
            return randomDate.AddHours(randomHours).AddMinutes(randomMinutes).AddSeconds(randomSeconds);
        }
    }
}
