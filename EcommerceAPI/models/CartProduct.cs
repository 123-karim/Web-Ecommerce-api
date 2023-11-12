using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceAPI.models
{
    public class CartProduct
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
       // public Cart Cart { get; set; }
        





    }
}
