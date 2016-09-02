using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnibenWeb.Domain.Interfaces.Validation;
using UnibenWeb.Domain.Validation.ContaContabeis;

namespace UnibenWeb.Domain.Entities
{
    [Table("ContaContabeis")]
    public class ContaContabil : ISelfValidator
    {
        public ContaContabil()
        {
                
        }
        [Key]
        public int ContaContabilId { get; set; }
        public int? ContaContabilPaiId { get; set; }
        public string Descricao { get; set; }

        // ======================================|
        // Atributo de Navegaçãcao Entre Objetos |
        // ======================================|

        public virtual ContaContabil ContaContabilPai { get; set; }

        // ===========================================|
        // Validação Dos Dados do Objeto Dessa Classe |
        // ===========================================|
        public ValueObjects.ValidationResult ResultadoValidacao { get; set; }

        public bool IsValid()
        {
            var fiscal = new ContaContabilFiscalizarRegras();
            ResultadoValidacao = fiscal.Validar(this);
            return ResultadoValidacao.IsValid;
        }
    }
}
