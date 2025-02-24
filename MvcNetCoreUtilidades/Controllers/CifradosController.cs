using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MvcNetCoreUtilidades.Helpers;

namespace MvcNetCoreUtilidades.Controllers
{
    public class CifradosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CifradoBasico()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CifradoBasico(string contenido, string resultado, string accion)
        {
            //ciframos el contenido
            string response = HelperCryptography.EncriptarTextoBasico(contenido);
            if (accion.ToLower()=="cifrar")
            {
                ViewData["TEXTOCIFRADO"]=response;
            }else if (accion.ToLower()=="comparar")
            {
                //SI EL USUARIO QUIERE COMPARAR NOS ESTARÁ ENVIANDO EL RESULTADO A COMPARAR
                if (response!=resultado)
                {
                    ViewData["MENSAJE"]="Los datos no coinciden";
                }
                else
                {
                    ViewData["MENSAJE"]="Contenidos iguales";
                }
            }
            return View();
        }

        public IActionResult CifradoEficiente()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CifradoEficiente(string contenido, string resultado, string accion)
        {
            if (accion.ToLower()=="cifrar")
            {
                string response =
                    HelperCryptography.CifrarContenido(contenido, false);
                ViewData["TEXTOCIFRADO"]=response;
                ViewData["SALT"]=HelperCryptography.Salt;
            }else if (accion.ToLower()=="comparar")
            {
                string response =
                    HelperCryptography.CifrarContenido(contenido, true);
                if (response!=resultado)
                {
                    ViewData["MENSAJE"]="LOS DATOS NO SON IGUALES";
                }
                else
                {
                    ViewData["MENSAJE"]="DATOS COINCIDEN";
                }
            }
            return View();
        }
    }
}
