namespace EcommerceAPI.models
{
    public interface IAuthServise
    {
        Task<AuthModel> RegisterAsync(RegiserModel model); 
        Task<AuthModel> ReuestSigningAsync(RequestSigningmodel model);
        Task<string> AddRoleToUser(RoleModel model);
    }
}
