using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace SPatrickBack.Model
{
    public class Product
    {
        public int id_Product { get; set; }
        public int idTypeProd { get; set; }
        public Guid idUser { get; set; }
        public int saldoCupo { get; set; }
        public string cardNumber { get; set; }
        public DateTime startDate { get; set; }
        public DateTime finishDate { get; set; }
        public bool state { get; set; }
        public ICollection<ProducType> prodType { get; set; }


    }
}
