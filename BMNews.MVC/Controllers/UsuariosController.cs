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
    [Authorize(Roles = "comum")] //roles sendo identificadas no Global.asax.cs
    public class UsuariosController : Controller
    {
        private readonly INoticiaAppService _noticiaApp;
        private readonly IUsuarioAppService _usuarioApp;

        public UsuariosController(INoticiaAppService noticiaApp, IUsuarioAppService usuarioApp)
        {
            _noticiaApp = noticiaApp;
            _usuarioApp = usuarioApp;
        }

        // GET: Usuarios
        public ActionResult Index()
        {
            var TodasNoticias =
                Mapper.Map<IEnumerable<Noticia>, IEnumerable<NoticiaViewModel>>(_noticiaApp.GetAll().Where(x => x.UsuarioId == Convert.ToInt32(Session["usuarioLogadoID"])));

            var NoticiasDesativadas =
                Mapper.Map<IEnumerable<Noticia>, IEnumerable<NoticiaViewModel>>(_noticiaApp.GetAll().Where(x => x.UsuarioId == Convert.ToInt32(Session["usuarioLogadoID"]) && x.Disponivel == false));

            var contTotalNoticia = TodasNoticias.Count();
            var contNoticiaDstv = NoticiasDesativadas.Count();

            Session["QuantNoti"] = contTotalNoticia;
            Session["QuantNotiDstv"] = contNoticiaDstv;

            return View();
        }

        
        // GET: Usuarios/Details/5
        public ActionResult Details(int id)
        {
            var usuario = _usuarioApp.GetById(id);
            var usuarioViewModel = Mapper.Map<Usuario, UsuarioViewModel>(usuario);

            if (usuarioViewModel.UsuarioId != Convert.ToInt32(Session["usuarioLogadoID"]))
            {
                return RedirectToAction("Details");
            }

            ViewBag.UsuarioId = Convert.ToInt32(Session["usuarioLogadoID"]);


            return View(usuarioViewModel);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int id)
        {
            var usuario = _usuarioApp.GetById(id);
            var usuarioViewModel = Mapper.Map<Usuario, UsuarioViewModel>(usuario);

            ViewBag.UsuarioId = new SelectList(_usuarioApp.GetAll(), Convert.ToInt32(Session["usuarioLogadoID"]), "Nome");

            if (usuarioViewModel.UsuarioId != Convert.ToInt32(Session["usuarioLogadoID"]))
            {
                return RedirectToAction("Edit");
            }

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

                return RedirectToAction("List");
            }

            return View(usuario);

        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int id)
        {
            var usuario = _usuarioApp.GetById(id);
            var usuarioViewModel = Mapper.Map<Usuario, UsuarioViewModel>(usuario);

            if (usuarioViewModel.UsuarioId != Convert.ToInt32(Session["usuarioLogadoID"]))
            {
                return RedirectToAction("Delete");
            }

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
                Mapper.Map<IEnumerable<Usuario>, IEnumerable<UsuarioViewModel>>(_usuarioApp.GetAll().Where(n => n.UsuarioId == Convert.ToInt32(Session["usuarioLogadoID"])));

            return View(usuarioViewModel);
        }
    }
}
