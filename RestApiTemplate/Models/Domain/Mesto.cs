using System.ComponentModel.DataAnnotations;

namespace RestApiTemplate.Models.Domain
{
    public class Mesto
    {
        [Key]
        public long PostanskiBroj { get; set; }
        public string? Naziv { get; set; }
    }
}
