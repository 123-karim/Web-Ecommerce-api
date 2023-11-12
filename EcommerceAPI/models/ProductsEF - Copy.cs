using EcommerceAPI.dataTransever;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace EcommerceAPI.models
{
    public class ProductsEF : IDataHelperproduct<Product>
    {
        private readonly dbContext db;
        public ProductsEF(dbContext db)
        {
        

            this.db = db;

        }
        public void Add(products prod)
        {
            if (db.Database.CanConnect())
            {
                using var datastream = new MemoryStream();
                prod.Image.CopyTo(datastream);
                var pro = new Product()
                {
              
                    TheCategory = prod.Category,
                    Count = prod.Count,
                    Discription = prod.Discription,
                    Image = datastream.ToArray(),
                    Price = prod.Price,
                    categoryId=prod.categoryId,
                    Title=prod.Title,
                    
                };
                db.Product.Add(pro);
                db.SaveChanges();
               
            }
           
        }
        

        public int Delete(int Id)
        {
            if (db.Database.CanConnect())
            {
               var del= db.Product.Find(Id);
                db.Product.Remove(del);
                db.SaveChanges();
                return 1;
            }
            return 0;
        }

        public List<Product> GetAll()
        {

            return db.Product.ToList();
        }

        public List<Product> GetById(int Id)
        {
            return db.Product.Where(x=>x.Id == Id).ToList();
        }

        public List<Product> GetByuserId(int id)
        {
            throw new NotImplementedException();
        }

        public int Update(int Id, products prod)
        {
             
            if (db.Database.CanConnect())
            {
                using var datastream = new MemoryStream();
                prod.Image.CopyTo(datastream);
                var pro = db.Product.SingleOrDefault(c=>c.Id==Id);

                pro.TheCategory = prod.Category;
                pro.Count = prod.Count;
                pro.Discription = prod.Discription;
                pro.Image = datastream.ToArray();
                pro.Price = prod.Price;
                pro.categoryId = prod.categoryId;
                pro.Title = prod.Title;
                

                db.Product.Update(pro);

                db.SaveChanges();
                return 1;
            }
            return 0;
        }
        // you ned to passing product in toupdate with controler not products
        
    }
}
