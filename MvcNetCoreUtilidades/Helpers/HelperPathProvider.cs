using Microsoft.AspNetCore.Hosting.Server;

namespace MvcNetCoreUtilidades.Helpers
{
    public enum Folders
    {
        Images, Facturas, Uploads, Temporal
    }
    public class HelperPathProvider
    {
        private IServer server;
        private IWebHostEnvironment hostEnvironment;
        private IHttpContextAccessor accessor;
        public HelperPathProvider(IWebHostEnvironment hostEnvironment, IHttpContextAccessor accessor, IServer server)
        {
            this.hostEnvironment=hostEnvironment;
            this.accessor=accessor;
            this.server=server;
        }

        public string MapPath(string fileName, Folders folder)
        {
            string carpeta = "";
            if (folder==Folders.Images)
            {
                carpeta="images";
            }else if (folder==Folders.Facturas)
            {
                carpeta="facturas";
            }
            else if(folder==Folders.Temporal)
            {
                carpeta="temp";
            }else if (folder==Folders.Uploads)
            {
                carpeta="uploads";
            }
            string rootPath = this.hostEnvironment.WebRootPath;
            string path = Path.Combine(rootPath, carpeta, fileName);
            return path;
            
        }
        public string MapUrlPath(string fileName,Folders folder)
        {
            string carpeta = "";
            if (folder==Folders.Images)
            {
                carpeta="images";
            }
            else if (folder==Folders.Facturas)
            {
                carpeta="facturas";
            }
            else if (folder==Folders.Temporal)
            {
                carpeta="temp";
            }
            else if (folder==Folders.Uploads)
            {
                carpeta="uploads";
            }
            var request = accessor.HttpContext.Request;
            string baseUrl = request.Scheme+"://"+request.Host;
            return baseUrl+"/"+carpeta+"/"+fileName;

        }

       
    }
}
