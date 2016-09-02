using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UnibenWeb.Domain.Interfaces.Validation;
using UnibenWeb.Domain.Validation.CentroCustos;
using UnibenWeb.Domain.ValueObjects;

namespace UnibenWeb.Domain.Entities
{
    [Table("CentroCustos")]
    public class CentroCusto : ISelfValidator
    {
        public CentroCusto()
        {

        }

        [Key]
        public int CentroCustoId { get; set; }
        public string CentroDeCusto { get; set; }
        public string Descricao { get; set; }
        public string Obs { get; set; }

        // ======================================|
        // Atributo de Navegaçãcao Entre Objetos |
        // ======================================|

        //

        // ===========================================|
        // Validação Dos Dados do Objeto Dessa Classe |
        // ===========================================|
        public ValueObjects.ValidationResult ResultadoValidacao { get; set; }

        public bool IsValid()
        {
            var fiscal = new CentroCustoFiscalizarRegras();
            ResultadoValidacao = fiscal.Validar(this);
            return ResultadoValidacao.IsValid;
        }
    }



}
