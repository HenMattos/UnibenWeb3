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
    [RoutePrefix("Cadastro/Pessoa")]
    [Route("{action=index}")]
    //[Route("{idpessoa:int?}/Pessoa/Detalhe/{iddetalhe:int?}")]

    public class PessoasController : Controller
    {
        private readonly IBaseAppService _baseAppService;
        private readonly IBancoAppService _bancoAppService;
        private readonly IPessoaAppService _pessoaAppService;
        private readonly IEstadoCivilAppService _estadoCivilAppService;

        public PessoasController(
            IPessoaAppService pessoaAppService, 
            IBancoAppService bancoAppService, 
            IEstadoCivilAppService estadoCivilAppService, 
            IBaseAppService baseAppService)
        {
            _pessoaAppService = pessoaAppService;
            _bancoAppService = bancoAppService;
            _estadoCivilAppService = estadoCivilAppService;
            _baseAppService = baseAppService;
        }

        // GET: Pessoas
        public ActionResult Index()
        {
            return View(_pessoaAppService.BuscaTodos(0, 999)); // máximo de 50 pessoas, aprimorar paginação.
        }

        // GET: Pessoas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var pessoaVM = _pessoaAppService.BuscaPorId(id.Value); //id.value - se for null causa exception, no entanto o null foi tratado anteriormente.
            if (pessoaVM == null)
            {
                return HttpNotFound();
            }
            return View(pessoaVM);
        }

        // GET: Pessoas/Create
        public ActionResult Create()
        {
            ViewBag.Bancos = _bancoAppService.ListasDeSelecao();
            ViewBag.PessoaTipos = _baseAppService.ListasDeSelecao<PessoaTipoVM>("PessoaTipoId","Descricao", "PessoaTipos","");
            ViewBag.EstadoCivis = _baseAppService.ListasDeSelecao<EstadoCivilVM>("EstadoCivilId", "descricao", "EstadoCivis", "");
            ViewBag.Sexos = _baseAppService.ListasDeSelecao<PessoaSexoVm>("PessoaSexoId", "Descricao", "PessoaSexos", "");
            return View();
        }

        // POST: Pessoas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateAjax]
        public ActionResult Create(PessoaEnderecoVM pessoaEnderecoVM)
        {
            if (ModelState.IsValid)
            {
                var result = _pessoaAppService.Adicionar(true, User.Identity.GetUserId(),pessoaEnderecoVM);
                if (!result.IsValid)
                {
                    foreach (var validationAppError in result.Erros)
                    {
                        ModelState.AddModelError(string.Empty, validationAppError.Message);
                    }
                    /*
                    ViewBag.Bancos = _bancoAppService.ListasDeSelecao();
                    ViewBag.PessoaTipos = _baseAppService.ListasDeSelecao<PessoaTipoVM>("PessoaTipoId", "Descricao", "PessoaTipos", "");
                    ViewBag.EstadoCivis = _baseAppService.ListasDeSelecao<EstadoCivilVM>("EstadoCivilId", "descricao", "EstadoCivis", "");
                    */
                    return Json(new { Resultado = result });
                    // return View(pessoaEnderecoVM);
                }
                //return RedirectToAction("Index");
                return Json(new { Resultado = pessoaEnderecoVM.PessoaId }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                /*
                ViewBag.Bancos = _bancoAppService.ListasDeSelecao();
                ViewBag.PessoaTipos = _baseAppService.ListasDeSelecao<PessoaTipoVM>("PessoaTipoId", "Descricao", "PessoaTipos", "");
                ViewBag.EstadoCivis = _baseAppService.ListasDeSelecao<EstadoCivilVM>("EstadoCivilId", "descricao", "EstadoCivis", "");
                */
            }
            return Json(new { Validar = true });
            //return View(pessoaEnderecoVM);
        }

        // GET: Pessoas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var pessoaVM = _pessoaAppService.BuscaPorId(id.Value);
            if (pessoaVM == null)
            {
                return HttpNotFound();
            }

            var listaBancos = _bancoAppService.BuscaComPesquisa(0, 999, null);
            ViewBag.Bancos = new SelectList(listaBancos, "BancoId", "Nome");

            var listaEstadoCivis = _estadoCivilAppService.BuscaTodos(0, 999);
            ViewBag.EstadoCivis = new SelectList(listaEstadoCivis, "EstadoCivilId", "descricao");

            return View(pessoaVM);
        }

        // POST: Pessoas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PessoaId,Nome,CPF,DataNascimento,DataCadastro,Ativo,SexoId, BancoId, EstadoCivilId")] PessoaVM pessoaVM)
        {
            if (ModelState.IsValid)
            {
                _pessoaAppService.Atualizar(true, User.Identity.GetUserId(), pessoaVM);
                return RedirectToAction("Index");
            }

            var selList = _bancoAppService.BuscaComPesquisa(0, 999, null);
            ViewBag.Bancos = new SelectList(selList, "BancoId", "Nome");

            var listaEstadoCivis = _estadoCivilAppService.BuscaTodos(0, 999);
            ViewBag.EstadoCivis = new SelectList(listaEstadoCivis, "EstadoCivilId", "descricao");

            return View(pessoaVM);
        }

        // GET: Pessoas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var pessoaVM = _pessoaAppService.BuscaPorId(id.Value);
            if (pessoaVM == null)
            {
                return HttpNotFound();
            }
            return View(pessoaVM);
        }

        // POST: Pessoas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var pessoaVM = _pessoaAppService.BuscaPorId(id);
            _pessoaAppService.Remover(true, User.Identity.GetUserId(), pessoaVM);
            return RedirectToAction("Index");
        }

        public ActionResult AjaxHandler(JQueryDataTableParamModel param)
        {
            var pessoas = _pessoaAppService.BuscaTodos(0, 50);
            IEnumerable<PessoaVM> filteredCompanies;

            

            //Check whether the companies should be filtered by keyword
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                //Used if particulare columns are filtered 
                var nameFilter = Convert.ToString(Request["sSearch_1"]);
                var addressFilter = Convert.ToString(Request["sSearch_2"]);
                var townFilter = Convert.ToString(Request["sSearch_3"]);

                //Optionally check whether the columns are searchable at all 
                var isNameSearchable = Convert.ToBoolean(Request["bSearchable_1"]);
                var isAddressSearchable = Convert.ToBoolean(Request["bSearchable_2"]);
                var isTownSearchable = Convert.ToBoolean(Request["bSearchable_3"]);

                filteredCompanies = _pessoaAppService.BuscaTodos(0, 50)
                   .Where(c => isNameSearchable && c.Nome.ToLower().Contains(param.sSearch.ToLower())
                               ||
                               isAddressSearchable && c.Nome.ToLower().Contains(param.sSearch.ToLower())
                               ||
                               isTownSearchable && c.Nome.ToLower().Contains(param.sSearch.ToLower()));
            }
            else
            {
                filteredCompanies = pessoas;
            }

            var isNameSortable = Convert.ToBoolean(Request["bSortable_1"]);
            var isAddressSortable = Convert.ToBoolean(Request["bSortable_2"]);
            var isTownSortable = Convert.ToBoolean(Request["bSortable_3"]);
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            Func<PessoaVM, string> orderingFunction = (c => sortColumnIndex == 1 && isNameSortable ? c.Nome :
                                                           sortColumnIndex == 2 && isAddressSortable ? c.Nome :
                                                           sortColumnIndex == 3 && isTownSortable ? c.Nome :
                                                           "");

            var sortDirection = Request["sSortDir_0"];
            if (sortDirection == "asc")
                filteredCompanies = filteredCompanies.OrderBy(orderingFunction);
            else
                filteredCompanies = filteredCompanies.OrderByDescending(orderingFunction);

            var displayedCompanies = filteredCompanies.Skip(param.iDisplayStart).Take(param.iDisplayLength);
            var result = from c in displayedCompanies select new[] { Convert.ToString(c.PessoaId), c.Nome, c.Nome, c.Nome };
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = pessoas.Count(),
                iTotalDisplayRecords = filteredCompanies.Count(),
                aaData = result
            }, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _pessoaAppService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
