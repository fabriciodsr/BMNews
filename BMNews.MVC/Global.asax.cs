using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using AutoMapper;
using BMNews.App.Interface;
using BMNews.Domain.Entities;
using BMNews.MVC.AutoMapper;
using BMNews.MVC.ViewModels;
using System.Web;
using BMNews.Data.Context;

namespace BMNews.MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {

        //private readonly IUsuarioAppService _usuarioApp;

        //public MvcApplication(IUsuarioAppService usuarioApp)
        //{
        //    _usuarioApp = usuarioApp;
        //}

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AutoMapperConfig.RegisterMappings();
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            if (FormsAuthentication.CookiesSupported == true)
            {
                if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                {
                    try
                    {
                        string username = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
                        string roles = string.Empty;

                        using (BMNewsContext Db = new BMNewsContext())
                        {
                            var user = Db.Usuarios.SingleOrDefault(u => u.Login == username);

                            if (user.Admin == true)
                            {
                                roles = "admin";
                            }
                            else if (user.Admin == false)
                            {
                                roles = "comum";
                            }
                            else
                            {
                                roles = null;
                            }
                        }

                        HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(
                            new System.Security.Principal.GenericIdentity(username, "Forms"), roles.Split(';'));
                    }
                    catch (Exception)
                    {
                        //Erro
                    }
                }
            }
        }

    }
}
