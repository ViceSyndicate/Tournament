using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament_Core.Entities;
using Tournament_Core.Repositories;
using Tournament_Data.Data;

namespace Tournament_Data.Data.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly TournamentApiContext _context;
        public GameRepository(TournamentApiContext context)
        {
            _context = context;
        }

//        System.ObjectDisposedException: 'Cannot access a disposed context instance.
//        A common cause of this error is disposing a context instance that was resolved from
//        dependency injection and then later trying to use the same context instance elsewhere in your application.
//        This may occur if you are calling 'Dispose' on the context instance, or wrapping it in a using statement.
//        If you are using dependency injection, you should let the dependency injection container take care of disposing context instances.
//        Object name: 'TournamentApiContext'.'
        public async void Add(Game game)
        {
            // This crashes if i try to supply an ID in the Post request.
            // Also crashes if you put in the wrong tournamentId
            try
            {
                _context.Game.Add(game);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public async Task<bool> AnyAsync(int id)
        {
            return await _context.Game.AnyAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Game>> GetAllAsync()
        {
            return await _context.Game.ToListAsync();
        }

        public async Task<Game> GetAsync(int id)
        {
            var result = await _context.Game.FindAsync(id);
            return result!;
        }

        public async void Remove(Game game)
        {
            _context.Game.Remove(game);
            await _context.SaveChangesAsync();
        }

        public async void Update(Game game)
        {
            _context.Entry(game).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }
    }
}