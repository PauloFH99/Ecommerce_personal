using Hello.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Hello.CamadaAcessoDados
{
    public class PerfilBD
    {
        MySQLPersistencia _bd = new MySQLPersistencia();

        public Perfil Obter(int id)
        {
            Models.Perfil perfil = null;

            string select = @"select * 
                              from perfil 
                              where id = " + id;

            DataTable dt = _bd.ExecutarSelect(select);

            if (dt.Rows.Count == 1)
            {
                //ORM - Relacional -> Objeto
                perfil = Map(dt.Rows[0]);
            }

            return perfil;
        }

        public List<Models.Perfil> Pesquisar(string descricao)
        {

            List<Models.Perfil> perfis = new List<Models.Perfil>();

            string select = @"select * 
                              from perfil 
                              where descricao like  @descricao";

            var parametros = _bd.GerarParametros();
            parametros.Add("@descricao","%"+descricao+ "%");

            DataTable dt = _bd.ExecutarSelect(select, parametros);

            foreach (DataRow row in dt.Rows)
            {
                perfis.Add(Map(row));
            }

            return perfis;

        }

        

       
        internal Models.Perfil Map(DataRow row)
        {
            Models.Perfil perfil = new Models.Perfil();

            perfil.Id = Convert.ToInt32(row["id"]);
            perfil.Descricao= row["descricao"].ToString();
           
            return perfil;
        }
    }
}

