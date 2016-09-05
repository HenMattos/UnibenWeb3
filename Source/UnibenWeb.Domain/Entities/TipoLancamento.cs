using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UnibenWeb.Domain.Interfaces.Validation;
using UnibenWeb.Domain.Validation.TipoLancamentos;

namespace UnibenWeb.Domain.Entities
{
    [Table("TipoLancamentos")]
    public class TipoLancamento : ISelfValidator
    {
        public TipoLancamento()
        {
        }

        [Key]
        public int TipoLancamentoId { get; set; }
        public int Descricao { get; set; }

        // ======================================|
        // Atributo de Navegaçãcao Entre Objetos |
        // ======================================|



        // ===========================================|
        // Validação Dos Dados do Objeto Dessa Classe |
        // ===========================================|
        public ValueObjects.ValidationResult ResultadoValidacao { get; set; }

        public bool IsValid()
        {
            var fiscal = new TipoLancamentoFiscalizarRegras();
            ResultadoValidacao = fiscal.Validar(this);
            return ResultadoValidacao.IsValid;
        }


    }
}
