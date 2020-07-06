using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
namespace Hello.CamadaAcessoDados
{
    public class MySQLPersistencia
    {
        MySqlConnection _con;
        MySqlCommand _cmd; //executa as instruções SQL

        int _ultimoId = 0;
        public int UltimoId { get => _ultimoId; set => _ultimoId = value; }

        public MySQLPersistencia()
        {
            string strCon = "Server=den1.mysql4.gear.host;Database=fippaulap2020;Uid=fippaulap2020;Pwd=paulo@P98";
            _con = new MySqlConnection(strCon);
            _cmd = _con.CreateCommand();
        }


        public void Abrir()
        {
            if (_con.State != System.Data.ConnectionState.Open)
                _con.Open();
        }

        public void Fechar()
        {
            _con.Close();
        }

        public Dictionary<string, object> GerarParametros()
        {
            return new Dictionary<string, object>();
        }

        /// <summary>
        /// Executa um comando SELECT
        /// </summary>
        /// <param name="select"></param>
        /// <returns></returns>
        public DataTable ExecutarSelect(string select, Dictionary<string, object> parametros = null)
        {
            Abrir();
            _cmd.CommandText = select;
            DataTable dt = new DataTable();

            if (parametros != null)
            {
                foreach (var p in parametros)
                {
                    _cmd.Parameters.AddWithValue(p.Key, p.Value);
                }
            }

            dt.Load(_cmd.ExecuteReader());

            Fechar();

            return dt;
        }

        /// <summary>
        /// Executa INSERT, DELETE, UPDATE E STORED PROCEDURE
        /// </summary>
        /// <param name="sql"></param>
        public int ExecutarNonQuery(string sql, Dictionary<string, object> parametros = null, Dictionary<string, byte[]> parametrosBinarios = null)
        {
            Abrir();
            _cmd.CommandText = sql;

            if (parametros != null)
            {
                foreach (var p in parametros)
                {
                    _cmd.Parameters.AddWithValue(p.Key, p.Value);
                }
            }

            if (parametrosBinarios != null)
            {
                foreach (var p in parametrosBinarios)
                {
                    _cmd.Parameters.Add(p.Key, MySqlDbType.Blob);
                    _cmd.Parameters[p.Key].Value = p.Value;
                }
            }


            int linhasAfetadas = _cmd.ExecuteNonQuery();
            _ultimoId = (int)_cmd.LastInsertedId;

            Fechar();

            return linhasAfetadas;
        }
        public Dictionary<string, byte[]> GerarParametrosBinarios()
        {
            return new Dictionary<string, byte[]>();
        }
        public object ExecutarScalar(string sql, Dictionary<string, object> parametros = null, Dictionary<string, byte[]> parametrosBinarios = null)
        {
            Abrir();
            _cmd.CommandText = sql;

            if (parametros != null)
            {
                foreach (var p in parametros)
                {
                    _cmd.Parameters.AddWithValue(p.Key, p.Value);
                }
            }

            if (parametrosBinarios != null)
            {
                foreach (var p in parametrosBinarios)
                {
                    _cmd.Parameters.Add(p.Key, MySqlDbType.Blob);
                    _cmd.Parameters[p.Key].Value = p.Value;
                }
            }


            object retorno = _cmd.ExecuteScalar();
            _ultimoId = (int)_cmd.LastInsertedId;

            Fechar();

            return retorno;
        }
        public DataTable ExecutarScalarF(string sql, Dictionary<string, object> parametros = null, Dictionary<string, byte[]> parametrosBinarios = null)
        {
            Abrir();
            _cmd.CommandText = sql;
            DataTable dt = new DataTable();
            if (parametros != null)
            {
                foreach (var p in parametros)
                {
                    _cmd.Parameters.AddWithValue(p.Key, p.Value);
                }
            }

            if (parametrosBinarios != null)
            {
                foreach (var p in parametrosBinarios)
                {
                    _cmd.Parameters.Add(p.Key, MySqlDbType.Blob);
                    _cmd.Parameters[p.Key].Value = p.Value;
                }
            }

            dt.Load(_cmd.ExecuteReader());

            _ultimoId = (int)_cmd.LastInsertedId;

            Fechar();

            return dt;
        }


    }
}