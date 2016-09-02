using UnibenWeb.Domain.Entities;
using UnibenWeb.Domain.Interfaces.Repository;
using UnibenWeb.Domain.Specification;
using UnibenWeb.Domain.Specification.Pessoa;
using UnibenWeb.Domain.Validation.Base;

namespace UnibenWeb.Domain.Validation.Pessoas
{
    class PessoaAptaParaEntrarNoSistema: FiscalBase<Pessoa>
    {
        private readonly IPessoaRepository _pessoaRepository;

        public PessoaAptaParaEntrarNoSistema(IPessoaRepository pessoaRepository)
        {
            _pessoaRepository = pessoaRepository;
            var pessoaDuplicada = new PessoaJaCadastrada(_pessoaRepository);
            base.AdicionarRegra("PessoaDuplicada",
                new Regra<Pessoa>(pessoaDuplicada, "Já existe o mesmo CPF cadastrado no sistema!"));
        }
    }
}
