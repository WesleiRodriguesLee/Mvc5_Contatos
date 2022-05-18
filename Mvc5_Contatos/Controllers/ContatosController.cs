using Mvc5_Contatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;


namespace Mvc5_Contatos.Controllers
{
    public class ContatosController : Controller
    {
        // GET: Contatos
        public ActionResult Index()
        {
            IEnumerable<ContatoViewModel> contatos = null;

            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:59216/api/");

                //HTTP GET
                var responseTask = client.GetAsync("contatos");
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<ContatoViewModel>>();
                    readTask.Wait();

                    contatos = readTask.Result;
                }
                else
                {
                    contatos = Enumerable.Empty<ContatoViewModel>();
                    ModelState.AddModelError(string.Empty, "Erro no servidor.Contate o Administrador.");
                }
            return View(contatos);
            }
           
        }
    }
}