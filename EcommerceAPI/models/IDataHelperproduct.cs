using EcommerceAPI.dataTransever;

namespace EcommerceAPI.models
{
    public interface IDataHelperproduct<Table>
    {
        List<Table> GetAll();
        List<Table> GetByuserId(int id);
        List<Table> GetById(int Id);
       
        void Add(products table);
        int Update(int Id,products table);
        int Delete(int Id);
    }
}
