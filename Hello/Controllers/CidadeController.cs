using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Hello.Controllers
{
    public class CidadeController : Controller
    {

        public IActionResult ObterEstados()
        {
            Hello.CamadaNegocio.CidadeCamadaNegocio ccn =
               new CamadaNegocio.CidadeCamadaNegocio();

            return Json(ccn.ObterEstados());


        }

        public IActionResult ObterCidades(string uf)
        {
            Hello.CamadaNegocio.CidadeCamadaNegocio ccn =
                new CamadaNegocio.CidadeCamadaNegocio();

            return Json(ccn.ObterCidades(uf));

        }
    }
}