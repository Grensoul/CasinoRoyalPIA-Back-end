using Casino_Royal_PIA_Back_end.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Casino_Royal_PIA_Back_end
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Rifa> Rifas { get; set; }
    }
}

