using Microsoft.EntityFrameworkCore;
using WebApiAutores.Entities;

namespace WebApiAutores.DataBase
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Autor> Autores { get; set; }
    }
}
