using Mvc5_Contatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using PagedList;


namespace Mvc5_Contatos.Controllers
{
    public class ContatosController : Controller
    {
        //Lista os contatos sem o endereço
        // GET: Contatos
        public ActionResult Index(int? pagina)
        {
            int paginaTamanho = 4;
            int paginaNumero = (pagina ?? 1);
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
            return View(contatos.ToPagedList(paginaNumero, paginaTamanho));
            }
           
        }

        //Criar um novo contato
        [HttpGet]
        public ActionResult create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult create(ContatoEnderecoViewModel contato)
        {
            if(contato == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:59216/api/");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<ContatoEnderecoViewModel>("contatos", contato);
                postTask.Wait();
                var result = postTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty, "Erro no Servidor. Contate o Administrador.");
                return View(contato);
            }
        }

        //Editar um contato
        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContatoViewModel contato = null;
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:59216/api/contatos");

                //HTTP GET
                var responseTask = client.GetAsync("?id=" + id.ToString());
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ContatoViewModel>();
                    readTask.Wait();

                    contato = readTask.Result;
                }
            }
            return View(contato);
        }

        [HttpPost]
        public ActionResult Edit(ContatoViewModel contato)
        {
            if(contato == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:59216/api/");

                //HTTP PUT
                var putTask = client.PutAsJsonAsync<ContatoViewModel>("contatos", contato);
                putTask.Wait();
                var result = putTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(contato);
        }

        //Deletar um contato
        public ActionResult Delete(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ContatoViewModel contato = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:59216/api/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("contatos/" + id.ToString());
                deleteTask.Wait();
                var result = deleteTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(contato);
        }

        //Detales de um contato
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ContatoViewModel contato = null;

            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:59216/api/contatos");

                //HTTP GET
                var responseTask = client.GetAsync("?id=" + id.ToString());
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ContatoViewModel>();
                    readTask.Wait();

                    contato = readTask.Result;
                }
            }
            return View(contato);
        }

        //SEARCH - BUSCAR UM CONTATO PELO NOME

        public ActionResult Search(string nome)
        {
            IEnumerable<ContatoViewModel> contatos = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:59216/api/");

                //HTTP GET
                var responseTask = client.GetAsync("contatos?nome=" + nome);
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