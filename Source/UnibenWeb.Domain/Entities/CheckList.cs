using System;

namespace UnibenWeb.Domain.Entities
{
    public class CheckList
    {
        public int CheckListId { get; set; }
        public string Descricao { get; set; }
        public string Obs { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataTermino { get; set; }
    }
}