using Microsoft.AspNetCore.Mvc;
using MvcNetCoreUtilidades.Helpers;

namespace MvcNetCoreUtilidades.Controllers
{
    public class UploadFilesController : Controller
    {
        private HelperPathProvider helper;
        public UploadFilesController(HelperPathProvider helper)
        {
            this.helper=helper;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SubirFichero()
        {
            return View();
        }
        //[HttpPost]
        //public async Task<IActionResult>SubirFichero(IFormFile fichero)
        //{
        //    //COMEZAMOS ALMACENANDO EL FICHERO EN LOS ELEMENTOS TEMPORALES
        //    string tempFolder = Path.GetTempPath();
        //    string fileName = fichero.FileName;
        //    /*
        //     * DENTRO DE NET CORE PODEMOS MONSTAR EL SERVIDOR DONDE DESEEMOS
        //     * LAS RUTAS DE FICHEROS NO DEBEMOS ESCRIBIRLAS, TENGO QUE GENERAR DICHAS RUTAS
        //     * CON EL SISTEM A DONDE ESTOY TRABAJANDO
        //     */
        //    string path = Path.Combine(tempFolder, fileName);
        //    //PARA SUBIR EL FICHERO SE UTILIZA Stream CON IFormFile
        //    using(Stream stream=new FileStream(path,FileMode.Create))
        //    {
        //        await fichero.CopyToAsync(stream);
        //    }
        //    ViewData["MENSAJE"]="Fichero subido a "+path;
        //    return View();
        //}

        [HttpPost]
        public async Task<IActionResult> SubirFichero(IFormFile fichero)
        {
            string fileName = fichero.FileName;
            
            string path = this.helper.MapPath(fileName, Folders.Images);
            
            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                await fichero.CopyToAsync(stream);
            }
            ViewData["PATH"]=this.helper.MapUrlPath(fileName,Folders.Images);
            ViewData["MENSAJE"]="Fichero subido a "+path;
            return View();
        }
    }
}
