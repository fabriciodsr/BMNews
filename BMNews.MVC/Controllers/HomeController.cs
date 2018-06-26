using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;
using AutoMapper;
using BMNews.App.Interface;
using BMNews.Domain.Entities;
using BMNews.Helpers;
using BMNews.MVC.ViewModels;

namespace BMNews.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly INoticiaAppService _noticiaApp;
        private readonly IUsuarioAppService _usuarioApp;

        public HomeController(INoticiaAppService noticiaApp, IUsuarioAppService usuarioApp)
        {
            _noticiaApp = noticiaApp;
            _usuarioApp = usuarioApp;
        }
        // GET: Home
        public ActionResult Index(string pesquisa, DateTime? startdate, DateTime? enddate)
        {

            if (pesquisa == null && !startdate.HasValue && !enddate.HasValue)
            {
                var noticiaViewModel =
                    Mapper.Map<IEnumerable<Noticia>, IEnumerable<NoticiaViewModel>>(_noticiaApp.GetAll().OrderByDescending(x => x.DataCadastro));

                return View(noticiaViewModel);
            }
            else
            {
                switch (pesquisa == null)
                {
                    case true:
                    {

                        var noticiaViewModel = Mapper.Map<IEnumerable<Noticia>, IEnumerable<NoticiaViewModel>>(_noticiaApp.GetAll().Where(n => n.DataCadastro.CompareTo(startdate) >= 0 && n.DataCadastro.CompareTo(enddate) <= 0));

                        return View(noticiaViewModel);
                    }
                    break;
                    case false:
                    {
                        var noticiaViewModel = Mapper.Map<IEnumerable<Noticia>, IEnumerable<NoticiaViewModel>>(_noticiaApp.GetAll().Where(n => n.PalavraChave.ToLower() == pesquisa.ToLower().Trim()));

                        return View(noticiaViewModel);
                    }
                    break;
                }
            }
            return View();
        }

        // GET: About
        public ActionResult About()
        {
            return View();
        }

        // GET: Contact
        public ActionResult Contact()
        {
            return View();
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UsuarioViewModel usuario)
        {

            if (ModelState.IsValid)
            {
                var usuarioDomain = Mapper.Map<UsuarioViewModel, Usuario>(usuario);
                usuarioDomain.Senha = Criptografia.Codificador(usuarioDomain.Senha);
                usuarioDomain.Ativo = true;

                try
                {
                    _usuarioApp.Add(usuarioDomain);
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            // raise a new exception nesting
                            // the current instance as InnerException
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
                return RedirectToAction("Login", "Login");
            }

            return View(usuario);
        }
    }
}