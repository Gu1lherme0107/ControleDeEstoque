using System;
using System.Data.SQLite;

public class Produto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public int Quantidade { get; set; }

    public Produto(int id, string nome, int quantidade)
    {
        Id = id;
        Nome = nome;
        Quantidade = quantidade;
    }
}

class Program
{
    private static string connString = "Data Source=estoque.db;Version=3;";

    static void Main(string[] args)
    {
        CriarTabela();

        while (true)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n--- Sistema de Inventário ---");
            Console.ResetColor();
            Console.WriteLine("1. Adicionar Produto");
            Console.WriteLine("2. Atualizar Produto");
            Console.WriteLine("3. Remover Produto");
            Console.WriteLine("4. Listar Produtos");
            Console.WriteLine("5. Sair");
            Console.Write("Escolha uma opção: ");
            var opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    AdicionarProduto();
                    break;
                case "2":
                    AtualizarProduto();
                    break;
                case "3":
                    RemoverProduto();
                    break;
                case "4":
                    ListarProdutos();
                    break;
                case "5":
                    return;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Opção inválida. Pressione qualquer tecla para tentar novamente.");
                    Console.ResetColor();
                    Console.ReadKey();
                    break;
            }
        }
    }

    // Cria a tabela de produtos no banco de dados (se não existir)
    static void CriarTabela()
    {
        using (var connection = new SQLiteConnection(connString))
        {
            connection.Open();
            string createTableQuery = "CREATE TABLE IF NOT EXISTS produtos (id INTEGER PRIMARY KEY AUTOINCREMENT, nome_produto TEXT, quantidade INTEGER)";
            SQLiteCommand command = new SQLiteCommand(createTableQuery, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }
    }

    // Adiciona um produto ao inventário
    static void AdicionarProduto()
    {
        Console.Write("Nome do produto: ");
        string? nome = Console.ReadLine();
        if (string.IsNullOrEmpty(nome))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Nome do produto não pode ser vazio.");
            Console.ResetColor();
            return;
        }

        Console.Write("Quantidade: ");
        int quantidade;
        while (!int.TryParse(Console.ReadLine(), out quantidade) || quantidade <= 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Quantidade inválida. Digite um número maior que 0: ");
            Console.ResetColor();
        }

        using (var connection = new SQLiteConnection(connString))
        {
            connection.Open();
            string insertQuery = "INSERT INTO produtos (nome_produto, quantidade) VALUES (@nome, @quantidade)";
            SQLiteCommand command = new SQLiteCommand(insertQuery, connection);
            command.Parameters.AddWithValue("@nome", nome);
            command.Parameters.AddWithValue("@quantidade", quantidade);
            command.ExecuteNonQuery();
            connection.Close();
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Produto adicionado com sucesso! Pressione qualquer tecla para voltar ao menu.");
        Console.ResetColor();
        Console.ReadKey();
    }

    // Atualiza um produto existente no inventário
    static void AtualizarProduto()
    {
        Console.Write("Digite o ID do produto a ser atualizado: ");
        int id;
        while (!int.TryParse(Console.ReadLine(), out id) || id <= 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("ID inválido. Digite um número válido: ");
            Console.ResetColor();
        }

        using (var connection = new SQLiteConnection(connString))
        {
            connection.Open();
            string selectQuery = "SELECT * FROM produtos WHERE id = @id";
            SQLiteCommand command = new SQLiteCommand(selectQuery, connection);
            command.Parameters.AddWithValue("@id", id);
            SQLiteDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Produto encontrado: ID = {reader["id"]}, Nome = {reader["nome_produto"]}, Quantidade = {reader["quantidade"]}");
                Console.ResetColor();

                // Atualizar nome
                Console.Write("Novo nome do produto (deixe em branco para manter o atual): ");
                string? novoNome = Console.ReadLine();
                if (!string.IsNullOrEmpty(novoNome))
                {
                    string updateNomeQuery = "UPDATE produtos SET nome_produto = @novoNome WHERE id = @id";
                    SQLiteCommand updateNomeCommand = new SQLiteCommand(updateNomeQuery, connection);
                    updateNomeCommand.Parameters.AddWithValue("@novoNome", novoNome);
                    updateNomeCommand.Parameters.AddWithValue("@id", id);
                    updateNomeCommand.ExecuteNonQuery();
                }

                // Atualizar quantidade
                Console.Write("Nova quantidade (deixe em branco para manter a atual): ");
                string? novaQuantidadeStr = Console.ReadLine();
                if (!string.IsNullOrEmpty(novaQuantidadeStr) && int.TryParse(novaQuantidadeStr, out int novaQuantidade) && novaQuantidade > 0)
                {
                    string updateQuantidadeQuery = "UPDATE produtos SET quantidade = @novaQuantidade WHERE id = @id";
                    SQLiteCommand updateQuantidadeCommand = new SQLiteCommand(updateQuantidadeQuery, connection);
                    updateQuantidadeCommand.Parameters.AddWithValue("@novaQuantidade", novaQuantidade);
                    updateQuantidadeCommand.Parameters.AddWithValue("@id", id);
                    updateQuantidadeCommand.ExecuteNonQuery();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Quantidade não foi alterada.");
                    Console.ResetColor();
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Produto atualizado com sucesso!");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Produto não encontrado.");
                Console.ResetColor();
            }

            connection.Close();
        }

        Console.WriteLine("Pressione qualquer tecla para voltar ao menu.");
        Console.ReadKey();
    }

    // Lista todos os produtos no inventário
    static void ListarProdutos()
    {
        using (var connection = new SQLiteConnection(connString))
        {
            connection.Open();
            string selectQuery = "SELECT * FROM produtos";
            SQLiteCommand command = new SQLiteCommand(selectQuery, connection);
            SQLiteDataReader reader = command.ExecuteReader();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Produtos no Inventário:");
            Console.ResetColor();
            while (reader.Read())
            {
                Console.WriteLine($"ID: {reader["id"]}, Nome: {reader["nome_produto"]}, Quantidade: {reader["quantidade"]}");
            }
            connection.Close();
        }

        Console.WriteLine("Pressione qualquer tecla para voltar ao menu.");
        Console.ReadKey();
    }

    // Remove um produto do inventário
    static void RemoverProduto()
    {
        Console.Write("Digite o ID do produto a ser removido: ");
        int id;
        while (!int.TryParse(Console.ReadLine(), out id) || id <= 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("ID inválido. Digite um número válido: ");
            Console.ResetColor();
        }

        using (var connection = new SQLiteConnection(connString))
        {
            connection.Open();
            string deleteQuery = "DELETE FROM produtos WHERE id = @id";
            SQLiteCommand command = new SQLiteCommand(deleteQuery, connection);
            command.Parameters.AddWithValue("@id", id);
            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Produto removido com sucesso!");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Produto não encontrado.");
                Console.ResetColor();
            }

            connection.Close();
        }

        Console.WriteLine("Pressione qualquer tecla para voltar ao menu.");
        Console.ReadKey();
    }
}
