using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UnibenWeb.Application.ViewModels
{
    public class CheckListContratoVM
    {
        [Key]
        public int CheckListContratoId { get; set; }
        public string CheckItem { get; set; }
        //public bool Checked { get; set; }
    }
}