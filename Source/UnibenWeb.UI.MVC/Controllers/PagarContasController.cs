using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UnibenWeb.Application.Interface;
using UnibenWeb.Application.ViewModels;
using UnibenWeb.Infra.CrossCutting.MvcFilters;
using UnibenWeb.UI.MVC.Models;

namespace UnibenWeb.UI.MVC.Controllers
{
    [RoutePrefix("Administrativo/ContasPagar")]
    [Route("{action=ListarContas}")]
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
        public ActionResult ListarContas()
        {
            return View(_baseAppService.Pesquisar<PagarContaVm>(0, 999, "", "PagarContas"));

        }


        public ActionResult CriarConta()
        {
            //ViewBag.Pessoas = _baseAppService.ListasDeSelecao<PessoaVM>("PessoaId", "Nome", "Pessoas", " Descricao = 'Fornecedor' ", " join [dbo].[PessoaTipos] tb2 on tb2.PessoaTipoId = tb.PessoaTipoId ", " tb.* ");
            ViewBag.Pessoas = _baseAppService.ListasDeSelecao<PessoaVM>("Pessoas", 0, " join [dbo].[PessoaTipos] tb2 on tb2.PessoaTipoId = tb.PessoaTipoId ", 0, "Descricao = 'Fornecedor'", " tb.* ", "Nome", "PessoaId", "Nome");
            ViewBag.CentroCustos = _baseAppService.ListasDeSelecao<CentroCustoVm>("CentroCustoId", "Descricao", "CentroCustos", "");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateAjax]
        public ActionResult CriarConta(PagarContaVm pagarContaVm)
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

        public ActionResult Detalhes(int id)
        {
            return View(_baseAppService.Pesquisar<PagarContaVm>(0, 999, " PagarContaId = " + Convert.ToString(id), "PagarContas"));
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


        public ActionResult PagarContaParcelas_DataTableAjaxHandler(JQueryDataTableParamModel param)
        {
            var id = HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["id"];
            IEnumerable<PagarContaParcelaVm> filteredContas;
            //Check whether the companies should be filtered by keyword
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                //Used if particulare columns are filtered 
                var Col_01_Filter = Convert.ToString(Request["sSearch_1"]);
                var Col_02_Filter = Convert.ToString(Request["sSearch_2"]);
                var Col_03_Filter = Convert.ToString(Request["sSearch_3"]);

                //Optionally check whether the columns are searchable at all 
                var isCol_01_Searchable = Convert.ToBoolean(Request["bSearchable_1"]);
                var isCol_02_Searchable = Convert.ToBoolean(Request["bSearchable_2"]);
                var isCol_03_Searchable = Convert.ToBoolean(Request["bSearchable_3"]);



                filteredContas = _baseAppService.Pesquisar<PagarContaParcelaVm>(0, 999, " contaOrigem_PagarContaId = " + id, "PagarContaParcelas")
                   .Where(c => isCol_01_Searchable && (Convert.ToString(c.Descricao)).ToLower().Contains(param.sSearch.ToLower())
                               ||
                               isCol_02_Searchable && (Convert.ToString(c.Observacao)).ToLower().Contains(param.sSearch.ToLower())
                               ||
                               isCol_03_Searchable && (Convert.ToString(c.DataVencimento)).ToLower().Contains(param.sSearch.ToLower())
                               ||
                               isCol_03_Searchable && (Convert.ToString(c.DataPagamento)).ToLower().Contains(param.sSearch.ToLower())
                               ||
                               isCol_03_Searchable && (Convert.ToString(c.ValorParcela)).ToLower().Contains(param.sSearch.ToLower())
                               ||
                               isCol_03_Searchable && (Convert.ToString(c.Juros)).ToLower().Contains(param.sSearch.ToLower())
                               ||
                               isCol_03_Searchable && (Convert.ToString(c.Desconto)).ToLower().Contains(param.sSearch.ToLower())
                               ||
                               isCol_03_Searchable && (Convert.ToString(c.Status)).ToLower().Contains(param.sSearch.ToLower())
                               );
            }
            else
            {
                filteredContas = _baseAppService.Pesquisar<PagarContaParcelaVm>(0, 999, " contaOrigem_PagarContaId = " + id, "PagarContaParcelas"); ;
            }

            var isCol_01_Sortable = Convert.ToBoolean(Request["bSortable_1"]);
            var isCol_02_Sortable = Convert.ToBoolean(Request["bSortable_2"]);
            var isCol_03_Sortable = Convert.ToBoolean(Request["bSortable_3"]);
            var isCol_04_Sortable = Convert.ToBoolean(Request["bSortable_4"]);
            var isCol_05_Sortable = Convert.ToBoolean(Request["bSortable_5"]);

            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

            Func<PagarContaParcelaVm, string> orderingFunction = (c => sortColumnIndex == 1 && isCol_01_Sortable ? Convert.ToString(c.Descricao) :
                                                           sortColumnIndex == 2 && isCol_02_Sortable ? Convert.ToString(c.Observacao) :
                                                           sortColumnIndex == 3 && isCol_03_Sortable ? Convert.ToString(c.DataVencimento) :
                                                           sortColumnIndex == 4 && isCol_04_Sortable ? Convert.ToString(c.DataPagamento) :
                                                           sortColumnIndex == 5 && isCol_05_Sortable ? Convert.ToString(c.ValorParcela) :
                                                           sortColumnIndex == 5 && isCol_05_Sortable ? Convert.ToString(c.Juros) :
                                                           sortColumnIndex == 6 && isCol_05_Sortable ? Convert.ToString(c.Desconto) :
                                                           sortColumnIndex == 7 && isCol_05_Sortable ? Convert.ToString(c.Status) :
                                                           "");


            var sortDirection = Request["sSortDir_0"];
            if (sortDirection == "asc")
                filteredContas = filteredContas.OrderBy(orderingFunction);
            else
                filteredContas = filteredContas.OrderByDescending(orderingFunction);

            var displayedContas = filteredContas.Skip(param.iDisplayStart).Take(param.iDisplayLength);
            var result = from c in displayedContas
                         select new[] {
                Convert.ToString(c.PagarContaParcelaId),
                Convert.ToString(c.Descricao),
                Convert.ToString(c.Observacao),
                Convert.ToString(c.DataVencimento),
                Convert.ToString(c.DataPagamento),
                Convert.ToString(c.Juros),
                Convert.ToString(c.Desconto),
                Convert.ToString(c.ValorParcela),
                Convert.ToString(c.Status)
                         };
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = filteredContas.Count(),
                iTotalDisplayRecords = filteredContas.Count(),
                aaData = result
            }, JsonRequestBehavior.AllowGet);
        }


    }
}