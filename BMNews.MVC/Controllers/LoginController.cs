using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using BMNews.App.Interface;
using BMNews.Domain.Entities;
using BMNews.Helpers;
using BMNews.MVC.ViewModels;

namespace BMNews.MVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioAppService _usuarioApp;

        public LoginController(IUsuarioAppService usuarioApp)
        {
            _usuarioApp = usuarioApp;
        }

        // GET: Login
        [Authorize]
        [Authorize(Roles = "comum")] //roles sendo identificadas no Global.asax.cs
        public ActionResult UsuarioIndex()
        {
            return RedirectToAction("Index", "Usuarios");
        }

        [Authorize(Roles = "admin")] //roles sendo identificadas no Global.asax.cs
        public ActionResult AdminIndex()
        {
            return RedirectToAction("Index", "Administradores");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogarAJAX(string login, string senha)
        {
            if (ModelState.IsValid)
            {
                string username = login;
                string password = Criptografia.Codificador(senha);

                var usuarioViewModel = Mapper.Map<IEnumerable<Usuario>, IEnumerable<UsuarioViewModel>>(_usuarioApp
                    .GetAll().Where(x => x.Login.Equals(username) && x.Senha.Equals(password))).FirstOrDefault();

                //if (usuarioViewModel.Count() > 0)
                //{
                //    if (Convert.ToBoolean(usuarioViewModel.FirstOrDefault(x => x.Ativo)) == false)
                //    {
                //        return Content("O Login está inativo!\n Consulte um Administrador para reaver seu acesso.");
                //    }
                //    else
                //    {
                //        return Content("logar");
                //    }

                //}

                if (usuarioViewModel != null)
                {
                    switch (usuarioViewModel.Ativo)
                    {
                        case true:
                        {
                            return Content("logar");
                        }
                        break;
                        case false:
                        {
                            return Content("O Login está inativo!\n Consulte um Administrador para reaver seu acesso.");
                        }
                        break;
                    }
                }
            }

            return Content("Login ou Senha inválidos!");
        }


        [HttpPost]
        public ActionResult Login(string login, string senha)
        {
            if (ModelState.IsValid)
            {
                string username = login;
                string password = Criptografia.Codificador(senha);

                var usuarioViewModel = Mapper.Map<IEnumerable<Usuario>, IEnumerable<UsuarioViewModel>>(_usuarioApp
                    .GetAll().Where(x => x.Login.Equals(username) && x.Senha.Equals(password))).FirstOrDefault();

                if (usuarioViewModel != null)
                {
                    switch (usuarioViewModel.Ativo)
                    {
                        case true:
                        {
                            switch (usuarioViewModel.Admin)
                            {
                                case true:
                                {
                                    FormsAuthentication.SetAuthCookie(username, true);
                                    Session["usuarioLogadoID"] = usuarioViewModel.UsuarioId;
                                    Session["nomeUsuarioLogado"] = usuarioViewModel.Nome;
                                    return RedirectToAction("AdminIndex", "Login");
                                }
                                break;
                                case false:
                                {
                                    FormsAuthentication.SetAuthCookie(username, true);
                                    Session["usuarioLogadoID"] = usuarioViewModel.UsuarioId;
                                    Session["nomeUsuarioLogado"] = usuarioViewModel.Nome;
                                    return RedirectToAction("UsuarioIndex", "Login");
                                }
                                break;
                            }
                            
                        }
                        break;
                        case false:
                        {
                            return Content("O Login está inativo!\n Consulte um Administrador para reaver seu acesso.");
                        }
                        break;
                    }
                }
            }
            return Content("Login ou Senha inválidos!");
        }


        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            Session.Clear();

            return RedirectToAction("Index", "Home");
        }
    }
}