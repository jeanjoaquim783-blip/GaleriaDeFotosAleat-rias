using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GaleriaFotos.Data;
using GaleriaFotos.Models;

namespace GaleriaFotos.Controllers;

// [ApiController] avisa o ASP.NET Core que essa classe responde
// a requisições da API (e trata erros de validação automaticamente)
[ApiController]
// [Route] define o prefixo da URL: tudo aqui começa com /api/fotos
[Route("api/fotos")]
public class FotosController : ControllerBase
{
    // O banco de dados é "injetado" automaticamente aqui pelo ASP.NET Core
    private readonly AppDbContext _db;
    private readonly IWebHostEnvironment _env;

    public FotosController(AppDbContext db, IWebHostEnvironment env)
    {
        _db = db;
        _env = env;
    }

    // GET /api/fotos
    // Retorna a lista de todas as fotos cadastradas, mais recentes primeiro
    [HttpGet]
    public async Task<IActionResult> ListarFotos()
    {
        var fotos = await _db.Fotos
            .OrderByDescending(f => f.DataUpload)
            .ToListAsync();

        return Ok(fotos);
    }

    // GET /api/fotos/aleatoria
    // Retorna UMA foto escolhida aleatoriamente
    [HttpGet("aleatoria")]
    public async Task<IActionResult> FotoAleatoria()
    {
        var total = await _db.Fotos.CountAsync();

        if (total == 0)
        {
            return NotFound(new { mensagem = "Nenhuma foto cadastrada ainda." });
        }

        // Sorteia um número entre 0 e o total de fotos
        var indiceAleatorio = new Random().Next(total);

        var foto = await _db.Fotos
            .Skip(indiceAleatorio) // pula até o índice sorteado
            .FirstAsync();          // pega essa foto

        return Ok(foto);
    }

    // POST /api/fotos
    // Recebe um arquivo enviado pelo formulário e salva ele
    [HttpPost]
    public async Task<IActionResult> UploadFoto(IFormFile arquivo)
    {
        // Validação básica: o arquivo foi mesmo enviado?
        if (arquivo == null || arquivo.Length == 0)
        {
            return BadRequest(new { mensagem = "Nenhum arquivo enviado." });
        }

        // Passo 1: gerar um nome único para não sobrescrever arquivos
        var extensao = Path.GetExtension(arquivo.FileName); // ex: ".jpg"
        var nomeUnico = $"{Guid.NewGuid()}{extensao}";

        // Passo 2: montar o caminho físico onde o arquivo vai ser salvo
        var pastaUploads = Path.Combine(_env.WebRootPath, "uploads");
        var caminhoCompleto = Path.Combine(pastaUploads, nomeUnico);

        // Passo 3: salvar o arquivo fisicamente no disco
        using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
        {
            await arquivo.CopyToAsync(stream);
        }

        // Passo 4: salvar o registro no banco de dados
        var foto = new Foto
        {
            NomeArquivo = nomeUnico,
            DataUpload = DateTime.UtcNow
        };

        _db.Fotos.Add(foto);
        await _db.SaveChangesAsync();

        return Ok(foto);
    }

    // DELETE /api/fotos/{id}
    // Remove uma foto do banco e do disco
    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoverFoto(int id)
    {
        var foto = await _db.Fotos.FindAsync(id);

        if (foto == null)
        {
            return NotFound();
        }

        // Remove o arquivo físico, se existir
        var caminho = Path.Combine(_env.WebRootPath, "uploads", foto.NomeArquivo);
        if (System.IO.File.Exists(caminho))
        {
            System.IO.File.Delete(caminho);
        }

        _db.Fotos.Remove(foto);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}
