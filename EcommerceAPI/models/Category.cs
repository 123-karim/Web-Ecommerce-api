using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.models
{
    public class Category
    {
        [Key]
        public int catId { get; set; }
        [Required,MaxLength(100)]
        public string Name { get; set; }
       
        


    }
}
