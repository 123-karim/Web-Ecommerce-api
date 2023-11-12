using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.dataTransever
{
    public class products
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }
       
        public int Price { get; set; }
        
        public int Count { get; set; }
        public string Category { get; set; }
        public IFormFile Image { get; set; }
        public string Discription { get; set; }
        public int categoryId { get; set; }
        
        
    }
}
