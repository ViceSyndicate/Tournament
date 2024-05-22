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

        public void Add(Tournament tournament)
        {
            _context.Tournament.Add(tournament);
            _context.SaveChanges();
        }

        public Task<bool> AnyAsync(int id)
        {
            return _context.Tournament.AnyAsync(e => e.Id == id);
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

        public void Remove(Tournament tournament)
        {
            _context.Tournament.Remove(tournament);
            _context.SaveChangesAsync();
        }

        public void Update(Tournament tournament)
        {

            _context.Entry(tournament).State = EntityState.Modified;
            try
            {
                _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }
    }
}
