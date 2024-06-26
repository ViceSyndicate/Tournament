﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament_Core.Entities;

namespace Tournament_Core.Repositories
{
    public interface IGameRepository
    {
        Task<IEnumerable<Game>> GetAllAsync();
        Task<Game> GetAsync(int id);
        Task<IEnumerable<Game>> GetAsync(string title);
        Task<bool> AnyAsync(int id);
        void Add(Game tournament);
        void Update(Game tournament);
        void Remove(Game tournament);
    }
}
