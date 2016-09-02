using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UnibenWeb.Domain.Interfaces.Validation;
using UnibenWeb.Domain.Validation.PagarContas;
using ValidationResult = UnibenWeb.Domain.ValueObjects.ValidationResult;

namespace UnibenWeb.Domain.Entities
{
    [Table("PagarContas")]
    public class PagarConta: ISelfValidator
    {
        public PagarConta()
        {
           // contaParcelas = new List<PagarContaParcela>();
        }

        [Key]
        public int PagarContaId{ get; set; }
        public int? FornecedorId { get; set; }
        //public int CentroCustoId { get; set; } // comercial, impostos, diretoria, cobrança, RH
        public int LocalPagamentoId { get; set; } // uniben parksul, uniben jdiniz
        public string Descricao { get; set; }
        public string Observacao { get; set; }
        public int TipoLancamento { get; set; } // Nota Fiscal, Conta Luz (fixa)
        public DateTime DataEmissao { get; set; }
        public int NumeroParcelas { get; set; }
        public decimal ValorTotal { get; set; }
        public bool Status { get; set; }

        // ======================================|
        // Atributo de Navegaçãcao Entre Objetos |
        // ======================================|
        public virtual Pessoa Fornecedor { get; set; }
        public virtual ICollection<CentroCusto> CentrosCusto { get; set; }
        public virtual ICollection<PagarContaParcela> ContaParcelas { get; set; }


        // ===========================================|
        // Validação Dos Dados do Objeto Dessa Classe |
        // ===========================================|
        public ValidationResult ResultadoValidacao { get; set; }
        public bool IsValid()
        {
            var fiscal = new ContaPagarFiscalizarRegras();
            ResultadoValidacao = fiscal.Validar(this);
            return ResultadoValidacao.IsValid;
        }
    }
}
