using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Nexus.Domain.Entities;

namespace Nexus.Infrastructure.Data;

public class NexusDbContext : IdentityDbContext
{
    public NexusDbContext(DbContextOptions<NexusDbContext> options) : base(options)
    {
        
    }

    public DbSet<Jogo> Jogos { get; set; }
    public DbSet<Filme> Filmes { get; set; }
    public DbSet<Genero> Generos { get; set; }
    public DbSet<Avaliacao> Avaliacoes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Jogo>()
            .HasMany(j => j.Generos)
            .WithMany(g => g.Jogos)
            .UsingEntity(j => j.ToTable("JogoGeneros"));
        
        modelBuilder.Entity<Filme>()
            .HasMany(f => f.Generos)
            .WithMany(g => g.Filmes)
            .UsingEntity(j => j.ToTable("FilmeGeneros"));
        
        modelBuilder.Entity<Jogo>()
            .Property(j => j.NotaUsuario)
            .HasPrecision(3, 1);
        
        modelBuilder.Entity<Filme>()
            .Property(f => f.NotaUsuario)
            .HasPrecision(3, 1);
        
        modelBuilder.Entity<Avaliacao>()
            .Property(a => a.Nota)
            .HasPrecision(3, 1);
        
        modelBuilder.Entity<Jogo>()
            .HasIndex(j => j.Titulo);
        
        modelBuilder.Entity<Filme>()
            .HasIndex(f => f.Titulo);
        
        modelBuilder.Entity<Genero>()
            .HasIndex(g => g.Nome)
            .IsUnique();
    }
}
