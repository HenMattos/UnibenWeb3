using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UnibenWeb.Application.Interface;
using UnibenWeb.Application.ViewModels;
using UnibenWeb.Infra.CrossCutting.MvcFilters;
using UnibenWeb.UI.MVC.Models;

namespace UnibenWeb.UI.MVC.Controllers
{
    [RoutePrefix("Administrativo/ContasPagar")]
    [Route("{action=Listar}")]
    public class PagarContasController : Controller
    {
        private readonly IBaseAppService _baseAppService;
        private readonly IPagarContaAppService _pagarContaAppService;

        public PagarContasController(IBaseAppService baseAppService, IPagarContaAppService pagarContaAppService)
        {
            _baseAppService = baseAppService;
            _pagarContaAppService = pagarContaAppService;
        }

        // GET: PagarContas
        public ActionResult Listar()
        {
            return View(_baseAppService.Pesquisar<PagarContaVm>(0, 999, "", "PagarContas").FirstOrDefault());

        }

        public ActionResult Criar()
        {
            //ViewBag.Pessoas = _baseAppService.ListasDeSelecao<PessoaVM>("PessoaId", "Nome", "Pessoas", " Descricao = 'Fornecedor' ", " join [dbo].[PessoaTipos] tb2 on tb2.PessoaTipoId = tb.PessoaTipoId ", " tb.* ");
            ViewBag.Pessoas = _baseAppService.ListasDeSelecao<PessoaVM>("Pessoas", 0, " join [dbo].[PessoaTipos] tb2 on tb2.PessoaTipoId = tb.PessoaTipoId ", 0, "Descricao = 'Fornecedor'", " tb.* ", "Nome", "PessoaId", "Nome");
            ViewBag.CentroCustos = _baseAppService.ListasDeSelecao<CentroCustoVm>("CentroCustoId", "Descricao", "CentroCustos", "");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateAjax]
        public ActionResult Criar(PagarContaVm pagarContaVm)
        {
            if (ModelState.IsValid)
            {
                var result = _pagarContaAppService.Adicionar(true, User.Identity.GetUserId(), pagarContaVm);
                if (!result.IsValid)
                {
                    foreach (var validationAppError in result.Erros)
                    {
                        ModelState.AddModelError(string.Empty, validationAppError.Message);
                    }

                    return Json(new { Resultado = result });
                }
                return Json(new { Resultado = pagarContaVm.PagarContaId }, JsonRequestBehavior.AllowGet);
                //return RedirectToAction("index");
            }
            else
            {
                return Json(new { Validar = true });
            }

            //return View("ListaContas");
        }

        public ActionResult Detalhar(int id)
        {
            //var pagarConta =  _baseAppService.Pesquisar<PagarContaVm>(0, 999, " PagarContaId = " + Convert.ToString(id), "PagarContas");
            //return Json(new { PagarContaDetalhes = pagarConta }, JsonRequestBehavior.AllowGet);
            return View(_baseAppService.Pesquisar<PagarContaVm>(0, 0, " PagarContaId = " + Convert.ToString(id), "PagarContas"));
        }

        public ActionResult Editar(int id)
        {
            return View(_baseAppService.Pesquisar<PagarContaVm>(0, 999, " PagarContaId = " + Convert.ToString(id), "PagarContas"));
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirConfirmado(int id)
        {
            var pagarContaVm = _baseAppService.Pesquisar<PagarContaVm>(0, 0, " PagarContaId = " + Convert.ToString(id), "PagarContas").FirstOrDefault();
            _pagarContaAppService.Excluir(true, User.Identity.GetUserId(), pagarContaVm);
            return RedirectToAction("Index");
        }


        public ActionResult AjaxHandler(JQueryDataTableParamModel param)
        {
            var cont = _baseAppService.Pesquisar<int>("PagarContas", 0, "join [dbo].[CentroCustos] tb2 on tb2.CentroCustoId = tb.CentroCustoId", 0, "", " count(*) as NumRegistros ", "1");
            IEnumerable<ContaPagarComCentroCustoVm> filteredContas;
            //Check whether the companies should be filtered by keyword

            var calculatedParams = param.GetCalculatedParams<ContaPagarComCentroCustoVm>(Request.QueryString);

            filteredContas = _baseAppService.Pesquisar<ContaPagarComCentroCustoVm>(
                "PagarContas",
                param.iDisplayStart,
                "join [dbo].[CentroCustos] tb2 on tb2.CentroCustoId = tb.CentroCustoId",
                param.iDisplayLength,
                calculatedParams.ToArray()[0] + calculatedParams.ToArray()[1],
                " tb.*, tb2.Descricao as CentroCustoDescricao ",
                calculatedParams.ToArray()[2]);

            var result = from c in filteredContas // displayedContas 
                         select new[] {
                Convert.ToString(c.PagarContaId),
                Convert.ToString(c.Descricao),
                Convert.ToString(c.CentroCustoDescricao),
                Convert.ToString(c.NumeroParcelas),
                Convert.ToString(c.Observacao),
                Convert.ToString(c.TipoLancamento),
                Convert.ToString(c.ValorTotal)};

            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = cont.FirstOrDefault().ToString(),
                iTotalDisplayRecords = filteredContas.Count(),
                aaData = result
            }, JsonRequestBehavior.AllowGet);
        }



    }
}