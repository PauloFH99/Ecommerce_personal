using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hello.Models
{
    public class CategoriaProdutos
    {
        int _id;
        string _nome;

        public int Id { get => _id; set => _id = value; }
        public string Nome { get => _nome; set => _nome = value; }
        public CategoriaProdutos()
        {
            _id = 0;
            _nome = "";
        }

        public CategoriaProdutos(int id)
        {
            _id = id;
        }
    }
}
