using Microsoft.EntityFrameworkCore;
using Tournament_Core.Entities;
using Tournament_Data.Data;

namespace Tournament_Core.Repositories
{
    public class TournamentRepository : ITournamentRepository
    {
        private readonly TournamentApiContext _context;
        public TournamentRepository(TournamentApiContext context)
        {
            _context = context;
        }

        public void Add(Tournament tournament)
        {
            _context.Tournament.Add(tournament);
            _context.SaveChangesAsync();
        }

        public async Task<bool> AnyAsync(int id)
        {
            return await _context.Tournament.AnyAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Tournament>> GetAllAsync()
        {
            return await _context.Tournament
                .Include(t => t.Games)
                .ToListAsync();
        }

        public async Task<Tournament> GetAsync(int id)
        {
            var result = await _context.Tournament
                .Include(t => t.Games)
                .FirstOrDefaultAsync(t => t.Id == id);
            return result!;
        }

        public async void Remove(Tournament tournament)
        {
            _context.Tournament.Remove(tournament);
            await _context.SaveChangesAsync();
        }

        public async void Update(Tournament tournament)
        {

            _context.Entry(tournament).State = EntityState.Modified;
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
