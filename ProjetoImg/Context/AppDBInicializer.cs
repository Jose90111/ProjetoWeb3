using ProjetoImg.Models;

namespace ProjetoImg.Context
{
    public class AppDBInicializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppCont>();

                if (context == null) throw new Exception("Context not found");

                // Garantir que o banco de dados está criado
                context.Database.EnsureCreated();

                // Inserir dados apenas se a tabela estiver vazia
                if (!context.Clientes.Any())
                {
                    string basePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imagem");

                    // Método auxiliar para carregar imagens
                    byte[] LoadImage(string fileName)
                    {
                        string filePath = Path.Combine(basePath, fileName);
                        if (File.Exists(filePath))
                        {
                            return File.ReadAllBytes(filePath);
                        }
                        throw new FileNotFoundException($"Arquivo não encontrado: {filePath}");
                    }

                    // Carregamento das imagens
                    byte[] foto1 = LoadImage("foto1.jpg");
                    byte[] foto2 = LoadImage("foto2.jpg");
                    byte[] foto3 = LoadImage("foto3.jpg");

                    // Adicionando registros
                    var Clientes = new List<Cad_cliente>
                    {
                        new Cad_cliente
                        {
                            Nome = "Teste da Silva",
                            CPF = "123.456.789-78",
                            Email = "teste.silva@teste.com",
                            DataNascimento = new DateTime(2000, 11, 07),
                            Sexo = "NÃO INFORMADO",
                            Foto = foto2,
                            Logradouro = "Rua das Flores",
                            Numero = "123",
                            Complemento = "Apto 101",
                            Bairro = "Centro",
                            Cidade = "São Paulo",
                            Estado = "SP",
                            CEP = "01000-000"
                        },
                        new Cad_cliente
                        {
                            Nome = "Teste Oliveira",
                            CPF = "432.456.432-78",
                            Email = "teste.teste@teste.com",
                            DataNascimento = new DateTime(1999, 07, 11),
                            Sexo = "Feminino",
                            Foto = foto1,
                            Logradouro = "Rua das Praias",
                            Numero = "543",
                            Complemento = "Apto 111",
                            Bairro = "Centro",
                            Cidade = "São Paulo",
                            Estado = "SP",
                            CEP = "01212-000"
                        },
                        new Cad_cliente
                        {
                            Nome = "Teste da Rocha",
                            CPF = "412.456.654-78",
                            Email = "teste.rocha@teste.com",
                            DataNascimento = new DateTime(2005, 08, 11),
                            Sexo = "Masculino",
                            Foto = foto3,
                            Logradouro = "Rua das Palmeiras",
                            Numero = "87",
                            Complemento = "Apto 12",
                            Bairro = "Centro",
                            Cidade = "São Paulo",
                            Estado = "SP",
                            CEP = "01000-242"
                        }
                    };

                    context.Clientes.AddRange(Clientes);
                    context.SaveChanges();
                }
            }
        }
    }
}
