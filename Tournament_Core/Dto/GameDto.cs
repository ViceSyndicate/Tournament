using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament_Core.Dto
{
    public class GameDto
    {
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public int TournamentId { get; set; }
        public GameDto()
        {
            
        }
        public GameDto(string title, DateTime startDate, int tournamentId) {
            Title = title;
            StartDate = startDate;
            TournamentId = tournamentId;
        }
    }
}
