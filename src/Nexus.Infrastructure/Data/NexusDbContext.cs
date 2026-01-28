using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Nexus.Domain.Entities;
using Nexus.Infrastructure.Identity;

namespace Nexus.Infrastructure.Data;

public class NexusDbContext : IdentityDbContext<ApplicationUser>
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

        modelBuilder.Entity<Jogo>(entity =>
        {
            entity.ToTable("Jogos");
            entity.HasKey(j => j.Id);
            entity.Property(j => j.Titulo).IsRequired().HasMaxLength(200);
            entity.Property(j => j.UsuarioId).IsRequired();

            entity.HasOne<ApplicationUser>()
                  .WithMany()
                  .HasForeignKey(j => j.UsuarioId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(j => j.Generos)
                  .WithMany(g => g.Jogos)
                  .UsingEntity<Dictionary<string, object>>(
                      "JogoGeneros",
                      j => j.HasOne<Genero>().WithMany().HasForeignKey("GeneroId"),
                      g => g.HasOne<Jogo>().WithMany().HasForeignKey("JogoId")
                  );
        });

        modelBuilder.Entity<Filme>(entity =>
        {
            entity.ToTable("Filmes");
            entity.HasKey(f => f.Id);
            entity.Property(f => f.Titulo).IsRequired().HasMaxLength(200);
            entity.Property(f => f.UsuarioId).IsRequired();

            entity.HasOne<ApplicationUser>()
                  .WithMany()
                  .HasForeignKey(f => f.UsuarioId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(f => f.Generos)
                  .WithMany(g => g.Filmes)
                  .UsingEntity<Dictionary<string, object>>(
                      "FilmeGeneros",
                      f => f.HasOne<Genero>().WithMany().HasForeignKey("GeneroId"),
                      g => g.HasOne<Filme>().WithMany().HasForeignKey("FilmeId")
                  );
        });

        modelBuilder.Entity<Genero>(entity =>
        {
            entity.ToTable("Generos");
            entity.HasKey(g => g.Id);
            entity.Property(g => g.Nome).IsRequired().HasMaxLength(100);
            entity.HasIndex(g => g.Nome).IsUnique();
        });

        modelBuilder.Entity<Avaliacao>(entity =>
        {
            entity.ToTable("Avaliacoes");
            entity.HasKey(a => a.Id);
            entity.Property(a => a.Conteudo).IsRequired().HasMaxLength(2000);
            entity.Property(a => a.UsuarioId).IsRequired();

            entity.HasOne<ApplicationUser>()
                  .WithMany()
                  .HasForeignKey(a => a.UsuarioId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(a => a.Jogo)
                  .WithMany(a => a.Avaliacoes)
                  .HasForeignKey(a => a.JogoId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(a => a.Filme)
                  .WithMany(a => a.Avaliacoes)
                  .HasForeignKey(a => a.FilmeId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
