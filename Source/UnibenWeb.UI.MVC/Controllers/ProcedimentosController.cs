using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using UnibenWeb.UI.MVC.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using UnibenWeb.Application.Interface;
using UnibenWeb.Application.ViewModels;
using UnibenWeb.Infra.CrossCutting.MvcFilters;

namespace UnibenWeb.UI.MVC.Controllers
{
    public class ProcedimentosController : Controller
    {
        // GET: Procedimentos
        public ActionResult Index()
        {
            return View();
        }
    }
}