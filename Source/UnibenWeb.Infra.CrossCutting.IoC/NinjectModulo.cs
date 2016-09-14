using Ninject.Modules;
using UnibenWeb.Domain.Interfaces.Repository;
using UnibenWeb.Domain.Interfaces.Repository.ReadOnly;
using UnibenWeb.Domain.Interfaces.Services;
using UnibenWeb.Domain.Services;
using UnibenWeb.Infra.Data.Context;
using UnibenWeb.Infra.Data.Interfaces;
using UnibenWeb.Infra.Data.Repositories;
using UnibenWeb.Infra.Data.Repositories.ReadOnly;
using UnibenWeb.Infra.Data.UoW;

namespace UnibenWeb.Infra.CrossCutting.IoC
{
    public class NinjectModulo: NinjectModule
    {
        public override void Load()
        {
            // Domain Service
            Bind<IPessoaService>().To<PessoaService>();
            Bind<IBancoService>().To<BancoService>();
            Bind<IEstadoCivilService>().To<EstadoCivilService>();
            Bind<IEnderecoService>().To<EnderecoService>();
            Bind<ICKContratoService>().To<CheckListContratoService>();
            Bind<IBaseService>().To<BaseService>();
            Bind<IPagarContaService>().To<PagarContaService>();
            Bind<ICentroCustoService>().To<CentroCustoService>();

            // Infra.Data
            Bind(typeof(IBaseRepository<>)).To(typeof(BaseRepository<>));
            Bind<IPessoaRepository>().To<PessoaRepository>();
            Bind<IEnderecoRepository>().To<EnderecoRepository>();
            Bind<IBancoRepository>().To<BancoRepository>();
            Bind<IEstadoCivilRepository>().To<EstadoCivilRepository>();
            Bind<IPagarContaRepository>().To<PagarContaRepository>();
            Bind<ICentroCustoRepository>().To<CentroCustoRepository>();
            Bind<IObservacaoRepository>().To<ObservacaoRepository>();
            Bind<IContaContabilRepository>().To<ContaContabilRepository>();
            Bind<IPessoaTipoRepository>().To<PessoaTipoRepository>();
            Bind<IProcedimentoRepository>().To<ProcedimentoRepository>();
            Bind<IProdutoRepository>().To<ProdutoRepository>();
            Bind<IProdutoTermoRepository>().To<ProdutoTermoRepository>();
            Bind<IPropostaRepository>().To<PropostaRepository>();
            Bind<ITelContatoRepository>().To<TelContatoRepository>();
            Bind<ITipoContratacaoProdutoRepository>().To<TipoContratacaoProdutoRepository>();
            Bind<IUnidadeNegocioRepository>().To<UnidadeNegocioRepository>();

            // Infra.Data ReadOnly
            Bind<IPessoaReadOnlyRepository>().To<PessoaReadOnlyRepository>();
            Bind<IBancoReadOnlyRepository>().To<BancoReadOnlyRepository>();
            Bind<ICorreiosReadOnlyRepository>().To<CorreiosReadOnlyRepository>();
            Bind<ICKContratoReadOnlyRepository>().To<CKContratoReadOnlyRepository>();
            Bind<IEnderecoReadOnlyRepository>().To<EnderecoReadOnlyRepository>();
            Bind<IBaseReadOnlyRepository>().To<BaseReadOnlyRepository>();
            Bind<IProcedimentoRepository>().To<ProcedimentoRepository>();
            Bind<IProdutoRepository>().To<ProdutoRepository>();
            Bind<IProdutoTermoRepository>().To<ProdutoTermoRepository>();
            Bind<IPropostaRepository>().To<PropostaRepository>();
            Bind<ITelContatoRepository>().To<TelContatoRepository>();
            Bind<ITipoContratacaoProdutoRepository>().To<TipoContratacaoProdutoRepository>();
            Bind<IUnidadeNegocioRepository>().To<UnidadeNegocioRepository>();


            //Bind<IEstadoCivilReadOnlyRepository>().To<EstadoCivilReadOnlyRepository>(); -- a implementar

            // Data Config ()
            Bind<IContextManager>().To<ContextManager>();
            Bind<IUnitOfWork>().To<UnitOfWork>();
        }
    }
}