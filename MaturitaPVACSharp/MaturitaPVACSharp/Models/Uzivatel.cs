using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace MaturitaPVACSharp.Models
{
    public class Uzivatel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Jmeno { get; set; }
        [Required]
        public string Heslo { get; set; }
        [Required]
        virtual public List<Clanek> Clanky { get; set; }
    }
}
