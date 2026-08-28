# Galeria de Fotos Aleatórias

## Pré-requisitos

1. **.NET 8 SDK** instalado — baixe em https://dotnet.microsoft.com/download
   Verifique com:
   ```bash
   dotnet --version
   ```

## Como rodar

1. Abra a pasta `GaleriaFotos` no VS Code (`code .`)

2. No terminal, dentro da pasta do projeto, baixe as dependências:
   ```bash
   dotnet restore
   ```

3. Rode a aplicação:
   ```bash
   dotnet run
   ```

4. O terminal vai mostrar algo como:
   ```
   Now listening on: http://localhost:5xxx
   ```
   Abra esse endereço no navegador.

5. Na primeira execução, o arquivo `galeria.db` (o banco SQLite) é criado
   automaticamente na pasta do projeto — não precisa fazer nada manual.

## Como usar o site

- Escolha um arquivo de imagem e clique em "Enviar foto" — ela aparece na galeria.
- Clique em "Mostrar foto aleatória" para sortear uma foto em destaque.

## Estrutura do projeto

- `Models/Foto.cs` — como uma foto é representada nos dados
- `Data/AppDbContext.cs` — conexão com o banco SQLite
- `Controllers/FotosController.cs` — as rotas da API (`/api/fotos`)
- `wwwroot/` — frontend (HTML, CSS, JS) e a pasta `uploads/` onde as imagens ficam salvas
- `Program.cs` — inicializa tudo

## Próximos passos sugeridos (para você evoluir sozinho)

- Adicionar validação de tipo/tamanho de arquivo no upload
- Adicionar botão de excluir foto na galeria (a rota DELETE já existe no backend)
- Usar um script Python separado para gerar thumbnails/miniaturas das fotos
  antes ou depois do upload
