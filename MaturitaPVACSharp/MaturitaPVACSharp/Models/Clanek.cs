using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MaturitaPVACSharp.Models
{
    public class Clanek
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nadpis { get; set; }
        [Required]
        public string Datum { get; set; }
        [Required]
        public string Telo { get; set; }
        [Required]
        virtual public Uzivatel Autor { get; set; }
    }
}
