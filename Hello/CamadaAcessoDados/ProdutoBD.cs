using Hello.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Hello.CamadaAcessoDados
{
    public class ProdutoBD
    {
        MySQLPersistencia _bd = new MySQLPersistencia();

        public bool Criar(Models.Produto produto)
        {
            //mapeamento Objeto-Relacional (ORM);

            string insert = @"insert into produto(descricao,valor,quantidade,idcategoria)
                              values(@descricao,@valor,@quantidade,@idcategoria)";

            var parametros = _bd.GerarParametros();
            parametros.Add("@descricao", produto.Descricao);
            parametros.Add("@valor", produto.Valor);
            parametros.Add("@quantidade", produto.Quantidade);
            parametros.Add("@idcategoria", produto.Categoria.Id);
            int linhasAfetadas = _bd.ExecutarNonQuery(insert, parametros);
            if (linhasAfetadas > 0)
            {
                produto.Id = _bd.UltimoId;
            }
            return linhasAfetadas > 0;
        }
        public List<Models.Produto> Pesquisar(string descricao)
        {

            List<Models.Produto> produtos = new List<Models.Produto>();
            string select = " ";
            if (descricao == " ")
            {
                select = "select *  from produto";
            }
            else
                select = @"select * 
                              from produto 
                              where descricao like @descricao";

            var parametros = _bd.GerarParametros();
            parametros.Add("@descricao", "%" + descricao + "%");

            DataTable dt = _bd.ExecutarSelect(select, parametros);

            foreach (DataRow row in dt.Rows)
            {
                produtos.Add(Map(row));
            }

            return produtos;

        }

        public bool IncluirFoto(int id, byte[] foto)
        {
            string insert = @"insert into imagensproduto(foto,idproduto) values(@foto,@idproduto)";
            var parametrosBinarios = _bd.GerarParametrosBinarios();
            parametrosBinarios.Add("@foto", foto);
            var parametros = _bd.GerarParametros();
            parametros.Add("@idproduto", id);
            int linhasAfetadas = _bd.ExecutarNonQuery(insert, parametros, parametrosBinarios);

            return linhasAfetadas > 0;
        }

        public byte[] ObterFoto(int id)
        {
            byte[] retorno = null;

            string select = @"select foto
                              from imagensproduto 
                              where idproduto = " + id;

            object fotoBd = _bd.ExecutarScalar(select);

            if (fotoBd != DBNull.Value)
            {
                retorno = (byte[])fotoBd;
            }

            return retorno;

        }
        public List<byte[]> ObterFotoS(int id)
        {
            List<byte[]> retornos = new List<byte[]>();
            byte[] retorno = null;

            string select = @"select foto
                              from imagensproduto 
                              where idproduto = " + id;

            object fotoBd = _bd.ExecutarScalar(select);
            DataTable dt = _bd.ExecutarScalarF(select);






            foreach (DataRow row in dt.Rows)
            {
                retornos.Add((byte[])row["foto"]);
            }




            return retornos;

        }



        public List<Models.Produto> PesquisarPorCategoria(int idcategoria)
        {

            List<Models.Produto> produtos = new List<Models.Produto>();
            string select = " ";
            if (idcategoria == 0)
            {
                select = "select *  from produto";
            }
            else
                select = @"select * 
                              from produto 
                              where idcategoria like @idcategoria";

            var parametros = _bd.GerarParametros();
            parametros.Add("@idcategoria", "%" + idcategoria + "%");

            DataTable dt = _bd.ExecutarSelect(select, parametros);

            foreach (DataRow row in dt.Rows)
            {
                produtos.Add(Map(row));
            }

            return produtos;

        }
        public List<Models.Produto> Pesquisar(int codigo)
        {

            List<Models.Produto> produtos = new List<Models.Produto>();

            string select = @"select * 
                              from produto 
                              where idproduto = @codigo";

            var parametros = _bd.GerarParametros();
            parametros.Add("@codigo", +codigo);

            DataTable dt = _bd.ExecutarSelect(select, parametros);

            foreach (DataRow row in dt.Rows)
            {
                produtos.Add(Map(row));
            }

            return produtos;

        }

        public Produto Obter(int id)
        {
            Models.Produto produto = null;

            string select = @"select * 
                              from produto 
                              where idproduto = " + id;

            DataTable dt = _bd.ExecutarSelect(select);

            if (dt.Rows.Count == 1)
            {
                //ORM - Relacional -> Objeto
                produto = Map(dt.Rows[0]);
            }

            return produto;
        }

        public bool Excluir(int id)
        {
            string select = @"delete 
                              from produto 
                              where idproduto = " + id;

            return _bd.ExecutarNonQuery(select) > 0;

        }
        public int qtdeFoto(int id)
        {
            string select = @"select * 
                              from imagensproduto 
                              where idproduto = " + id;
            DataTable dt = _bd.ExecutarSelect(select);

            return dt.Rows.Count;

        }

        internal Models.Produto Map(DataRow row)
        {
            Models.Produto produto = new Models.Produto();
            CamadaAcessoDados.CategoriaProdutoBD categoriaProduto = new CategoriaProdutoBD();

            produto.Id = Convert.ToInt32(row["idproduto"]);
            produto.Descricao = row["descricao"].ToString();
            produto.Quantidade = Convert.ToInt32(row["quantidade"]);
            produto.Valor = Convert.ToDouble(row["valor"]);
            produto.Categoria = categoriaProduto.Obter(Convert.ToInt32(row["idcategoria"]));
            produto.QtdeFoto = qtdeFoto(Convert.ToInt32(row["idproduto"]));
            return produto;
        }
    }
}

