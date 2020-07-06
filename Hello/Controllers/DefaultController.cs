using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Hello.Controllers
{
    public class DefaultController : Controller
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
        public IActionResult Logar([FromBody] Dictionary<string, string> dados)
        {
            // ViewBag.Usuario = usuario;
            // ViewBag.Senha = senha;

            // ViewBag.Usuario=Request.Form["usuario"];
            //ViewBag.Senha = Request.Form["senha"];
            string login = dados["nome"];
            string senha = dados["senha"];
            CamadaNegocio.UsuarioCamadaNegocio ucn = new CamadaNegocio.UsuarioCamadaNegocio();

            bool operacao;
            Models.Usuario Usuario = new Models.Usuario();
            (operacao, Usuario) = ucn.Validar(login, senha);

            if (operacao)
            {
                return Json(new
                {
                    msg = "Bem-vindo",
                    operacao = "true",
                    usuario = Usuario.NomeUsuario
                });
            }
            else
            {
                return Json(new
                {
                    msg = "Dados inválidos",
                    operacao = "false"
                });
            }


        }
        [HttpPost]
        public IActionResult Criar([FromBody] Dictionary<string, string> dados)
        {
            bool operacao = false;
            int senhaNumero;
            string msg = "";
            if(!int.TryParse(dados["senha"],out senhaNumero))
            {
                msg = "Senha inválida.Digite apenas números";
            }
            else 
            {
                Models.Usuario usuario = new Models.Usuario();
                usuario.NomeUsuario = dados["nomeUsuario"];
                usuario.Senha = Convert.ToInt32(dados["senha"]);
                CamadaNegocio.UsuarioCamadaNegocio
                    ucn = new CamadaNegocio.UsuarioCamadaNegocio();
                (operacao,msg)=ucn.Criar(usuario);
            }
            return Json(new
            {
                operacao,
                msg
            }) ;
        }
    }
}