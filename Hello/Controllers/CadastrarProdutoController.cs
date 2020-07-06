using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Hello.Controllers
{
    public class CadastrarProdutoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar([FromBody] Dictionary<string, string> dados)
        {
            bool operacao = false;
            string msg = "";
            int qtde = 0;
            Double valor = 0.0;
            Models.Produto produto = new Models.Produto();
            qtde = 0;
            if (String.IsNullOrEmpty(dados["descricao"]))
            {
                msg = "Descrição não pode ser vazia";
            }
            else if (String.IsNullOrEmpty(dados["quantidade"]))
            {
                msg = "Quantidade não pode ser vazia";
            }
            else if (String.IsNullOrEmpty(dados["valor"]))
            {
                msg = "Valor não pode ser vazia";
            }
            else
            {

                produto.Descricao = dados["descricao"];
                produto.Categoria = new Models.CategoriaProdutos(Convert.ToInt32(dados["categoria"]));
                produto.Quantidade = Convert.ToInt32(dados["quantidade"]);
                produto.Valor = Convert.ToDouble(dados["valor"]);

                CamadaNegocio.ProdutoCamadaNegocio
                    ucn = new CamadaNegocio.ProdutoCamadaNegocio();
                (operacao, msg) = ucn.Criar(produto);
            }

            return Json(new
            {
                id = produto.Id,
                operacao,
                msg
            });

        }
        // [HttpPost]
        public IActionResult Foto(string msg)
        {
            bool operacao = false;
            string nome = "";

            int id = Convert.ToInt32(Request.Form["id"]);
            for (var i = 0; i < Request.Form.Files.Count; i++)
            {
                nome = Request.Form.Files[i].FileName;
                if (System.IO.Path.GetExtension(nome) != ".jpg")
                {
                    msg = "Formato de foto inválido.";
                }
                else
                {
                    //FileStream fs = new FileStream(@"c:\\zssss.jpg", FileMode.Create);
                    //Request.Form.Files[0].CopyTo(fs);

                    MemoryStream ms = new MemoryStream();
                    Request.Form.Files[i].CopyTo(ms);
                    byte[] arq = ms.ToArray();

                    CamadaNegocio.ProdutoCamadaNegocio
                        ucn = new CamadaNegocio.ProdutoCamadaNegocio();
                    (operacao, msg) = ucn.IncluirFoto(msg, id, arq);

                }

            }
            //string nome = Request.Form.Files[0].FileName;

            if (operacao == false)
                msg = "Produto nao cadastrado.";

            return Json(new
            {

                operacao,
                msg
            });
        }
        public IActionResult ObterFoto(int id)
        {
            CamadaNegocio.ProdutoCamadaNegocio ucn = new CamadaNegocio.ProdutoCamadaNegocio();

            byte[] foto = ucn.ObterFoto(id);

            if (foto == null)
                return File("~/imgs/semfoto.jpg", "image/jpg");
            else return File(ucn.ObterFoto(id), "image/jpg");
        }

        public IActionResult ObterFotoS(int id, int pos)
        {
            CamadaNegocio.ProdutoCamadaNegocio ucn = new CamadaNegocio.ProdutoCamadaNegocio();

            List<byte[]> foto = ucn.ObterFotoS(id);


            if (foto == null)
                return File("~/imgs/semfoto.jpg", "image/jpg");
            else
                return File(foto[pos], "image/jpg");
        }
        public IActionResult IndexListar()
        {
            return View();
        }
        public IActionResult Pesquisar(string descricao)
        {
            //System.Threading.Thread.Sleep(5000);
            CamadaNegocio.ProdutoCamadaNegocio ucn = new CamadaNegocio.ProdutoCamadaNegocio();

            List<Models.Produto> produtos = ucn.ObterProdutos(descricao);



            return Json(produtos);
        }
        public IActionResult ObterProduto(int codigo)
        {
            //System.Threading.Thread.Sleep(5000);
            CamadaNegocio.ProdutoCamadaNegocio ucn = new CamadaNegocio.ProdutoCamadaNegocio();

            Models.Produto produto = ucn.Obter(codigo);



            return Json(produto);
        }
        [HttpDelete]
        public IActionResult Excluir(int id)
        {
            CamadaNegocio.ProdutoCamadaNegocio ucn = new CamadaNegocio.ProdutoCamadaNegocio();
            bool operacao = ucn.Excluir(id);

            return Json(new
            {
                operacao
            });
        }
    }
}