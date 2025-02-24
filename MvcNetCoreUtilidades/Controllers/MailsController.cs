using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;

namespace MvcNetCoreUtilidades.Controllers
{
    public class MailsController : Controller
    {
        //NECESITAMOS EL FICHERO DE CONFIGURACION
        private IConfiguration configuration;
        public MailsController(IConfiguration configuration)
        {
            this.configuration=configuration;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SendMail()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SendMail(string to, string asunto, string mensaje)
        {
            MailMessage mail = new MailMessage();
            string user=this.configuration.GetValue<string>("MailSettings:Credentials:User");
            mail.From=new MailAddress(user);
            mail.To.Add(to);
            mail.Subject=asunto;
            mail.Body=mensaje;
            mail.IsBodyHtml=true;
            mail.Priority=MailPriority.Normal;
            string password = this.configuration.GetValue<string>("MailSettings:Credentials:Password");
            string host = this.configuration.GetValue<string>("MailSettings:Server:Host");
            int port = this.configuration.GetValue<int>("MailSettings:Server:Port");
            bool ssl = this.configuration.GetValue<bool>("MailSettings:Server:Ssl");
            bool defaultCredentials = this.configuration.GetValue<bool>("MailSettings:Server:DefaultCredentials");
            //SE CREA LA CLASE SERVIDOR SMTP
            SmtpClient smtp = new SmtpClient();
            smtp.Host=host;
            smtp.Port=port;
            smtp.EnableSsl=ssl;
            smtp.UseDefaultCredentials=defaultCredentials;
            //SE CREAN LAS CREDENCIALES PARA EL MAIL
            NetworkCredential credential = new NetworkCredential(user, password);
            smtp.Credentials=credential;
            smtp.UseDefaultCredentials=false;
            await smtp.SendMailAsync(mail);
            ViewData["MENSAJE"]="Mail Enviado Correctamente";
            return View();
        }
    }
}
