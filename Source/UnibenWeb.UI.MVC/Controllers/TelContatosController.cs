using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UnibenWeb.Application.Interface;
using UnibenWeb.Application.ViewModels;
using UnibenWeb.Infra.Data.Context;

namespace UnibenWeb.UI.MVC.Controllers
{
    public class TelContatosController : Controller
    {
        // GET: TelContatos
        public ActionResult Index()
        {
            return View();
        }
    }
}