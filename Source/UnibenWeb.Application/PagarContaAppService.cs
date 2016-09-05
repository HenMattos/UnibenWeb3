using System;
using AutoMapper;
using UnibenWeb.Application.Interface;
using UnibenWeb.Application.Validation;
using UnibenWeb.Application.ViewModels;
using UnibenWeb.Domain.Entities;
using UnibenWeb.Domain.Interfaces.Services;
using System.Collections.Generic;

namespace UnibenWeb.Application
{
    public class PagarContaAppService : BaseAppService, IPagarContaAppService
    {
        private readonly IPagarContaService _pagarContaService;
        private readonly IBaseService _baseService;
        private readonly ICentroCustoService _centroCustoService;

        public PagarContaAppService(IPagarContaService pagarContaService, IBaseService baseService, ICentroCustoService centroCustoService)
        {
            _pagarContaService = pagarContaService;
            _baseService = baseService;
            _centroCustoService = centroCustoService;
        }

        public ValidationAppResult Adicionar(bool doLog, string userId, PagarContaVm pagarContaVm)
        {
            var pagarConta = Mapper.Map<PagarContaVm, PagarConta>(pagarContaVm);
            pagarConta.CentrosCusto = new List<CentroCusto>();
            var commaDelimitedListOfCentrosCusto = string.Join(",", (IList < int >)pagarContaVm.CentroCustoId).ToString();
            var centrosCustoVm = _baseService.Pesquisar<CentroCustoVm>(0, 0, " CentroCustoId in (" + commaDelimitedListOfCentrosCusto + ")", "CentroCustos");

            foreach (var _centroCustoVm in centrosCustoVm)
            {
                var centroCusto = Mapper.Map<CentroCustoVm, CentroCusto>(_centroCustoVm);
                _centroCustoService.VincularObjetoContexto(centroCusto);
                pagarConta.CentrosCusto.Add(centroCusto);
            }

            var auxValor = pagarConta.ValorTotal;
            var auxVencimentos = DateTime.Now;
            pagarConta.ContaParcelas = new List<PagarContaParcela>();

            for (int i = 0; i < pagarConta.NumeroParcelas; i++)
            {
                auxVencimentos = auxVencimentos.AddMonths(1);
                var novaParcela = new PagarContaParcela { ValorParcela = (pagarConta.ValorTotal / pagarConta.NumeroParcelas), ContaOrigem = pagarConta, DataPagamento = null, DataVencimento = auxVencimentos, Desconto = 0, Juros = 0, Descricao = "", Observacao = "", Status = false};
                pagarConta.ContaParcelas.Add(novaParcela);
            }

            BeginTransaction();
            var result =  _pagarContaService.Adicionar(pagarConta);
            if (!result.IsValid) { return DomainToApllicationResult(result); }
            Commit(doLog, userId);
            pagarContaVm.PagarContaId = pagarConta.PagarContaId;
            return DomainToApllicationResult(result);
        }

        public void Excluir(bool doLog, string userId, PagarContaVm pagarContaVm)
        {
            var pagarConta = Mapper.Map<PagarContaVm, PagarConta>(pagarContaVm);
            BeginTransaction();
            _pagarContaService.Excluir(pagarConta);
            Commit(doLog, userId);
        }


    }
}
