using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPatrickBack.Model
{
    public class ProductRequire
    {
        public int ProductTypeId { get; set; }
        public int saldoCupo { get; set; }
        public string? cardNumber { get; set; }
        public DateTime startDate { get; set; }
        public DateTime? finishDate { get; set; }
        public bool state { get; set; } = true;


    }
}
