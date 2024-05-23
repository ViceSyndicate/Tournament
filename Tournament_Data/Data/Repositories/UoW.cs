using Tournament_Core.Repositories;

namespace Tournament_Data.Data.Repositories
{
    public class UoW : IUoW
    {
        private readonly TournamentApiContext _context;

        ITournamentRepository IUoW.TournamentRepository => TournamentRepository();

        IGameRepository IUoW.GameRepository => GameRepository();

        public UoW(TournamentApiContext context)
        {
            _context = context;
        }
        public ITournamentRepository TournamentRepository()    
        {
            ITournamentRepository repository = new TournamentRepository(_context);
            return repository;
        }

        public IGameRepository GameRepository () {

            IGameRepository repository = new GameRepository(_context);
            return repository;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        Task IUoW.CompleteAsync()
        {
            throw new NotImplementedException();
        }
    }
}
