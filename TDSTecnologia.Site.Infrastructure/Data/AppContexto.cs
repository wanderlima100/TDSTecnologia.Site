using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TDSTecnologia.Site.Core.Entities;
using TDSTecnologia.Site.Core.Dominio;
using TDSTecnologia.Site.Infrastructure.Map;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TDSTecnologia.Site.Infrastructure.Data
{
    /*public class AppContexto : DbContext */
    public class AppContexto :  IdentityDbContext<Usuario, Permissao, string>
    {
        public AppContexto(DbContextOptions<AppContexto> opcoes) : base(opcoes)
        {
        }

        public DbSet<Curso> CursoDao { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Permissao> Permissoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder
            //.Entity<Curso>()
            //.Property(c => c.Turno)
            //.HasConversion(
            //v => v.ToString(),
            //v => (DomTurno)Enum.Parse(typeof(DomTurno), v));
            //.HasConversion(DominioConverter.ConverterDomTurno());
            modelBuilder.ApplyConfiguration(new CursoMapConfiguration());
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("tds");
            modelBuilder.ApplyConfiguration(new PermissaoMapConfiguration());
            modelBuilder.ApplyConfiguration(new UsuarioMapConfiguration());
        }
    }
}
