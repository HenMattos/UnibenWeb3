using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnibenWeb.Domain.Entities
{
    [Table("Bancos")]
    public class Banco
    {
        public Banco()
        {
            Pessoas = new List<Pessoa>();

        }
        [Key]
        public int BancoId { get; set; }
        public string Nome { get; set; }
        public string Gerente { get; set; }
        public string Celular { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Obs { get; set; }

        public virtual ICollection<Pessoa> Pessoas { get; set; }
    }
}
