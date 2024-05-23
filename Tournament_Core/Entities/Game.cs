using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament_Core.Entities
{
    public class Game
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "The Game needs a Title.")]
        public string Title { get; set; }
        public DateTime Time { get; set; }
        [ForeignKey("Tournament")]
        public int TournamentId { get; set; }
    }
}