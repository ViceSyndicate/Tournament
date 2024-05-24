using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament_Core.Dto
{
    public class TournamentUpdateDto
    {
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TournamentUpdateDto() { }

        public TournamentUpdateDto(string title, DateTime startDate)
        {
            Title = title;
            StartDate = startDate;
            EndDate = startDate.AddMonths(3);
        }
    }
}
