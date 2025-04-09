using System;
using System.Collections.Generic;

public class Produto
{
    // Properties autoimplementadas
    public int Codigo { get; set; }
    
    // Property com validação
    private string _nome;
    public string Nome
    {
        get => _nome;
        set => _nome = string.IsNullOrWhiteSpace(value) 
            ? throw new ArgumentException("O nome não pode ser vazio") 
            : value;
    }
    
    // Properties com validação
    private decimal _preco;
    public decimal Preco
    {
        get => _preco;
        set => _preco = value < 0 
            ? throw new ArgumentException("O preço não pode ser negativo") 
            : value;
    }
    
    private int _estoque;
    public int Estoque
    {
        get => _estoque;
        set => _estoque = value < 0 
            ? throw new ArgumentException("O estoque não pode ser negativo") 
            : value;
    }
    
    // Property somente leitura
    public DateTime DataCadastro { get; } = DateTime.Now;
    
    // Properties calculadas
    public decimal ValorEmEstoque => Preco * Estoque;
    public string Status => Estoque > 0 ? "Disponível" : "Esgotado";
    
    // Método para exibir informações formatadas
    public override string ToString()
    {
        return $"Código: {Codigo} | Nome: {Nome,-20} | Preço: {Preco,8:C} | " +
               $"Estoque: {Estoque,3} | Valor Total: {ValorEmEstoque,10:C} | " +
               $"Status: {Status,-10} | Cadastro: {DataCadastro:dd/MM/yy}";
    }
}