using Microsoft.AspNetCore.Mvc;
using Tournament_Core.Entities;
using Tournament_Core.Repositories;
using Tournament_Data.Data;
using Tournament_Data.Data.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tournament_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly TournamentApiContext _context;
        // REPLACE WITH IGameRepositoru
        IGameRepository repository;
        public GamesController(TournamentApiContext context)
        {
            _context = context;
            // REPLACE WITH GameRepositoru
            repository = new GameRepository(context);
        }

        // GET: api/Tournaments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tournament>>> GetTournament()
        {
            //var tournaments = await _context.Tournament.ToListAsync();
            var data = await repository.GetAllAsync();
            return Ok(data);
        }

        // GET: api/Tournaments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tournament>> GetTournament(int id)
        {
            var tournament = repository.GetAsync(id);
            if (tournament == null)
            {
                return NotFound();
            }
            return await tournament;
        }

        // PUT: api/Tournaments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTournament(int id, Tournament tournament)
        {

            if (id != tournament.Id && !TournamentExists(id))
            {
                return BadRequest();
            }

            repository.Update(tournament);
            return NoContent();
        }

        // POST: api/Tournaments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tournament>> PostTournament(Tournament tournament)
        {
            repository.Add(tournament);
            // Black Magic
            return CreatedAtAction("GetTournament", new { id = tournament.Id }, tournament);
        }

        // DELETE: api/Tournaments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTournament(int id)
        {
            var tournament = await repository.GetAsync(id);
            if (tournament == null)
            {
                return NotFound();
            }
            else
            {
                repository.Remove(tournament);
                return NoContent();
            }
        }

        private bool TournamentExists(int id)
        {
            return repository.AnyAsync(id).Result;
        }
    }
}
