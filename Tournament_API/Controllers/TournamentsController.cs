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
using Tournament_Data.Data.Repositories;

namespace Tournament_API.Controllers
{
    [Route("api/tournaments")]
    [ApiController]
    public class TournamentsController : ControllerBase
    {
        private readonly TournamentApiContext _context;
        ITournamentRepository repository;
        private readonly IUoW _UoW;
        public TournamentsController(TournamentApiContext context)
        {
            _context = context;
            repository = new TournamentRepository(context);
            _UoW = new UoW(context);
        }

        // GET: api/Tournaments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tournament>>> GetTournament()
        {
            //var data = await repository.GetAllAsync();
            var data = await _UoW.TournamentRepository.GetAllAsync();
            return Ok(data);
        }

        // GET: api/Tournaments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tournament>> GetTournament(int id)
        {
            var tournament = _UoW.TournamentRepository.GetAsync(id);
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

            _UoW.TournamentRepository.Update(tournament);
            return NoContent();
        }

        // POST: api/Tournaments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tournament>> PostTournament(Tournament tournament)
        {
            _UoW.TournamentRepository.Add(tournament);
            // Black Magic
            return CreatedAtAction("GetTournament", new { id = tournament.Id }, tournament);
        }

        // DELETE: api/Tournaments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTournament(int id)
        {
            var tournament = await _UoW.TournamentRepository.GetAsync(id);
            if (tournament == null)
            {
                return NotFound();
            }
            else
            {
                _UoW.TournamentRepository.Remove(tournament);
                return NoContent();
            }
        }

        private bool TournamentExists(int id)
        {
            return _UoW.TournamentRepository.AnyAsync(id).Result;
        }
    }
}
