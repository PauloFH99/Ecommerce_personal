using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Hello.Controllers
{
    public class CategoriaProdutoController : Controller
    {
        public IActionResult ObterCategorias(string codigo)
        {
            Hello.CamadaNegocio.CategoriaProdutoCamadaNegocio ccn =
               new CamadaNegocio.CategoriaProdutoCamadaNegocio();

            return Json(ccn.ObterCategorias(codigo));
        }
        public IActionResult ObterProdutos(string pd)
        {
            Hello.CamadaNegocio.ProdutoCamadaNegocio pcn =
               new CamadaNegocio.ProdutoCamadaNegocio();

            return Json(pcn.ObterProdutos(pd));
        }
        public IActionResult ObterProdutosPorCategoria(string pd)
        {
            Hello.CamadaNegocio.ProdutoCamadaNegocio pcn =
               new CamadaNegocio.ProdutoCamadaNegocio();

            return Json(pcn.ObterProdutosPorCategoria(pd));
        }
    }
}