using System.ComponentModel.DataAnnotations;

namespace SPatrickBack.Model
{
    public class UserContact
    {
        [Key]
        public int UserContactID { get; set; }
        public string idUser { get; set; }
        public int ContactProductId { get; set; }
        public string ContactName { get; set; }
        public string Image { get; set; }
    }
}
