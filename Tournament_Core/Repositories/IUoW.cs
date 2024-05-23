﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament_Core.Repositories
{
    public interface IUoW
    {
        public ITournamentRepository TournamentRepository { get; }
        public IGameRepository GameRepository { get; }

        Task CompleteAsync();
    }
}
