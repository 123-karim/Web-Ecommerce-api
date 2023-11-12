using EcommerceAPI.dataTransever;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace EcommerceAPI.models
{
    public class Cart
    {
        
        
        public int Id { get; set; }
        public DateTime Date { get; set; }= DateTime.Now;
        public int Quantity { get; set; }
        public int totalprice { get; set;}
        public string UserEmail { get; set;}
       // public List<CartProduct> cartproduct { get; set; }
        
       
    }
    
}
