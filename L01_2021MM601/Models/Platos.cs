﻿using System.ComponentModel.DataAnnotations;

namespace L01_2021MM601.Models
{
    public class Platos
    {
        [Key]
        public int platoId { get; set; }
        public string nombrePlato { get; set; }
        public decimal precio { get; set; }
    }
}
