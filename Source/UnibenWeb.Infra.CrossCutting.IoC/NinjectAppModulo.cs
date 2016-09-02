using Ninject.Modules;
using UnibenWeb.Application;
using UnibenWeb.Application.Interface;

namespace UnibenWeb.Infra.CrossCutting.IoC
{
    public class NinjectAppModulo: NinjectModule
    {
        public override void Load()
        {
            // Application
            Bind<IPessoaAppService>().To<PessoaAppService>();
            Bind<IBancoAppService>().To<BancoAppService>();
            Bind<IEstadoCivilAppService>().To<EstadoCivilAppService>();
            Bind<IEnderecoAppService>().To<EnderecoAppService>();
            Bind<ICKContratoAppService>().To<CKContratoAppService>();
            Bind<IBaseAppService>().To<BaseAppService>();
            Bind<IPagarContaAppService>().To<PagarContaAppService>();
            Bind<ICentroCustoAppService>().To<CentroCustoAppService>();
        }
    }
}