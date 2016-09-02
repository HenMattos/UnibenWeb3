using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnibenWeb.Application.ViewModels
{
    public class ContaPagarComCentroCustoVm
    {
        [Key]
        public int PagarContaId { get; set; }
        public int? FornecedorId { get; set; } // FK - Pessoa Id
        public int CentroCustoId { get; set; } // FK - CentroCustoId -- comercial, impostos, diretoria, cobrança, RH
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

        public string CentrosCustoDescricao { get; set; }

        // Centro de Custo
        //public int CentroCustoId { get; set; }
        //public string CentroCustoDescricao { get; set; }
        //public string ObservacaoCentroCusto { get; set; }

        //public ICollection<CentroCustoVm> CentrosCusto { get; set; }

    }
}
