using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hello.CamadaNegocio
{
    public class CategoriaProdutoCamadaNegocio
    {

        public List<Models.CategoriaProdutos> ObterCategorias(string codigo)
        {
            List<Models.CategoriaProdutos> categorias = new List<Models.CategoriaProdutos>();
            CamadaAcessoDados.CategoriaProdutoBD categoriaprodutoBD= new CamadaAcessoDados.CategoriaProdutoBD();
            int cod = 0;
            if (codigo == "undefined" || codigo == " " || codigo == null)
                categorias = categoriaprodutoBD.Pesquisar(" ");
            else
                categorias = categoriaprodutoBD.Pesquisar(Convert.ToInt32(codigo));

            return categorias;
        }
        public Models.CategoriaProdutos Obter(string codigo)
        {
            Models.CategoriaProdutos CategoriaProduto = new Models.CategoriaProdutos();
            CamadaAcessoDados.CategoriaProdutoBD CategoriaProdutosBD = new CamadaAcessoDados.CategoriaProdutoBD();
            CategoriaProduto = CategoriaProdutosBD.Obter(Convert.ToInt32(codigo));

            return CategoriaProduto;
        }

       
    }
}



