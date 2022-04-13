using System;

namespace SPatrickBack.ModelRequire
{
    public class TransferRequire
    {
        public int? transferID{ get; set; }
        public int? productIDOrigin { get; set; }
        public int? productIDDestination { get; set; }
        public int transactionValue { get; set; }
        public DateTime? transactionDate { get; set; }
        public string concepto { get; set; }
        public string tipo { get; set; }
    }
}
