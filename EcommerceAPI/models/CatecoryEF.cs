namespace EcommerceAPI.models
{
    public class CatecoryEF : IDataHelper<Category>
    { 
        private readonly dbContext db;
        public CatecoryEF(dbContext db)
        {
            this.db = db;

        }
        public int Add(Category table)
        {
            if (db.Database.CanConnect())
            {
                db.Category.Add(table);
                db.SaveChanges();
                return 1;
            }           
            return 0;
                
        }

        public int Delete(int Id)
        {
            if (db.Database.CanConnect()) {

                var de = db.Category.Find(Id);
                db.Category.Remove(de);
                db.SaveChanges();
                return 1;

            }           
            return 0;
           
        }

        public List<Category> GetAll()
        {
                return db.Category.ToList();  
        }

        public List<Category> GetById(int Id)
        {
            return db.Category.Where(s=>s.catId==Id).ToList();
        }

        public List<Category> GetByuserId(int id)
        {
            throw new NotImplementedException();
        }
        public int Update(int Id, Category table)
        {
            if (db.Database.CanConnect())
            {
                
                db.Category.Update(table);
                db.SaveChanges();
                return 1;
            }
            return 0;
        }
    }
}
