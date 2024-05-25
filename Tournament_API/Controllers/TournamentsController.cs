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
using AutoMapper;
using Tournament_Core.Dto;
using Azure;
using Microsoft.AspNetCore.JsonPatch;

namespace Tournament_API.Controllers
{
    [Route("api/tournaments")]
    [ApiController]
    public class TournamentsController : ControllerBase
    {
        private readonly TournamentApiContext _context;
        private readonly IMapper mapper;
        ITournamentRepository repository;
        private readonly IUoW _UoW;
        public TournamentsController(TournamentApiContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            repository = new TournamentRepository(context);
            _UoW = new UoW(context);
        }

        // GET: api/Tournaments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tournament>>> GetTournament(bool includeGames)
        {
            var data = await _UoW.TournamentRepository.GetAllAsync();
            if (includeGames)
            {
                return Ok(mapper.Map<IEnumerable<TournamentDto>>(data));
            }
            else
            {
                foreach (var tournament in data)
                {
                    tournament.Games = null;
                }
                return Ok(mapper.Map<IEnumerable<TournamentDto>>(data));
            }
        }

        // GET: api/Tournaments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tournament>> GetTournament(int id)
        {
            var tournament = await _UoW.TournamentRepository.GetAsync(id);
            if (tournament == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<TournamentDto>(tournament));
        }

        // PUT: api/Tournaments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTournament(int id, Tournament tournament)
        {

            if (id != tournament.Id && !TournamentExists(id).Result)
            {
                return BadRequest();
            }

            _UoW.TournamentRepository.Update(tournament);
            return NoContent();
        }


        [HttpPatch("{id}")]
        public async Task<ActionResult<TournamentUpdateDto>> PatchTournament(int id, JsonPatchDocument<TournamentUpdateDto> patchDocument)
        {
            if (!TournamentExists(id).Result)
            {
                return NotFound();
            }

            var tournament = await repository.GetAsync(id);
            TournamentUpdateDto tournamentUpdateDto = mapper.Map<TournamentUpdateDto>(tournament);

            patchDocument.ApplyTo(tournamentUpdateDto, ModelState);

            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (!TryValidateModel(tournamentUpdateDto)) return BadRequest(ModelState);

            return NoContent();
        }

        // POST: api/Tournaments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tournament>> PostTournament(TournamentDto tournamentDto)
        {
            var tournament = new Tournament(tournamentDto.Title, tournamentDto.StartDate);
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

        private Task<bool> TournamentExists(int id)
        {
            return _UoW.TournamentRepository.AnyAsync(id);
        }
    }
}
