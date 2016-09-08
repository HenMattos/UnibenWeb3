using System.Collections.Generic;
using UnibenWeb.Domain.Entities;
using UnibenWeb.Domain.Interfaces.Repository.ReadOnly;
using UnibenWeb.Domain.Interfaces.Services;
using UnibenWeb.Domain.ValueObjects;

namespace UnibenWeb.Domain.Services
{
    public class CheckListContratoService: ICKContratoService
    {

        private readonly ICKContratoReadOnlyRepository _cKContratoReadOnlyRepository;

        public CheckListContratoService(ICKContratoReadOnlyRepository cKContratoReadOnlyRepository)
        {
            _cKContratoReadOnlyRepository = cKContratoReadOnlyRepository;
        }

        public ValidationResult Adicionar(CheckListContrato cKContrato)
        {
            throw new System.NotImplementedException();
        }

        public CheckListContrato BuscaPorId(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<CheckListContrato> BuscaTodos(int skip, int take)
        {
            return _cKContratoReadOnlyRepository.BuscaTodos();
        }

        public void Atualizar(CheckListContrato cKContrato)
        {
            throw new System.NotImplementedException();
        }

        public void Remover(CheckListContrato cKContrato)
        {
            throw new System.NotImplementedException();
        }
    }
}