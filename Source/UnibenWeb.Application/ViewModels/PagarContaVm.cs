using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UnibenWeb.Application.ViewModels
{
    public class PagarContaVm
    {
        public PagarContaVm()
        {

        }

        [Key]
        public int PagarContaId { get; set; }
        public int? FornecedorId { get; set; } // FK - Pessoa Id
        public IEnumerable<int> CentroCustoId { get; set; } // FK - CentroCustoId -- comercial, impostos, diretoria, cobrança, RH
        public int LocalPagamentoId { get; set; } // FK - LocalPagamentoId -- uniben parksul, uniben jdiniz
        [DataType(DataType.MultilineText)]
        public string Descricao { get; set; }
        [DataType(DataType.MultilineText)]
        public string Observacao { get; set; }
        public int TipoLancamento { get; set; } // Nota Fiscal, Conta Luz (fixa)
        [DisplayName("Data de Emissao (Dia/Mes/Ano)")]
        [DataType(DataType.Date, ErrorMessage = "Insira uma data válida no formato dia/mes/ano")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DataEmissao { get; set; }
        public int NumeroParcelas { get; set; }
        [DataType(DataType.Currency)]
        public decimal ValorTotal { get; set; }
        public bool Status { get; set; }

    }
}
