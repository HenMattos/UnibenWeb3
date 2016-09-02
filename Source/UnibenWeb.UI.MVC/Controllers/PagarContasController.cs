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

        // GET: PagarContas
        public ActionResult Teste()
        {
            return View();

        }

        public ActionResult Criar()
        {
            //ViewBag.Pessoas = _baseAppService.ListasDeSelecao<PessoaVM>("PessoaId", "Nome", "Pessoas", " Descricao = 'Fornecedor' ", " join [dbo].[PessoaTipos] tb2 on tb2.PessoaTipoId = tb.PessoaTipoId ", " tb.* ");
            var defaultListItem = new SelectListItem { Text = "Selecione...", Value = "False" };
            ViewBag.Pessoas = _baseAppService.ListasDeSelecao<PessoaVM>("Pessoas", 0, " join [dbo].[PessoaTipos] tb2 on tb2.PessoaTipoId = tb.PessoaTipoId ", 0, "TipoDescricao = 'Fornecedor'", " tb.*, tb2.Descricao as TipoDescricao ", "Nome", "PessoaId", "Nome");

           // ((SelectList)ViewBag.Pessoas).

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
            var pagarConta = _baseAppService.Pesquisar<PagarContaVm>(0, 0, " PagarContaId = " + Convert.ToString(id), "PagarContas").FirstOrDefault();
            //return Json(new { PagarContaDetalhes = pagarConta }, JsonRequestBehavior.AllowGet);
            //return PartialViewResult("_Detalhar", pagarConta);
            //return View(_baseAppService.Pesquisar<PagarContaVm>(0, 0, " PagarContaId = " + Convert.ToString(id), "PagarContas"));


            if (true /*There is no error in your model*/)
            {
                //YourModelType model = new YourModelType();


                // initialize an instance of YourModelType
                // using ...args...
                //pagarConta.ItemOption.Active = ...;
                //pagarConta.ItemOption.ItemOptionCode = ...;
                //pagarConta.ItemOption.Name = ...;

                return PartialView("_Detalhar", pagarConta);
            }
            else
            {
                // You can choose whatever http status code makes sense
                // e.g 301, 400 etc..
                //return HttpStatusCodeResult(400, "An error occurred");
                return PartialView("_Detalhar", pagarConta);
            }
        }

        public ActionResult Editar(int id)
        {
            var pagarContaVm = (_baseAppService.Pesquisar<PagarContaVm>(0, 0, " PagarContaId = " + Convert.ToString(id), "PagarContas"));
            return PartialView("_Editar", pagarContaVm);
        }

        public ActionResult Excluir(int id)
        {
            var pagarContaVm = (_baseAppService.Pesquisar<PagarContaVm>(0, 0, " PagarContaId = " + Convert.ToString(id), "PagarContas").FirstOrDefault());
            return PartialView("_Excluir", pagarContaVm);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirConfirmado(int id)
        {
            var pagarContaVm = _baseAppService.Pesquisar<PagarContaVm>(0, 0, " PagarContaId = " + Convert.ToString(id), "PagarContas").FirstOrDefault();
            _pagarContaAppService.Excluir(true, User.Identity.GetUserId(), pagarContaVm);
            return RedirectToAction("Listar");
        }


        public ActionResult AjaxHandler(JQueryDataTableParamModel param)
        {
            var cont = _baseAppService.Pesquisar<int>("PagarContas", 0, "", 0, "", " count(*) as NumRegistros ", "1");
            IEnumerable<ContaPagarComCentroCustoVm> filteredContas;
            //Check whether the companies should be filtered by keyword
            var calculatedParams = param.GetCalculatedParams<ContaPagarComCentroCustoVm>(Request.QueryString);

            var auxSelect = ", (COALESCE((SELECT CAST(tb3.Descricao AS VARCHAR(55)) + ', ' AS[text()] FROM[CentroCustos] AS tb3 "
                + " join [dbo].[ContaPagarCentroCusto] tb2 on tb3.CentroCustoId = tb2.CentroCustoId"
         + " where tb2.ContaPagarId = tb.PagarContaId ORDER BY tb3.CentroCustoId"
         + " FOR XML PATH(''), TYPE).value('.[1]', 'VARCHAR(8000)'), '')) as CentrosCustoDescricao";
            
            filteredContas = _baseAppService.Pesquisar<ContaPagarComCentroCustoVm>(
                "PagarContas",
                param.iDisplayStart,
                "", //" join [dbo].[ContaPagarCentroCusto] tb2 on tb2.ContaPagarId = tb.PagarContaId join [dbo].[CentroCustos] tb3 on tb3.CentroCustoId = tb2.CentroCustoId ",
                param.iDisplayLength,
                calculatedParams.ToArray()[0] + calculatedParams.ToArray()[1],
                " tb.* " + auxSelect, // , tb3.Descricao as CentrosCustoDescricao
                calculatedParams.ToArray()[2]);


            //var commaDelimitedListOfCentrosCusto = string.Join(",", (IList<int>)pagarContaVm.CentroCustoId).ToString();

            var result = from c in filteredContas // displayedContas 
                         select new[] {
                Convert.ToString(c.PagarContaId),
                Convert.ToString(c.Descricao),
                Convert.ToString(c.CentrosCustoDescricao),
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