using System.ComponentModel.DataAnnotations;

namespace SPatrickBack.Model
{
    public class OperationLog
    {
        [Key]
        public int OperationId { get; set; }
        public int OperationProductID { get; set; }
        public int OperationValue { get; set; }
        public string OperationFunction { get; set; }
    }
}
