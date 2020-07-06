using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
namespace Hello.CamadaAcessoDados
{
    public class UsuarioBD
    {
        MySQLPersistencia _bd = new MySQLPersistencia();

        public bool Criar(Models.Usuario usuario)
        {
            //mapeamento Objeto-Relacional (ORM);

            string insert = @"insert into usuario(nome, senha)
                              values(@nome, @senha)";

            var parametros = _bd.GerarParametros();
            parametros.Add("@nome", usuario.NomeUsuario);
            parametros.Add("@senha", usuario.Senha);

            int linhasAfetadas = _bd.ExecutarNonQuery(insert, parametros);
            if (linhasAfetadas > 0)
            {
                usuario.Id = _bd.UltimoId;
            }
            return linhasAfetadas > 0;
        }

        public (bool, Models.Usuario) Validar(string usuarioNome, string senha)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@nome", usuarioNome);
            parametros.Add("@senha", senha);

            string sql = @"select count(*) as conta,coalesce(id,0)id,coalesce(nome,'')nome,
                          coalesce(senha,'')senha
                          from usuario
                          where nome = @nome and senha = @senha";

            DataTable dt = _bd.ExecutarSelect(sql, parametros);
            int conta = Convert.ToInt32(dt.Rows[0]["conta"]);

            if (conta == 0)
                return (false, null);
            else
                return (true, new Models.Usuario(Convert.ToInt32(dt.Rows[0]["id"]),
                                                Convert.ToString(dt.Rows[0]["nome"]),
                                                Convert.ToInt32(dt.Rows[0]["senha"])));
        }
        public bool validarLogin(string nome)
        {
            string sql = "select count(*) as usuario from usuario where nome = '" + nome + "'";
            DataTable dt = _bd.ExecutarSelect(sql);
            int usuario = Convert.ToInt32(dt.Rows[0]["usuario"]);

            if (usuario == 0)
                return true;
            else
                return false;
        }


    }
}
