﻿using System.ComponentModel.DataAnnotations;

namespace SPatrickBack.Model
{
    public class TransactionType
    {
        [Key]
        public int transactionTypeID { get; set; }
        public string nameTransaction { get; set; }
    }
}
