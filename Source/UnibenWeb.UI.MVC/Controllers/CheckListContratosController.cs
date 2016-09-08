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
    public class CheckListContratosController : Controller
    {
        private readonly ICheckListContratoAppService _ckContratoAppService;

        public CheckListContratosController(ICheckListContratoAppService ckContratoAppService)
        {
            _ckContratoAppService = ckContratoAppService;
        }

        // GET: CheckListContratos
        public ActionResult Index()
        {
            return View(_ckContratoAppService.BuscaTodos(0,50));
        }

        // GET: CheckListContratos/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            CheckListContratoVM checkListContratoVM = _ckContratoAppService.BuscaPorId(id);
            /*
            if (checkListContratoVM == null)
            {
                return HttpNotFound();
            }
            */
            return View(checkListContratoVM);
            
        }

        // GET: CheckListContratos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CheckListContratos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CheckListContratoId,CheckItem")] CheckListContratoVM checkListContratoVM)
        {
            if (ModelState.IsValid)
            {
                //db.CheckListContratoVMs.Add(checkListContratoVM);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(checkListContratoVM);
        }

        // GET: CheckListContratos/Edit/5
        /*
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //CheckListContratoVM checkListContratoVM = db.CheckListContratoVMs.Find(id);
            if (checkListContratoVM == null)
            {
                return HttpNotFound();
            }
            return View(checkListContratoVM);
        }
        */

        // POST: CheckListContratos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CheckListContratoId,CheckItem")] CheckListContratoVM checkListContratoVM)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(checkListContratoVM).State = EntityState.Modified;
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(checkListContratoVM);
        }

        // GET: CheckListContratos/Delete/5
        /*
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //CheckListContratoVM checkListContratoVM = db.CheckListContratoVMs.Find(id);
            if (checkListContratoVM == null)
            {
                return HttpNotFound();
            }
            return View(checkListContratoVM);
        }
        */

        // POST: CheckListContratos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //CheckListContratoVM checkListContratoVM = db.CheckListContratoVMs.Find(id);
            //db.CheckListContratoVMs.Remove(checkListContratoVM);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
