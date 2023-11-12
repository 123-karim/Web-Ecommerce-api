using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.dataTransever
{
    public class Cartproducts
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }
        public int Price { get; set; }

        public int Count { get; set; }
        public string TheCategory { get; set; }
        public byte[] Image { get; set; }
        public string Discription { get; set; }
        public int categoryId { get; set; }
        public int CartId { get; set; }


    }
}
