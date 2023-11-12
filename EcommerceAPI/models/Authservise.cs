using EcommerceAPI.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OData.ModelBuilder;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EcommerceAPI.models
{
    public class Authservise : IAuthServise
    {
        private readonly UserManager<ApplicationUser> _UserManager;
        private readonly RoleManager<IdentityRole> _rolrManager;
        //private readonly Jwt _Jwt;
      
        public Authservise(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> rolrManager)
        {

            _rolrManager = rolrManager;
            _UserManager = userManager;
        }
        public async Task<AuthModel> RegisterAsync(RegiserModel model)
        {
            if (await _UserManager.FindByEmailAsync(model.Email) is not null)

                return new AuthModel { Message = "Email is already registered before" };

            if (await _UserManager.FindByNameAsync(model.UserName) is not null)

                return new AuthModel { Message = "UserName is already registered before" };
            var User = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };
            var Userresult = await _UserManager.CreateAsync(User, model.password);
            if (!Userresult.Succeeded)
            {
                var error = string.Empty;
                foreach (var item in Userresult.Errors)
                {
                    error += $"{item.Description}###";
                }
                return new AuthModel { Message = error };
            }
            await _UserManager.AddToRoleAsync(User, "User");
            var jwtsecuritytoken = await CreateJwtToken(User);
            return new AuthModel
            {
                Email = User.Email,
                ExpireDate = jwtsecuritytoken.ValidTo,
                IsAuthenticated = true,
                Roles = new List<string> { "User" },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtsecuritytoken),
                UserName = User.UserName
            };

        }
        public async Task<AuthModel> ReuestSigningAsync(RequestSigningmodel model)
        {
            var authModel = new AuthModel();
            var user=await _UserManager.FindByEmailAsync(model.Email);

            if (user == null||!await _UserManager.CheckPasswordAsync(user,model.Password)) 
            { authModel.Message = "Your Email or password is incorrect";
                return authModel;
            }
            var JwtToken = await CreateJwtToken(user);
            var rolelist = await _UserManager.GetRolesAsync(user);
            authModel.IsAuthenticated = true;
            authModel.Email = user.Email;
            authModel.UserName = user.UserName;
            authModel.Roles=rolelist.ToList();
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(JwtToken);
            authModel.ExpireDate=JwtToken.ValidTo;
            return authModel;   
        }
        public async Task<string> AddRoleToUser(RoleModel model) 
        {
            var user = await _UserManager.FindByIdAsync(model.UserId);
            if (user == null||!await _rolrManager.RoleExistsAsync(model.Role))
             return "the userId or role is incorrect"  ; 
            if(await _UserManager.IsInRoleAsync(user, model.Role))
            { return "the user is already in this role before"; }
            var result = await _UserManager.AddToRoleAsync(user, model.Role);
            if (result.Succeeded) 
             return string.Empty;
            return "something went wrong";   
        }

        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _UserManager.GetClaimsAsync(user);
            var roles=await _UserManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();
            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email,user.Email),
                    new Claim("uid",user.Id)
                }.Union(userClaims).Union(roleClaims);
                var symetricSecuritykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("S/9zBoHxckFjAxTuo4XIW6ZNGlZ2fYzFwcauQFd3zrA="));
                var _signingcredentials =new SigningCredentials(symetricSecuritykey,SecurityAlgorithms.HmacSha256);
                var jwtsecuritytoken = new JwtSecurityToken(
                    issuer: "SecureApi",
                    audience: "SecureApiUser",
                    claims: claims,
                    expires: DateTime.Now.AddDays(60),
                    signingCredentials: _signingcredentials
                    );
                return jwtsecuritytoken;


        }
            
    }
}
