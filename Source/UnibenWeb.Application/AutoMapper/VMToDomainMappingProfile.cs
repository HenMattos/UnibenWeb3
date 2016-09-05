using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnibenWeb.Application.ViewModels;
using UnibenWeb.Domain.Entities;

namespace UnibenWeb.Application.AutoMapper
{
    public class VMToDomainMappingProfile: Profile
    {

        public override string ProfileName
        {
            get { return "DMToDomainMappings"; }
        }

        protected override void Configure()
        {
            CreateMap<PessoaVM, Pessoa>();
            CreateMap<EnderecoVM,Endereco>();
            CreateMap<PessoaEnderecoVM, Pessoa>();
            CreateMap<PessoaEnderecoVM, Endereco>();
            CreateMap<BancoVM, Banco>();
            CreateMap<EstadoCivilVM, EstadoCivil>();
            CreateMap<Pessoa,OperadoraVm > ();
            CreateMap<PagarConta, PagarContaVm>();
            CreateMap<CentroCusto, CentroCustoVm>();
        }

    }
}
