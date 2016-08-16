﻿using System;
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

        public PagarContaAppService(IPagarContaService pagarContaService, IBaseService baseService)
        {
            _pagarContaService = pagarContaService;
            _baseService = baseService;
        }

        public ValidationAppResult Adicionar(bool doLog, string userId, PagarContaVm pagarContaVm)
        {
            var pagarConta = Mapper.Map<PagarContaVm, PagarConta>(pagarContaVm);
            var auxValor = pagarConta.ValorTotal;
            var auxVencimentos = DateTime.Now;
            pagarConta.contaParcelas = new List<PagarContaParcela>();
            for (int i = 0; i < pagarConta.NumeroParcelas; i++)
            {
                var novaParcela = new PagarContaParcela { ValorParcela = (pagarConta.ValorTotal / pagarConta.NumeroParcelas), contaOrigem = pagarConta, DataPagamento = null, DataVencimento = auxVencimentos.AddMonths(1), Desconto = 0, Juros = 0, Descricao = "", Observacao = "", Status = false};
                pagarConta.contaParcelas.Add(novaParcela);
            }
            BeginTransaction();
            var result =  _pagarContaService.Adicionar(pagarConta);
            if (!result.IsValid) { return DomainToApllicationResult(result); }
            Commit(doLog, userId);
            pagarContaVm.PagarContaId = pagarConta.PagarContaId;
            return DomainToApllicationResult(result);
        }

    }
}
