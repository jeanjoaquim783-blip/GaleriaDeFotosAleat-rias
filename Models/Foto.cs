namespace GaleriaFotos.Models;

// Essa classe representa UMA foto no nosso sistema.
// Cada propriedade vira uma "coluna" na tabela do banco de dados.
public class Foto
{
    // Identificador único, gerado automaticamente pelo banco (1, 2, 3...)
    public int Id { get; set; }

    // O nome do arquivo como ele fica salvo no disco (ex: "3f9a2b.jpg")
    // Repare que NÃO é o nome original do arquivo enviado pelo usuário —
    // isso evita que duas pessoas enviando "foto.jpg" causem conflito.
    public string NomeArquivo { get; set; } = string.Empty;

    // Data e hora em que a foto foi enviada
    public DateTime DataUpload { get; set; } = DateTime.UtcNow;

    // Descrição opcional (o "?" indica que pode ser nula/vazia)
    public string? Descricao { get; set; }
}
