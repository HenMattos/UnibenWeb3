using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using UnibenWeb.Domain.Entities;

namespace UnibenWeb.Infra.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<UnibenWeb.Infra.Data.Context.UnibenContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(UnibenWeb.Infra.Data.Context.UnibenContext context)
        {
            context.EstadoCivis.AddOrUpdate(p => p.EstadoCivilId
                , new EstadoCivil() { Descricao = "Solteiro(a)", Obs = "Estado Civil Solteiro(a) da Pessoa" }
                , new EstadoCivil() { Descricao = "Casado(a)", Obs = "Estado Civil Casado(a) da Pessoa" });

            context.Bancos.AddOrUpdate(b => b.BancoId
                , new Banco() { Nome = "Banco do Brasil" }
                , new Banco() { Nome = "Santander" }
                );

            context.PessoaSexos.AddOrUpdate(b => b.PessoaSexoId
            , new PessoaSexo() { Descricao = "Masculino" }
            , new PessoaSexo() { Descricao = "Feminino" }
            );

            context.PessoaTipos.AddOrUpdate(x => x.PessoaTipoId
                , new PessoaTipo() { Descricao = "Pessoa Jurídica", Obs = "Pessoa Jurídica Geral" }
                , new PessoaTipo() { Descricao = "Operadora", Obs = "Operadoras que disponibilizam os serviços" }
                , new PessoaTipo() { Descricao = "Empresa", Obs = "Empresa" }
                , new PessoaTipo() { Descricao = "Instituição", Obs = "Instituição" }
                , new PessoaTipo() { Descricao = "Associação", Obs = "Associação" }
                , new PessoaTipo() { Descricao = "PessoaFísica", Obs = "PessoaFísica" }
                , new PessoaTipo() { Descricao = "Beneficiário", Obs = "Beneficiário" }
                , new PessoaTipo() { Descricao = "Titular", Obs = "Titular" }
                , new PessoaTipo() { Descricao = "BeneficiárioTemp", Obs = "Beneficiários com cadastros incompletos (campos não obrigatórios)" }
                , new PessoaTipo() { Descricao = "Instituição", Obs = "Instituição" }
                , new PessoaTipo() { Descricao = "Fornecedor", Obs = "Fornecedores da Uniben" }
                );

            context.SaveChanges();

            var objBanco = context.Bancos.Find(1);
            var objPessoaTipo = context.PessoaTipos.Find(11);

            var objPessoa = new Pessoa()
            {
                Agencia = "32",
                Ativo = true,
                BancoId = 1,
                Banco = objBanco,
                CartaoSUS = "14",
                ContaCorrente = "1546789",
                CPF_CNPJ = "14155096000101",
                DataNascimento = new DateTime(1957, 05, 30),
                Enderecos = new List<Endereco>() { },
                PessoaTipoId = 11,
                PessoaTipo = objPessoaTipo,
                Nome = "Fornecedor Teste"
            };
            context.Pessoas.AddOrUpdate(x => x.PessoaId, objPessoa);

            context.SegmentoAssistenciais.AddOrUpdate(x => x.SegmentoAssistencialId
                , new SegmentoAssistencial() { Descricao = "Ambulatorial/Coparticipação/Regional com limitador em reais por procedimento.", Obs = "" }
                , new SegmentoAssistencial() { Descricao = "Completo/Apartamento/Coparticipativo/Regional", Obs = "" }
                , new SegmentoAssistencial() { Descricao = "Completo/Coparticipação/Enfermaria/Regional/ com limitador", Obs = "" }
                , new SegmentoAssistencial() { Descricao = "Completo/Apartamento/Sem coparticipação/Regional.", Obs = "" }
                , new SegmentoAssistencial() { Descricao = "Completo/Apartamento/Nacional/Sem Coparticipação", Obs = "" }
                , new SegmentoAssistencial() { Descricao = "Completo/Apartamento/Com coparticipação/Nacional", Obs = "" }
                , new SegmentoAssistencial() { Descricao = "Completo/Apartamento/Regional/Maior coparticipação/Com limitador", Obs = "" }
                , new SegmentoAssistencial() { Descricao = "Odontológica", Obs = "" }
                , new SegmentoAssistencial() { Descricao = "Completo/Enfermaria/Coparticipativo/Local", Obs = "" }
                , new SegmentoAssistencial() { Descricao = "Completo/Enfermaria/Coparticipativo/Estadual", Obs = "" }
                , new SegmentoAssistencial() { Descricao = "Completo/Apartamento/Coparticipativo/Estadual", Obs = "" }
                , new SegmentoAssistencial() { Descricao = "Completo/Enfermaria/Sem coparticipação/Estadual", Obs = "" }
                , new SegmentoAssistencial() { Descricao = "Completo/Apartamento/Sem Coparticipação/Estadual", Obs = "" }
                , new SegmentoAssistencial() { Descricao = "Ambulatorial/Coparticipativo/Nacional", Obs = "" }
                , new SegmentoAssistencial() { Descricao = "Hospitalar/Coparticipativo/Regional", Obs = "" }
                , new SegmentoAssistencial() { Descricao = "Completo/Enfermaria/Sem coparticipação/Regional", Obs = "" }
                , new SegmentoAssistencial() { Descricao = "Completo/Apartamento/Coparticipativo/Local", Obs = "" }
                , new SegmentoAssistencial() { Descricao = "Completo/Apartamento/Sem Coparticipação/Local", Obs = "" }
                , new SegmentoAssistencial() { Descricao = "Completo/Enfermaria/Sem Coparticipação/Local", Obs = "" }
                , new SegmentoAssistencial() { Descricao = "Completo/Apartamento/sem coparticipação/Nacional", Obs = "" }
                , new SegmentoAssistencial() { Descricao = "Ambulatorial/Coparticipativo/Estadual", Obs = "" });

            context.TipoContratacaoProdutos.AddOrUpdate(x => x.TipoContratacaoProdutoId
                            , new TipoContratacaoProduto() { Descricao = "Adesão", Obs = "" }
                            , new TipoContratacaoProduto() { Descricao = "Empresarial", Obs = "" });

            context.AbrangenciaPlanos.AddOrUpdate(x => x.AbrangenciaPlanoId
                , new AbrangenciaPlano() { Descricao = "Estadual", Obs = "" }
                , new AbrangenciaPlano() { Descricao = "Nacional", Obs = "" });

            context.FatorModeradores.AddOrUpdate(x => x.FatorModeradorId
    , new FatorModerador() { Descricao = "Com Coparticipação", Obs = "" }
    , new FatorModerador() { Descricao = "Sem Coparticipação", Obs = "" });

            context.AcomodacaoTipos.AddOrUpdate(x => x.AcomodacaoTipoId
            , new AcomodacaoTipo() { Descricao = "Apartamento", Obs = "" }
            , new AcomodacaoTipo() { Descricao = "Enfermaria", Obs = "" });

            context.CentroCustos.AddOrUpdate(x => x.CentroCustoId
                , new CentroCusto() { Descricao = "Assessoria Contabilidade", Obs = "" }
                , new CentroCusto() { Descricao = "Assessoria Recursos Humanos", Obs = "" }
                , new CentroCusto() { Descricao = "Assessoria Tecnologia da Informação", Obs = "" }
                , new CentroCusto() { Descricao = "Assessoria Jurídica", Obs = "" }
                , new CentroCusto() { Descricao = "Benefícios", Obs = "" }
                , new CentroCusto() { Descricao = "Cobrança", Obs = "" }
                , new CentroCusto() { Descricao = "Cadastro", Obs = "" }
                , new CentroCusto() { Descricao = "Comercial", Obs = "" }
                , new CentroCusto() { Descricao = "Diretoria", Obs = "" }
                , new CentroCusto() { Descricao = "Financeiro", Obs = "" }
                , new CentroCusto() { Descricao = "Guia de Recolhimento - GRRF", Obs = "" }
                , new CentroCusto() { Descricao = "Impostos", Obs = "" }
                , new CentroCusto() { Descricao = "Informática", Obs = "" }
                , new CentroCusto() { Descricao = "Manutenção / Limpeza Matriz e Filial", Obs = "" }
                , new CentroCusto() { Descricao = "Marketing", Obs = "" }
                , new CentroCusto() { Descricao = "Patrocínio", Obs = "" }
                , new CentroCusto() { Descricao = "Recibos", Obs = "" }
                , new CentroCusto() { Descricao = "Recursos Humanos", Obs = "" }
                , new CentroCusto() { Descricao = "Uniben Filial", Obs = "" }
                , new CentroCusto() { Descricao = "Uniben Matriz", Obs = "" }
                , new CentroCusto() { Descricao = "Unimed Cataguases", Obs = "" }
                , new CentroCusto() { Descricao = "Uniben Juiz de Fora", Obs = "" }
                , new CentroCusto() { Descricao = "Uniodonto Cataguases", Obs = "" }
                , new CentroCusto() { Descricao = "Uniodonto Juiz de Fora", Obs = "" }
                );

            var obj1 = new ContaContabil() { Descricao = "Pessoa", ContaContabilPaiId = null };
            context.ContaContabeis.AddOrUpdate(x => x.ContaContabilId, obj1);
            context.ContaContabeis.AddOrUpdate(x => x.ContaContabilId, new ContaContabil() { Descricao = "Pessoa Salário", ContaContabilPai = obj1 });
            context.ContaContabeis.AddOrUpdate(x => x.ContaContabilId, new ContaContabil() { Descricao = "Pessoa Transporte", ContaContabilPai = obj1 });

        }
    }
}
