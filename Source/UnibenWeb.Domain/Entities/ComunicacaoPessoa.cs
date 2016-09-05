using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnibenWeb.Domain.Entities
{
    [Table("ComunicacaoPessoas")]
    public class ComunicacaoPessoa
    {
        public ComunicacaoPessoa()
        {

        }
        [Key]
        public int ComunicacaoPessoaId { get; set; }
        public int PessoaId { get; set; }
        public string Descricacao { get; set; }
        public string Obs { get; set; }
        public DateTime DataRegistro { get; set; }
        public int UsuarioRegistroId { get; set; }
        public DateTime DataSolucao { get; set; }
        public int UsuarioSolucaoId { get; set; }

        // public virtual ComunicacaoSolucao ComunicacaoSolucao { get; set; }
        // public virtual MotivoContato MotivoContato { get; set; }


    }
}
