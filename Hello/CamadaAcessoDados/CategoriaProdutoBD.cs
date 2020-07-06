using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Hello.CamadaAcessoDados
{
    public class CategoriaProdutoBD
    {
        MySQLPersistencia _bd = new MySQLPersistencia();
       
        public List<Models.CategoriaProdutos> Pesquisar(string descricao)
        {

            List<Models.CategoriaProdutos> categoriaprodutos = new List<Models.CategoriaProdutos>();
            string select = " ";
            if (descricao == " ") {
                select = "select *  from categoria";
            }
            else
             select = @"select * 
                              from categoria 
                              where descricao like @descricao";

            var parametros = _bd.GerarParametros();
            parametros.Add("@descricao", "%" + descricao + "%");

            DataTable dt = _bd.ExecutarSelect(select, parametros);

            foreach (DataRow row in dt.Rows)
            {
                categoriaprodutos.Add(Map(row));
            }

            return categoriaprodutos;

        }
        public List<Models.CategoriaProdutos> Pesquisar(int codigo)
        {

            List<Models.CategoriaProdutos> categoriaprodutos = new List<Models.CategoriaProdutos>();

            string select = @"select * 
                              from categoria 
                              where idcategoria = @codigo";

            var parametros = _bd.GerarParametros();
            parametros.Add("@codigo",+codigo);

            DataTable dt = _bd.ExecutarSelect(select, parametros);

            foreach (DataRow row in dt.Rows)
            {
                categoriaprodutos.Add(Map(row));
            }

            return categoriaprodutos;

        }
        public Models.CategoriaProdutos Obter(int id)
        {
            Models.CategoriaProdutos categoriaproduto = new Models.CategoriaProdutos();

            string select = @"select * 
                              from categoria 
                              where idcategoria = " + id;

            DataTable dt = _bd.ExecutarSelect(select);

            if (dt.Rows.Count == 1)
            {
                //ORM - Relacional -> Objeto
                categoriaproduto = Map(dt.Rows[0]);
            }

            return categoriaproduto;

        }
        internal Models.CategoriaProdutos Map(DataRow row)
        {
            Models.CategoriaProdutos categoriaproduto = new Models.CategoriaProdutos();
            categoriaproduto.Id = Convert.ToInt32(row["idcategoria"]);
            categoriaproduto.Nome = row["descricao"].ToString();
          
            return categoriaproduto;
        }
    }
}
