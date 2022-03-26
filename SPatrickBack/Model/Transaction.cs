using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SPatrickBack.Model
{
    public class Transaction
    {
        [Key]
        public int transactionID { get; set; }
        public int transactionTypeID { get; set; }
        public int productIDOrigin { get; set; }
        public Product product { get; set; }
        public int productIDDestination { get; set; }
        public int transactionValue { get; set; }
        public DateTime transactionDate { get; set; }
        public TransactionType transacType { get; set; }

    }
}
