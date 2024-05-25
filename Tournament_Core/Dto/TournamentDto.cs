using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament_Core.Entities;

namespace Tournament_Core.Dto
{
    public class TournamentDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        private  DateTime EndDate { get; set; }
        public ICollection<Game> Games { get; set; } = new List<Game>();
        public TournamentDto(string title, DateTime startDate) { 
            Title = title;
            StartDate = startDate;
            EndDate = startDate.AddMonths(3);
        }
    }
}
