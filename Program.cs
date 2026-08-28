using Microsoft.EntityFrameworkCore;
using GaleriaFotos.Data;

// "builder" é o objeto que vai juntar todas as configurações
// antes de a aplicação realmente começar a rodar.
var builder = WebApplication.CreateBuilder(args);

// Registra o suporte a Controllers (as classes com as rotas da API)
builder.Services.AddControllers();

// Registra o AppDbContext, dizendo pra ele usar SQLite
// e salvar o banco num arquivo chamado "galeria.db"
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=galeria.db"));

// Constrói a aplicação com tudo que foi configurado acima
var app = builder.Build();

// Permite servir arquivos estáticos (HTML, CSS, JS) da pasta wwwroot
// Isso significa: quando alguém acessa "/", o index.html é entregue
app.UseDefaultFiles();

app.UseStaticFiles();

// Ativa o roteamento para os Controllers (nossas rotas /api/fotos)
app.MapControllers();

// Garante que o banco de dados e a tabela existam antes de rodar.
// Em produção real se usa "migrations", mas isso é o jeito mais
// simples de começar.
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

// Inicia a aplicação, deixando ela "escutando" requisições
app.Run();
