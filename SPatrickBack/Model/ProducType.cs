using System.ComponentModel.DataAnnotations;

namespace SPatrickBack.Model
{
    public class ProducType
    {
        [Key]
        public int ProductTypeID { get; set; }
        [Required]
        public string nameProduct { get; set; }
    }
}
