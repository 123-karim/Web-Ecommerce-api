namespace EcommerceAPI.models
{
    public interface IDataHelper<Table>
    {
        List<Table> GetAll();
        List<Table> GetByuserId(int id);
        List<Table> GetById(int Id);
       
        int Add(Table table);
        int Update(int Id,Table table);
        int Delete(int Id);
    }
}
