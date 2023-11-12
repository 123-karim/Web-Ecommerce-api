using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.models
{
    public interface IDataHelperCart<Table>
    {
        List<Table> GetAll();
        List<Table> GetByuserId(int id);
        List<CartProduct> GetById(int Id);
       
        int AddCart([FromBody] List<CartProduct> cart, string email);
        int Update(int Id,Table table);
        int Delete(int Id);
    }
}
