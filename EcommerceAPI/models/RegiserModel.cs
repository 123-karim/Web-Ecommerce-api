using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.models
{
    public class RegiserModel
    {
        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }
        [StringLength(100)]
        public string UserName { get; set; }
        [StringLength(150)]
        public string Email { get; set; }
        [StringLength(200)]
        public string password { get; set; }
    }
}
