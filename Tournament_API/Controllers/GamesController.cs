using Microsoft.AspNetCore.Mvc;
using Tournament_Core.Entities;
using Tournament_Core.Repositories;
using Tournament_Data.Data;
using Tournament_Data.Data.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tournament_API.Controllers
{
    [Route("api/games")]
    [ApiController]
    public class GamesController : ControllerBase
    {

        private readonly TournamentApiContext _context;
        IGameRepository repository;
        public GamesController(TournamentApiContext context)
        {
            _context = context;
            repository = new GameRepository(context);
        }

        // GET: api/Tournaments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> GetGame()
        {
            //var tournaments = await _context.Tournament.ToListAsync();
            var data = await repository.GetAllAsync();
            return Ok(data);
        }

        // GET: api/Tournaments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> GetGame(int id)
        {
            var game = repository.GetAsync(id);
            if (game == null)
            {
                return NotFound();
            }
            return await game;
        }

        // PUT: api/Tournaments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame(int id, Game game)
        {

            if (id != game.Id && !GameExists(id))
            {
                return BadRequest();
            }

            repository.Update(game);
            return NoContent();
        }

        // POST: api/Tournaments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Game>> PostGame(Game game)
        {
            repository.Add(game);
            // Black Magic
            return CreatedAtAction("GetGame", new { id = game.Id }, game);
        }

        // DELETE: api/Tournaments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            var game = await repository.GetAsync(id);
            if (game == null)
            {
                return NotFound();
            }
            else
            {
                repository.Remove(game);
                return NoContent();
            }
        }

        private bool GameExists(int id)
        {
            return repository.AnyAsync(id).Result;
        }
    }
}
