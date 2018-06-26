using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using BMNews.App.Interface;
using BMNews.Domain.Entities;
using BMNews.Helpers;
using BMNews.MVC.ViewModels;

namespace BMNews.MVC.Controllers
{
    [Authorize(Roles = "admin")]//roles sendo identificadas no Global.asax.cs
    public class AdministradoresController : Controller
    {
        private readonly INoticiaAppService _noticiaApp;
        private readonly IUsuarioAppService _usuarioApp;

        public AdministradoresController(INoticiaAppService noticiaApp, IUsuarioAppService usuarioApp)
        {
            _noticiaApp = noticiaApp;
            _usuarioApp = usuarioApp;
        }

        // GET: Usuarios
        public ActionResult Index()
        {
            var TodasNoticias =
                Mapper.Map<IEnumerable<Noticia>, IEnumerable<NoticiaViewModel>>(_noticiaApp.GetAll());

            var TodosUsuarios =
                Mapper.Map<IEnumerable<Usuario>, IEnumerable<UsuarioViewModel>>(_usuarioApp.GetAll());

            var NoticiasDesativadas =
                Mapper.Map<IEnumerable<Noticia>, IEnumerable<NoticiaViewModel>>(_noticiaApp.GetAll().Where(x => x.Disponivel = false));

            var UsuariosDesativadas =
                Mapper.Map<IEnumerable<Usuario>, IEnumerable<UsuarioViewModel>>(_usuarioApp.GetAll().Where(x => x.Ativo = false));


            var contTotalUsuario = TodosUsuarios.Count();
            var contTotalNoticia = TodasNoticias.Count();
            var contUsuarioDstv = UsuariosDesativadas.Count();
            var contNoticiaDstv = NoticiasDesativadas.Count();

            Session["QuantUsu"] = contTotalUsuario;
            Session["QuantNoti"] = contTotalNoticia;
            Session["QuantUsuDstv"] = contUsuarioDstv;
            Session["QuantNotiDstv"] = contNoticiaDstv;

            return View();
        }


        // GET: Usuarios/Details/5
        public ActionResult Details(int id)
        {
            var usuario = _usuarioApp.GetById(id);
            var usuarioViewModel = Mapper.Map<Usuario, UsuarioViewModel>(usuario);

            return View(usuarioViewModel);
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
                return RedirectToAction("Index");
            }

            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int id)
        {
            var usuario = _usuarioApp.GetById(id);
            var usuarioViewModel = Mapper.Map<Usuario, UsuarioViewModel>(usuario);

            return View(usuarioViewModel);
        }

        // POST: Usuarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UsuarioViewModel usuario)
        {
            if (ModelState.IsValid)
            {
                var usuarioDomain = Mapper.Map<UsuarioViewModel, Usuario>(usuario);
                usuarioDomain.Senha = Criptografia.Codificador(usuarioDomain.Senha);
                _usuarioApp.Update(usuarioDomain);

                return RedirectToAction("Index");
            }

            return View(usuario);

        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int id)
        {
            var usuario = _usuarioApp.GetById(id);
            var usuarioViewModel = Mapper.Map<Usuario, UsuarioViewModel>(usuario);

            return View(usuarioViewModel);

        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var usuario = _usuarioApp.GetById(id);
            _usuarioApp.Remove(usuario);

            return RedirectToAction("List");

        }

        // GET: Usuarios/List
        public ActionResult List()
        {
            var usuarioViewModel =
                Mapper.Map<IEnumerable<Usuario>, IEnumerable<UsuarioViewModel>>(_usuarioApp.GetAll());

            return View(usuarioViewModel);
        }
    }
}
