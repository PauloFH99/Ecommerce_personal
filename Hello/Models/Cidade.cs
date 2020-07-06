using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hello.Models
{
    public class Cidade
    {
        int _id;
        string _nome;

        public int Id { get => _id; set => _id = value; }
        public string Nome { get => _nome; set => _nome = value; }

        public Cidade()
        {
            _id = 0;
            _nome = "";
        }
    }
}
