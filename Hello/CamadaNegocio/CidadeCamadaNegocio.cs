using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hello.CamadaNegocio
{
    public class CidadeCamadaNegocio
    {

        public List<string> ObterEstados()
        {
            List<string> ufs = new List<string>();

            //dados.....dal...

            ufs.Add("SP");
            ufs.Add("PR");
            ufs.Add("MT");

            return ufs;
        }

        public List<Models.Cidade> ObterCidades(string uf)
        {
            List<Models.Cidade> cidades = new List<Models.Cidade>();
            //ir no bd...

            if (uf == "SP")
            {
                cidades.Add(new Models.Cidade()
                {
                    Id = 1,
                    Nome = "Adamatina"
                });
                cidades.Add(new Models.Cidade()
                {
                    Id = 2,
                    Nome = "Presidente Prudente"
                });
            }
            else if (uf == "MT")
            {
                cidades.Add(new Models.Cidade()
                {
                    Id = 3,
                    Nome = "Campo Grande"
                });
                cidades.Add(new Models.Cidade()
                {
                    Id = 4,
                    Nome = "Dourados"
                });
            }
            else if (uf == "PR")
            {
                cidades.Add(new Models.Cidade()
                {
                    Id = 5,
                    Nome = "Londrina"
                });
                cidades.Add(new Models.Cidade()
                {
                    Id = 6,
                    Nome = "Curitiba"
                });
            }

            return cidades;
        }
    }
}
