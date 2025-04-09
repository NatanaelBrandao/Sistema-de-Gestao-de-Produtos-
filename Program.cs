class Program
{
    private static List<Produto> produtos = new List<Produto>();
    private static int proximoCodigo = 1;

    static void Main()
    {
        Console.WriteLine("=== SISTEMA DE GESTÃO DE PRODUTOS ===");
        
        while (true)
        {
            Console.WriteLine("\nMENU PRINCIPAL:");
            Console.WriteLine("1 - Cadastrar novo produto");
            Console.WriteLine("2 - Listar todos os produtos");
            Console.WriteLine("3 - Editar produto existente");
            Console.WriteLine("4 - Remover produto");
            Console.WriteLine("5 - Consultar produto por código");
            Console.WriteLine("6 - Sair do sistema");
            Console.Write("\nDigite sua opção: ");

            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    CadastrarProduto();
                    break;
                case "2":
                    ListarProdutos();
                    break;
                case "3":
                    EditarProduto();
                    break;
                case "4":
                    RemoverProduto();
                    break;
                case "5":
                    ConsultarProduto();
                    break;
                case "6":
                    Console.WriteLine("\nSaindo do sistema...");
                    return;
                default:
                    Console.WriteLine("\nOpção inválida! Tente novamente.");
                    break;
            }
        }
    }

    static void CadastrarProduto()
    {
        Console.WriteLine("\nCADASTRO DE NOVO PRODUTO");

        try
        {
            var produto = new Produto();
            produto.Codigo = proximoCodigo++;
            
            Console.Write("Nome do produto: ");
            produto.Nome = Console.ReadLine();
            
            Console.Write("Preço unitário: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal preco))
                produto.Preco = preco;
            else
                throw new FormatException("Preço inválido");
            
            Console.Write("Quantidade em estoque: ");
            if (int.TryParse(Console.ReadLine(), out int estoque))
                produto.Estoque = estoque;
            else
                throw new FormatException("Estoque inválido");

            produtos.Add(produto);
            Console.WriteLine("\nProduto cadastrado com sucesso!");
            Console.WriteLine(produto);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nErro ao cadastrar: {ex.Message}");
            proximoCodigo--; // Reverte o incremento do código em caso de erro
        }
    }

    static void ListarProdutos()
    {
        Console.WriteLine("\nLISTA DE PRODUTOS CADASTRADOS");
        
        if (produtos.Count == 0)
        {
            Console.WriteLine("Nenhum produto cadastrado.");
            return;
        }

        Console.WriteLine("-------------------------------------------------------------------------------------------");
        Console.WriteLine("| Código | Nome               | Preço    | Est | Valor Total | Status    | Data Cadastro |");
        Console.WriteLine("-------------------------------------------------------------------------------------------");
        
        foreach (var p in produtos)
        {
            Console.WriteLine($"| {p.Codigo,6} | {p.Nome,-18} | {p.Preco,8:C} | {p.Estoque,3} | " +
                              $"{p.ValorEmEstoque,11:C} | {p.Status,-9} | {p.DataCadastro:dd/MM/yyyy} |");
        }
        
        Console.WriteLine("-------------------------------------------------------------------------------------------");
        Console.WriteLine($"Total de produtos: {produtos.Count} | Valor total em estoque: {CalcularValorTotalEstoque():C}");
    }

    static void EditarProduto()
    {
        Console.Write("\nDigite o código do produto a editar: ");
        if (!int.TryParse(Console.ReadLine(), out int codigo))
        {
            Console.WriteLine("Código inválido!");
            return;
        }

        var produto = produtos.Find(p => p.Codigo == codigo);
        if (produto == null)
        {
            Console.WriteLine("Produto não encontrado!");
            return;
        }

        Console.WriteLine("\nDados atuais do produto:");
        Console.WriteLine(produto);

        try
        {
            Console.Write("\nNovo nome (enter para manter): ");
            string novoNome = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(novoNome))
                produto.Nome = novoNome;

            Console.Write("Novo preço (enter para manter): ");
            string inputPreco = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(inputPreco) && decimal.TryParse(inputPreco, out decimal novoPreco))
                produto.Preco = novoPreco;

            Console.Write("Novo estoque (enter para manter): ");
            string inputEstoque = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(inputEstoque) && int.TryParse(inputEstoque, out int novoEstoque))
                produto.Estoque = novoEstoque;

            Console.WriteLine("\nProduto atualizado com sucesso!");
            Console.WriteLine(produto);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nErro ao atualizar: {ex.Message}");
        }
    }

    static void RemoverProduto()
    {
        Console.Write("\nDigite o código do produto a remover: ");
        if (!int.TryParse(Console.ReadLine(), out int codigo))
        {
            Console.WriteLine("Código inválido!");
            return;
        }

        var produto = produtos.Find(p => p.Codigo == codigo);
        if (produto == null)
        {
            Console.WriteLine("Produto não encontrado!");
            return;
        }

        Console.WriteLine("\nProduto a ser removido:");
        Console.WriteLine(produto);

        Console.Write("\nTem certeza que deseja remover? (S/N): ");
        if (Console.ReadLine().ToUpper() == "S")
        {
            produtos.Remove(produto);
            Console.WriteLine("Produto removido com sucesso!");
        }
        else
        {
            Console.WriteLine("Operação cancelada.");
        }
    }

    static void ConsultarProduto()
    {
        Console.Write("\nDigite o código do produto a consultar: ");
        if (!int.TryParse(Console.ReadLine(), out int codigo))
        {
            Console.WriteLine("Código inválido!");
            return;
        }

        var produto = produtos.Find(p => p.Codigo == codigo);
        if (produto == null)
        {
            Console.WriteLine("Produto não encontrado!");
            return;
        }

        Console.WriteLine("\nDADOS DO PRODUTO");
        Console.WriteLine("----------------------------------------");
        Console.WriteLine($"Código: {produto.Codigo}");
        Console.WriteLine($"Nome: {produto.Nome}");
        Console.WriteLine($"Preço unitário: {produto.Preco:C}");
        Console.WriteLine($"Quantidade em estoque: {produto.Estoque}");
        Console.WriteLine($"Valor total em estoque: {produto.ValorEmEstoque:C}");
        Console.WriteLine($"Status: {produto.Status}");
        Console.WriteLine($"Data de cadastro: {produto.DataCadastro:dd/MM/yyyy HH:mm}");
        Console.WriteLine("----------------------------------------");
    }

    static decimal CalcularValorTotalEstoque()
    {
        decimal total = 0;
        foreach (var p in produtos)
        {
            total += p.ValorEmEstoque;
        }
        return total;
    }
}