using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Hello.Controllers
{
    public class CadastrarAlunoController : Controller
    {
        public IActionResult Index()
        {


            return View();
        }
        public IActionResult Login()
        {


            return View("Login");
        }

        [HttpPost]
        public IActionResult Criar([FromBody] Dictionary<string, string> dados)
        {
            bool operacao = false;
            int senhaNumero;
            string msg = "";
            if (dados["nomeUsuario"] == "")
            {
                msg = "Nome não informado.";
            }
            else if (!int.TryParse(dados["senha"], out senhaNumero))
            {
                msg = "Senha inválida. Digite apenas número.";
            }
            if (dados["senha"] != dados["senhaConf"])
            {
                msg = "Senhas diferentes.";
            }
            else
            {
                Models.Usuario usuario = new Models.Usuario();
                usuario.NomeUsuario = dados["nomeUsuario"];
                usuario.Senha = Convert.ToInt32(dados["senha"]);

                CamadaNegocio.UsuarioCamadaNegocio
                    ucn = new CamadaNegocio.UsuarioCamadaNegocio();
                (operacao, msg) = ucn.Criar(usuario);
            }

            return Json(new
            {
                operacao,
                msg
            });

        }
        public IActionResult IndexObterPerfis()
        {
            return View();
        }
        public IActionResult ObterPerfis(string descricao)
        {
            CamadaNegocio.UsuarioCamadaNegocio ucn = new CamadaNegocio.UsuarioCamadaNegocio();
            return Json(ucn.ObterPerfis(descricao));
        }
    }

}