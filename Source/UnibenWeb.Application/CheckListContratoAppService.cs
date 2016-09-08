using System.Collections.Generic;
using AutoMapper;
using UnibenWeb.Application.Interface;
using UnibenWeb.Application.Validation;
using UnibenWeb.Application.ViewModels;
using UnibenWeb.Domain.Entities;
using UnibenWeb.Domain.Interfaces.Services;

namespace UnibenWeb.Application
{
    public class CheckListContratoAppService : BaseAppService, ICheckListContratoAppService
    {

        private readonly ICKContratoService _ckContratoService;

        public CheckListContratoAppService(ICKContratoService ckContratoService)
        {
            _ckContratoService = ckContratoService;
        }

        public ValidationAppResult Adicionar(CheckListContratoVM pessoa)
        {
            throw new System.NotImplementedException();
        }

        public CheckListContratoVM BuscaPorId(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<CheckListContratoVM> BuscaTodos(int skip, int take)
        {
            var checkListContratos = _ckContratoService.BuscaTodos(skip, take);
            return Mapper.Map<IEnumerable<CheckListContrato>, IEnumerable<CheckListContratoVM>>(checkListContratos);
            //return Mapper.Map<IEnumerable<Pessoa>, IEnumerable<PessoaVM>>(_pessoaRepository.GetAll(skip,take));
        }

        public void Atualizar(CheckListContratoVM pessoa)
        {
            throw new System.NotImplementedException();
        }

        public void Remover(CheckListContratoVM pessoa)
        {
            throw new System.NotImplementedException();
        }
    }
}