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
    internal class GameRepository : IGameRepository
    {
        private readonly TournamentApiContext _context;
        public GameRepository(TournamentApiContext context)
        {
            _context = context;
        }

        public void Add(Game tournament)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Game>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Game> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Game tournament)
        {
            throw new NotImplementedException();
        }

        public void Update(Game tournament)
        {
            throw new NotImplementedException();
        }
    }
}