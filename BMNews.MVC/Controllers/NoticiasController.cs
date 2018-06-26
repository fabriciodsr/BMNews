using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using BMNews.App.Interface;
using BMNews.Domain.Entities;
using BMNews.MVC.ViewModels;

namespace BMNews.MVC.Controllers
{
    public class NoticiasController : Controller
    {
        
        private readonly INoticiaAppService _noticiaApp;
        private readonly IUsuarioAppService _usuarioApp;

        public NoticiasController(INoticiaAppService noticiaApp, IUsuarioAppService usuarioApp)
        {
            _noticiaApp = noticiaApp;
            _usuarioApp = usuarioApp;
        }

        public ActionResult Detalhes(int id)
        {
            var noticia = _noticiaApp.GetById(id);
            var noticiaViewModel = Mapper.Map<Noticia, NoticiaViewModel>(noticia);

            ViewBag.UsuarioId = new SelectList(_usuarioApp.GetAll(), "UsuarioId", "Nome", noticiaViewModel.UsuarioId);

            return View(noticiaViewModel);
        }

        // GET: Noticias
        public ActionResult Index()
        {
            var noticiaViewModel = Mapper.Map<IEnumerable<Noticia>, IEnumerable<NoticiaViewModel>>(_noticiaApp.GetAll().Where(n => n.Disponivel));

            return View(noticiaViewModel);
        }

        //Froala
        [HttpPost]
        public ActionResult FroalaUploadImage(HttpPostedFileBase file, int? postId)
        {
            var fileName = Path.GetFileName(file.FileName);
            var rootPath = Server.MapPath("~/Images/Upload");
            file.SaveAs(Path.Combine(rootPath, fileName));
            return Json(new { link = "Images/Upload/" + fileName }, JsonRequestBehavior.AllowGet);
        }


        // GET: Produto/Details/5
        [Authorize(Roles = "admin")]
        public ActionResult Details(int id)
        {
            var noticia = _noticiaApp.GetById(id);
            var noticiaViewModel = Mapper.Map<Noticia, NoticiaViewModel>(noticia);

            ViewBag.UsuarioId = new SelectList(_usuarioApp.GetAll(), "UsuarioId", "Nome", noticiaViewModel.UsuarioId);

            return View(noticiaViewModel);
        }

        // GET: Noticias/List
        [Authorize(Roles = "admin")]
        public ActionResult List()
        {
            var noticiaViewModel = Mapper.Map<IEnumerable<Noticia>, IEnumerable<NoticiaViewModel>>(_noticiaApp.GetAll());

            return View(noticiaViewModel);
        }

        // GET: Produto/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            ViewBag.UsuarioId = new SelectList(_usuarioApp.GetAll(), "UsuarioId", "Nome");
            return View();
        }

        // POST: Produto/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NoticiaViewModel noticia)
        {
            Server.HtmlEncode(noticia.Conteudo);

            if (ModelState.IsValid)
            {
                var noticiaDomain = Mapper.Map<NoticiaViewModel, Noticia>(noticia);
                _noticiaApp.Add(noticiaDomain);

                return RedirectToAction("List");
            }

            ViewBag.UsuarioId = new SelectList(_usuarioApp.GetAll(), "UsuarioId", "Nome", noticia.UsuarioId);
            return View(noticia);
        }

        // GET: Produto/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int id)
        {
            var noticia = _noticiaApp.GetById(id);
            var noticiaViewModel = Mapper.Map<Noticia, NoticiaViewModel>(noticia);

            ViewBag.UsuarioId = new SelectList(_usuarioApp.GetAll(), "UsuarioId", "Nome", noticiaViewModel.UsuarioId);

            return View(noticiaViewModel);
        }

        // POST: Produto/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(NoticiaViewModel noticia)
        {
            Server.HtmlEncode(noticia.Conteudo);

            if (ModelState.IsValid)
            {
                var noticiaDomain = Mapper.Map<NoticiaViewModel, Noticia>(noticia);
                _noticiaApp.Update(noticiaDomain);

                return RedirectToAction("List");
            }

            ViewBag.UsuarioId = new SelectList(_usuarioApp.GetAll(), "UsuarioId", "Nome", noticia.UsuarioId);
            return View(noticia);
        }

        // GET: Produto/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            var noticia = _noticiaApp.GetById(id);
            var noticiaViewModel = Mapper.Map<Noticia, NoticiaViewModel>(noticia);

            return View(noticiaViewModel);
        }

        // POST: Produto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var noticia = _noticiaApp.GetById(id);
            _noticiaApp.Remove(noticia);

            return RedirectToAction("List");
        }

        /////////////////
        /// 
        /// 
        [Authorize(Roles = "comum")]
        public ActionResult MinhasNoticias()
        {
            var noticiaViewModel = Mapper.Map<IEnumerable<Noticia>, IEnumerable<NoticiaViewModel>>(_noticiaApp.GetAll()
                .Where(n => n.UsuarioId == Convert.ToInt32(Session["usuarioLogadoID"])));

            return View(noticiaViewModel);
        }

        [Authorize(Roles = "comum")]
        public ActionResult CriarNoticia()
        {
            ViewBag.UsuarioId = new SelectList(_usuarioApp.GetAll().Where(n => n.UsuarioId == Convert.ToInt32(Session["usuarioLogadoID"])), "UsuarioId", "Nome");

            return View();
        }

        // POST: Produto/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CriarNoticia(NoticiaViewModel noticia)
        {
            Server.HtmlEncode(noticia.Conteudo);

            if (ModelState.IsValid)
            {
                var noticiaDomain = Mapper.Map<NoticiaViewModel, Noticia>(noticia);
                _noticiaApp.Add(noticiaDomain);

                return RedirectToAction("MinhasNoticias");
            }

            ViewBag.UsuarioId = new SelectList(_usuarioApp.GetAll().Where(n => n.UsuarioId == Convert.ToInt32(Session["usuarioLogadoID"])), "UsuarioId", "Nome", noticia.UsuarioId);
            return View(noticia);
        }

        [Authorize(Roles = "comum")]
        public ActionResult EditarNoticia(int id)
        {
            var noticia = _noticiaApp.GetById(id);
            var noticiaViewModel = Mapper.Map<Noticia, NoticiaViewModel>(noticia);

            ViewBag.UsuarioId = new SelectList(_usuarioApp.GetAll().Where(n => n.UsuarioId == Convert.ToInt32(Session["usuarioLogadoID"])), "UsuarioId", "Nome", noticiaViewModel.UsuarioId);

            if (noticiaViewModel.UsuarioId != Convert.ToInt32(Session["usuarioLogadoID"]))
            {
                return RedirectToAction("MinhasNoticias");
            }

            return View(noticiaViewModel);
        }

        // POST: Produto/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarNoticia(NoticiaViewModel noticia)
        {
            Server.HtmlEncode(noticia.Conteudo);

            if (ModelState.IsValid)
            {
                var noticiaDomain = Mapper.Map<NoticiaViewModel, Noticia>(noticia);
                _noticiaApp.Update(noticiaDomain);

                return RedirectToAction("MinhasNoticias");
            }

            ViewBag.UsuarioId = new SelectList(_usuarioApp.GetAll().Where(n => n.UsuarioId == Convert.ToInt32(Session["usuarioLogadoID"])), "UsuarioId", "Nome", noticia.UsuarioId);
            return View(noticia);
        }

        [Authorize(Roles = "comum")]
        public ActionResult DeletarNoticia(int id)
        {
            var noticia = _noticiaApp.GetById(id);
            var noticiaViewModel = Mapper.Map<Noticia, NoticiaViewModel>(noticia);

            if (noticiaViewModel.UsuarioId != Convert.ToInt32(Session["usuarioLogadoID"]))
            {
                return RedirectToAction("MinhasNoticias");
            }

            return View(noticiaViewModel);
        }

        // POST: Produto/Delete/5
        [HttpPost, ActionName("DeletarNoticia")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletarNoticiaConfirmed(int id)
        {
            var noticia = _noticiaApp.GetById(id);
            _noticiaApp.Remove(noticia);

            return RedirectToAction("MinhasNoticias");
        }
    }

}