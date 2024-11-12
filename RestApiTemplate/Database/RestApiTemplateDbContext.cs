using Microsoft.EntityFrameworkCore;
using RestApiTemplate.Models.Domain;

namespace RestApiTemplate.Database
{
    public class RestApiTemplateDbContext : DbContext
    {
        public RestApiTemplateDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Fakultet> Fakultet { get; set; }
        public DbSet<Mesto> Mesto { get; set; }
        public DbSet<Student> Student { get; set; }



    }
}
