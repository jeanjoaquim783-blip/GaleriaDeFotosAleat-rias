using Microsoft.EntityFrameworkCore;
using GaleriaFotos.Models;

namespace GaleriaFotos.Data;

// Essa classe representa "o banco de dados" dentro do código.
// Ela herda de DbContext, uma classe pronta do Entity Framework
// que sabe como conversar com o banco.
public class AppDbContext : DbContext
{
    // O construtor recebe as configurações de conexão
    // (isso será configurado no Program.cs)
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // Essa linha diz: "existe uma tabela chamada Fotos,
    // e cada linha dela é representada pela classe Foto"
    public DbSet<Foto> Fotos { get; set; }
}
