using System;
using System.Collections.Generic;

namespace SPatrickBack.Model
{
    public class Transaction
    {
        public int idTransaction { get; set; }
        public int idTypeTrans { get; set; }
        public int idProductoOrigin { get; set; }
        public int idProductoDestination { get; set; }
        public int transactionValue { get; set; }
        public DateTime transactionDate { get; set; }
        public ICollection<TransactionType> transacType { get; set; }

    }
}
