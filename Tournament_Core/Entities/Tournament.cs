using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament_Core.Entities
{
    public class Tournament
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "The Tournament needs a Title.")]
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public ICollection<Game> Games { get; set; } = new List<Game>();
    }
}