using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament_Core.Entities;
using Tournament_Data.Data;

namespace Tournament_Core.Repositories
{
    internal class TournamentRepository : ITournamentRepository
    {
        private readonly TournamentApiContext _context;
        public TournamentRepository(TournamentApiContext context)
        {
            _context = context;
        }

        public void Add(Tournament tournament)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Tournament>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Tournament> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Tournament tournament)
        {
            throw new NotImplementedException();
        }

        public void Update(Tournament tournament)
        {
            throw new NotImplementedException();
        }
    }
}
