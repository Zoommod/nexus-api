using System;
using Nexus.Domain.Entities;

namespace Nexus.Infrastructure.Data;

public static class DataSeeder
{
    public static void Seed(NexusDbContext context)
    {
        context.Database.EnsureCreated();

        if (context.Generos.Any())
        {
            Console.WriteLine("✅ Banco já possui dados. Seed ignorado.");
            return;
        }

        Console.WriteLine("🌱 Populando banco com dados iniciais...");

        var generos = new List<Genero>
        {
            new Genero 
            { 
                Id = Guid.NewGuid(), 
                Nome = "RPG", 
                Descricao = "Role-Playing Game - Jogos de interpretação de papéis", 
                DataCriacao = DateTime.UtcNow 
            },
            new Genero 
            { 
                Id = Guid.NewGuid(), 
                Nome = "Ação", 
                Descricao = "Jogos e filmes de ação intensa", 
                DataCriacao = DateTime.UtcNow 
            },
            new Genero 
            { 
                Id = Guid.NewGuid(), 
                Nome = "Aventura", 
                Descricao = "Jogos e filmes focados em exploração e história", 
                DataCriacao = DateTime.UtcNow 
            },
            new Genero 
            { 
                Id = Guid.NewGuid(), 
                Nome = "Terror", 
                Descricao = "Jogos e filmes de horror e suspense psicológico", 
                DataCriacao = DateTime.UtcNow 
            },
            new Genero 
            { 
                Id = Guid.NewGuid(), 
                Nome = "Suspense", 
                Descricao = "Jogos e filmes com tensão e mistério", 
                DataCriacao = DateTime.UtcNow 
            },
            new Genero 
            { 
                Id = Guid.NewGuid(), 
                Nome = "Ficção Científica", 
                Descricao = "Sci-fi - Universos futuristas e tecnológicos", 
                DataCriacao = DateTime.UtcNow 
            },
            new Genero 
            { 
                Id = Guid.NewGuid(), 
                Nome = "Estratégia", 
                Descricao = "Jogos que exigem planejamento e táticas", 
                DataCriacao = DateTime.UtcNow 
            },
            new Genero 
            { 
                Id = Guid.NewGuid(), 
                Nome = "Souls-like", 
                Descricao = "Jogos no estilo Dark Souls - alta dificuldade", 
                DataCriacao = DateTime.UtcNow 
            },
            new Genero 
            { 
                Id = Guid.NewGuid(), 
                Nome = "Plataforma", 
                Descricao = "Jogos de pular e correr", 
                DataCriacao = DateTime.UtcNow 
            },
            new Genero 
            { 
                Id = Guid.NewGuid(), 
                Nome = "FPS", 
                Descricao = "First-Person Shooter - Tiro em primeira pessoa", 
                DataCriacao = DateTime.UtcNow 
            },
            new Genero 
            { 
                Id = Guid.NewGuid(), 
                Nome = "Comédia", 
                Descricao = "Filmes e jogos com foco em humor", 
                DataCriacao = DateTime.UtcNow 
            },
            new Genero 
            { 
                Id = Guid.NewGuid(), 
                Nome = "Drama", 
                Descricao = "Filmes com foco em histórias emocionais", 
                DataCriacao = DateTime.UtcNow 
            }
        };

        context.Generos.AddRange(generos);
        context.SaveChanges();

        Console.WriteLine($"✅ {generos.Count} gêneros criados com sucesso!");
    }
}
