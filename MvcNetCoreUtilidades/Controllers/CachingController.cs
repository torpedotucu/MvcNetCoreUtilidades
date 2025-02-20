using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace MvcNetCoreUtilidades.Controllers
{
    public class CachingController : Controller
    {
        private IMemoryCache memoryCache;
        public CachingController(IMemoryCache cache)
        {
            this.memoryCache=cache;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MemoriaPersonalizada(int? tiempo)
        {
            if (tiempo==null)
            {
                tiempo=60;
            }
            string fecha = DateTime.Now.ToLongDateString()+" -- "+DateTime.Now.ToLongTimeString();
            //DEBEMOS PREGUNTAR SI EXISTE ALGO EN CACHE 
            if (this.memoryCache.Get("FECHA")==null)
            {
                //NO EXISTE EN CACHE TODAVIA
                //CREAMOS EL OBJETO ENTRY OPTIONS CON EL TIEMPO
                MemoryCacheEntryOptions options =
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(tiempo.Value));
                this.memoryCache.Set("FECHA", fecha,options);
                ViewData["MENSAJE"]="Fecha almacenada en cache";
                ViewData["FECHA"]=fecha;
            }
            else
            {
                fecha=this.memoryCache.Get<string>("FECHA");
                ViewData["MENSAJE"]="Fecha recuperada de Cache";
                ViewData["FECHA"]=fecha;
            }
            return View();
        }

        [ResponseCache(Duration =5, Location=ResponseCacheLocation.Client)]
        public IActionResult MemoriaDistribuida()
        {
            string fecha = DateTime.Now.ToLongDateString()+" -- "+DateTime.Now.ToLongTimeString();
            ViewData["FECHA"]=fecha;
            return View();
        }


    }
}
