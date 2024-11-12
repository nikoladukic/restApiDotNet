
using System.ComponentModel.DataAnnotations;

namespace RestApiTemplate.Models.Domain
{
    public class Student
    {
        [Key]
        public string? BrojIndeksa { get; set; }
        public string? Ime { get; set; }
        public string? Prezime { get; set; }
        public Mesto? Grad { get; set; }
        public Fakultet? Fakultet { get; set; }

    }
}
