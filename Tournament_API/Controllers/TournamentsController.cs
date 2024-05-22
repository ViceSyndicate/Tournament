using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tournament_Data.Data;
using Tournament_Core.Entities;
using Tournament_Core.Repositories;

namespace Tournament_API.Controllers
{
    [Route("api/tournaments")]
    [ApiController]
    public class TournamentsController : ControllerBase
    {
        private readonly TournamentApiContext _context;
        ITournamentRepository repository;
        public TournamentsController(TournamentApiContext context)
        {
            _context = context;
            repository = new TournamentRepository(context);
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
