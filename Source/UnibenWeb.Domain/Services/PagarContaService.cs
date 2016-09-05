using System;
using UnibenWeb.Domain.Entities;
using UnibenWeb.Domain.Interfaces.Repository;
using UnibenWeb.Domain.Interfaces.Services;
using UnibenWeb.Domain.Validation.PagarContas;
using UnibenWeb.Domain.ValueObjects;

namespace UnibenWeb.Domain.Services
{
    public class PagarContaService : IPagarContaService
    {

        private readonly IPagarContaRepository _pagarContaRepository;
        //private readonly IPagarContaReadOnlyRepository _pagarContaReadOnlyRepository;

        public PagarContaService(IPagarContaRepository pagarContaRepository) // , IPagarContaReadOnlyRepository pagarContaRepository
        {
            _pagarContaRepository = pagarContaRepository;
        }

        public ValidationResult Adicionar(PagarConta pagarConta)
        {
            var resultValidacao = new ValidationResult();
            if (!pagarConta.IsValid())
            {
                resultValidacao.AdicionarErro(pagarConta.ResultadoValidacao);
                return resultValidacao;
            }

            var fiscal = new RegraNegocioContasPagar(_pagarContaRepository);
            var validacaoService = fiscal.Validar(pagarConta);
            if (!validacaoService.IsValid)
            {
                resultValidacao.AdicionarErro(validacaoService);
                return resultValidacao;
            }

            //adicionar
            _pagarContaRepository.Add(pagarConta);



            return resultValidacao;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Excluir(PagarConta pagarConta)
        {
            _pagarContaRepository.Remove(pagarConta);
        }
    }
}
