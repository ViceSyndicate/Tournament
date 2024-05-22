using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public async void Add(Tournament tournament)
        {
            _context.Tournament.Add(tournament);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> AnyAsync(int id)
        {
            return await _context.Tournament.AnyAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Tournament>> GetAllAsync()
        {
            return await _context.Tournament.ToListAsync();
        }

        public async Task<Tournament> GetAsync(int id)
        {
            var result = await _context.Tournament.FindAsync(id);
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
