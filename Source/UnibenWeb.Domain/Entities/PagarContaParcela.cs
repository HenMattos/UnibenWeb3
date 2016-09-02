using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UnibenWeb.Domain.Interfaces.Validation;
using UnibenWeb.Domain.Validation.PagarContaParcelas;

namespace UnibenWeb.Domain.Entities
{
    [Table("PagarContaParcelas")]
    public class PagarContaParcela : ISelfValidator
    {
        public PagarContaParcela()
        {

        }

        [Key]
        public int PagarContaParcelaId { get; set; }
        public string Descricao { get; set; }
        public string Observacao { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime? DataPagamento { get; set; }
        public decimal ValorParcela { get; set; }
        public decimal Juros { get; set; }
        public decimal Desconto { get; set; }
        public bool Status { get; set; } // Pago?

        public int ContaOrigemId { get; set; }

        // ======================================|
        // Atributo de Navegaçãcao Entre Objetos |
        // ======================================|

        public virtual PagarConta ContaOrigem { get; set; }

        // ===========================================|
        // Validação Dos Dados do Objeto Dessa Classe |
        // ===========================================|
        public ValueObjects.ValidationResult ResultadoValidacao { get; set; }
        public bool IsValid()
        {
            var fiscal = new ContaPagarParcelaFiscalizarRegras();
            ResultadoValidacao = fiscal.Validar(this);
            return ResultadoValidacao.IsValid;
        }
    }
}
