// Pegamos referências dos elementos HTML que vamos manipular
const form = document.getElementById("form-upload");
const inputArquivo = document.getElementById("input-arquivo");
const galeria = document.getElementById("galeria");
const btnAleatoria = document.getElementById("btn-aleatoria");
const fotoDestaque = document.getElementById("foto-destaque");

// Função auxiliar: monta o caminho da imagem a partir do nome do arquivo
function caminhoDaFoto(nomeArquivo) {
  return `/uploads/${nomeArquivo}`;
}

// ----------------------------------------------------
// 1. CARREGAR E EXIBIR TODAS AS FOTOS
// ----------------------------------------------------
async function carregarGaleria() {
  // "fetch" faz uma requisição HTTP para o backend
  const resposta = await fetch("/api/fotos");
  const fotos = await resposta.json();

  // Limpa a galeria atual antes de redesenhar
  galeria.innerHTML = "";

  fotos.forEach((foto) => {
    const img = document.createElement("img");
    img.src = caminhoDaFoto(foto.nomeArquivo);
    img.alt = foto.descricao || "Foto da galeria";
    galeria.appendChild(img);
  });
}

// ----------------------------------------------------
// 2. ENVIAR UMA NOVA FOTO (upload)
// ----------------------------------------------------
form.addEventListener("submit", async (evento) => {
  // Impede o comportamento padrão do formulário (recarregar a página)
  evento.preventDefault();

  const arquivo = inputArquivo.files[0];
  if (!arquivo) return;

  // FormData é o formato correto para enviar arquivos via fetch
  const dados = new FormData();
  dados.append("arquivo", arquivo);

  await fetch("/api/fotos", {
    method: "POST",
    body: dados,
  });

  // Limpa o campo de arquivo e recarrega a galeria com a nova foto
  inputArquivo.value = "";
  carregarGaleria();
});

// ----------------------------------------------------
// 3. MOSTRAR UMA FOTO ALEATÓRIA EM DESTAQUE
// ----------------------------------------------------
btnAleatoria.addEventListener("click", async () => {
  const resposta = await fetch("/api/fotos/aleatoria");

  if (!resposta.ok) {
    fotoDestaque.innerHTML = "<p>Nenhuma foto cadastrada ainda.</p>";
    return;
  }

  const foto = await resposta.json();
  fotoDestaque.innerHTML = `<img src="${caminhoDaFoto(foto.nomeArquivo)}" alt="Foto aleatória" />`;
});

// ----------------------------------------------------
// Quando a página carrega pela primeira vez, já busca as fotos
// ----------------------------------------------------
carregarGaleria();
