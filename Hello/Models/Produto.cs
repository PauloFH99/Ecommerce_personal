using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hello.Models
{
    public class Produto
    {
        int _id;
        string _descricao;
        double _valor;
        CategoriaProdutos _categoria;
        int _quantidade;
        int _qtdeFoto;


        public int Id { get => _id; set => _id = value; }
        public string Descricao { get => _descricao; set => _descricao = value; }
        public double Valor { get => _valor; set => _valor = value; }
        public CategoriaProdutos Categoria { get => _categoria; set => _categoria = value; }
        public int Quantidade { get => _quantidade; set => _quantidade = value; }
        public int QtdeFoto { get => _qtdeFoto; set => _qtdeFoto = value; }

        public Produto()
        {
            _id = 0;
            _descricao = "";
            _valor = 0.0;
            _categoria = new CategoriaProdutos();
            _quantidade = 0;
            _qtdeFoto = 0;
        }
        public Produto(int id, string descricao, double valor, CategoriaProdutos categoria, int quantidade)
        {
            _id = id;
            _descricao = descricao;
            _valor = valor;
            _categoria = categoria;
            _quantidade = quantidade;

        }
        public Produto(int id, string descricao, double valor, CategoriaProdutos categoria, int quantidade, int qtdeFoto)
        {
            _id = id;
            _descricao = descricao;
            _valor = valor;
            _categoria = categoria;
            _quantidade = quantidade;
            _qtdeFoto = qtdeFoto;
        }


    }
}
