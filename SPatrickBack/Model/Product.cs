using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPatrickBack.Model
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        public string idUser { get; set; }
        public int saldoCupo { get; set; }
        public string cardNumber { get; set; }
        public DateTime startDate { get; set; }
        public DateTime finishDate { get; set; }
        public bool state { get; set; }
        public int ProductTypeID { get; set; }
        public ProducType ProductType { get; set; }
        public ICollection<Transaction> Transactions { get; set; }


    }
}
