using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web.Http.ModelBinding;
using ToyShopAPI.Classes;
using ToyShopAPI.Data;
using ToyShopAPI.Models;

namespace ToyShopAPI.Services
{
    public interface IUserService
    {
        AuthenticateResponseModel Authenticate(AuthenticateRequestModel model);
        IEnumerable<UserModel> GetAll();
        UserModel GetById(string id);
    }

    public class UserService : IUserService
    {
        private ApplicationDbContext _context;
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<UserModel> _users;
       // {
       //     new UserModel { Id = "1", FirstName = "Test", LastName = "User", Email = "test@mail.com", Password = "test" }
       // };
      //



        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }

        public AuthenticateResponseModel Authenticate(AuthenticateRequestModel model)
        {
            _users = _context.Users.ToList();

           




            var user = _users.SingleOrDefault(x => x.Email == model.Email && x.Password == model.Password);

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return new AuthenticateResponseModel(user, token);
        }

        public IEnumerable<UserModel> GetAll()
        {
            return _users;
        }

        public UserModel GetById(string id)
        {
            return _users.FirstOrDefault(x => x.Id == id);
        }

        // helper methods

        private string generateJwtToken(UserModel user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}



