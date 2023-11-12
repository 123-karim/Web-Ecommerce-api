using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace EcommerceAPI.models
{
    public class CartsEf : IDataHelperCart<Cart>
    {
        private readonly dbContext db;
        public CartsEf(dbContext db)
        {
            this.db = db;

        }
        public int AddCart([FromBody] List<CartProduct> Products,[FromHeader]string email)
        {

            if (db.Database.CanConnect())
            {
                Cart cart=new Cart();
                int quantity = 0;
                int totalprice = 0;
               

                foreach (var item in Products)
                {
                    var cartpro = new CartProduct()
                    {
                       // Id = item.Id,
                        TheCategory = item.TheCategory,
                        Count = item.Count,
                        Discription = item.Discription,
                        Image = item.Image,
                        Price = item.Price,
                        categoryId = item.categoryId,
                        Title = item.Title,
                        CartId = item.CartId,

                    };
                    quantity += item.Count;
                    totalprice += item.Count*item.Price;

                    db.cartProduct.Add(cartpro);
                   
                    db.SaveChanges();

                }
                //cart.Id = id;
                cart.Quantity = quantity;
                cart.totalprice = totalprice;
                cart.UserEmail= email;
                db.cart.Add(cart);
                db.SaveChanges();
                return 1;
            }
            return 0;
        }

        public int Delete(int Id)
        {
            if (db.Database.CanConnect())
            {

                var de = db.cart.Find(Id);
                db.cart.Remove(de);
                db.SaveChanges();
                return 1;

            }
            return 0;
        }

        public List<Cart> GetAll()
        {

            return db.cart.ToList();
        }

        public List<CartProduct> GetById(int Id)
        {
            List<CartProduct> products = db.cartProduct.Where(i=>i.CartId==Id).ToList();
            
          
          return products;

        }

        public List<Cart> GetByuserId(int id)
        {
            throw new NotImplementedException();
        }

        public int Update(int Id, Cart table)
        {
            if (db.Database.CanConnect())
            {
                db.cart.Update(table);
                db.SaveChanges();
                return 1;
            }
            return 0;
        }
    }
}
