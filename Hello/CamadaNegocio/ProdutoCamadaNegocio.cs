using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hello.CamadaNegocio
{
    public class ProdutoCamadaNegocio
    {
        public (bool, string) Criar(Models.Produto produto)
        {
            string msg = "";
            bool operacao = false;
            CamadaAcessoDados.ProdutoBD produtoBD = new CamadaAcessoDados.ProdutoBD();
            // descricao não vazia
            if (String.IsNullOrEmpty(produto.Descricao))
            {
                msg = "Descrição não pode ser vazia";
            }
            else if (produto.Quantidade <= 0)
            {
                msg = "Quantidade deve ser preenchida";

            }
            else if (produto.Valor <= 0)
            {
                msg = "Valor deve ser preenchida";

            }
            if (produto.Categoria == null)
            {
                msg = "Categoria deve ser escolhida";
            }
            else if (produtoBD.Criar(produto))
            {

                msg = "Produto " + produto.Descricao + " cadastrado com sucesso.";
                operacao = true;
            }
            else
            {
                msg = "Erro ao cadastrar produto";
            }
            return (operacao, msg);
        }
        public bool Validar()
        {

            return false;
        }

        public List<Models.CategoriaProdutos> ObterCategorias(string codigo)
        {
            List<Models.CategoriaProdutos> categorias = new List<Models.CategoriaProdutos>();
            CamadaAcessoDados.CategoriaProdutoBD categoriaprodutoBD = new CamadaAcessoDados.CategoriaProdutoBD();
            int cod = 0;
            if (codigo == "undefined")
                categorias = categoriaprodutoBD.Pesquisar(" ");
            else
                categorias = categoriaprodutoBD.Pesquisar(Convert.ToInt32(codigo));

            return categorias;
        }
        public (bool, string) IncluirFoto(string msg, int id, byte[] foto)
        {

            bool operacao = false;


            //if (foto.LongCount() > 10 * (1024 * 4))
            //{
            //    msg = "Arquivo muito grande.";
            //}
            //else
            {
                CamadaAcessoDados.ProdutoBD ubd = new CamadaAcessoDados.ProdutoBD();
                operacao = ubd.IncluirFoto(id, foto);
            }
            return (operacao, msg);
        }

        public byte[] ObterFoto(int id)
        {
            CamadaAcessoDados.ProdutoBD ubd = new CamadaAcessoDados.ProdutoBD();
            return ubd.ObterFoto(id);
        }
        public List<byte[]> ObterFotoS(int id)
        {
            CamadaAcessoDados.ProdutoBD ubd = new CamadaAcessoDados.ProdutoBD();
            return ubd.ObterFotoS(id);
        }

        public Models.Produto Obter(int codigo)
        {
            Models.Produto produto = new Models.Produto();
            CamadaAcessoDados.ProdutoBD produtoBD = new CamadaAcessoDados.ProdutoBD();

            produto = produtoBD.Obter(codigo);

            return produto;
        }

        public List<Models.Produto> ObterProdutos(string pd)
        {
            List<Models.Produto> produtos = new List<Models.Produto>();
            //ir no bd...
            CamadaAcessoDados.ProdutoBD produtoBD = new CamadaAcessoDados.ProdutoBD();

            if (pd == "undefined")
                produtos = produtoBD.Pesquisar(" ");
            else
                produtos = produtoBD.Pesquisar(pd);



            return produtos;
        }
        public List<Models.Produto> ObterProdutos(int pd)
        {
            List<Models.Produto> produtos = new List<Models.Produto>();
            //ir no bd...
            CamadaAcessoDados.ProdutoBD produtoBD = new CamadaAcessoDados.ProdutoBD();

            if (pd == 0)
                produtos = produtoBD.Pesquisar(" ");
            else
                produtos = produtoBD.Pesquisar(pd);



            return produtos;
        }
        public Models.Produto ObterProduto(int codigo)
        {
            Models.Produto produto = new Models.Produto();
            //ir no bd...
            CamadaAcessoDados.ProdutoBD produtoBD = new CamadaAcessoDados.ProdutoBD();

            if (codigo == 0)
                produto = produtoBD.Obter(codigo);
            else
                produto = produtoBD.Obter(codigo);



            return produto;
        }
        public List<Models.Produto> ObterProdutosPorCategoria(string pd)
        {
            List<Models.Produto> produtos = new List<Models.Produto>();
            //ir no bd...
            CamadaAcessoDados.ProdutoBD produtoBD = new CamadaAcessoDados.ProdutoBD();

            if (pd == "undefined")
                produtos = produtoBD.Pesquisar(" ");
            else
                produtos = produtoBD.PesquisarPorCategoria(Convert.ToInt32(pd));



            return produtos;
        }
        public bool Excluir(int id)
        {
            CamadaAcessoDados.ProdutoBD ubd = new CamadaAcessoDados.ProdutoBD();
            return ubd.Excluir(id);
        }
    }
}


