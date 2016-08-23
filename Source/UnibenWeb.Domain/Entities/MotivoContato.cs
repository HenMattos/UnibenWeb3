﻿using System;
using System.ComponentModel.DataAnnotations;

namespace UnibenWeb.Domain.Entities
{
    public class MotivoContato
    {
        [Key]
        public int MotivoContatoId { get; set; }
        public string Descricao { get; set; }
        public string Obs { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
    }
}