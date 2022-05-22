using Casino_Royal_PIA_Back_end.Entidades;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Casino_Royal_PIA_Back_end
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }


        public DbSet<Rifa> Rifas { get; set; }
        public DbSet<Participante> Participantes { get; set; }
        public DbSet<Premio> Premios { get; set; }
        public DbSet<RifaParticipante> RifaParticipantes { get; set; }
        public DbSet<Tarjeta> Tarjetas { get; set; }
    }
}

