using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

using Todo.Service.Model.User;
using Todo.Service.Interfaces;
using Microsoft.AspNetCore.Identity;




namespace Todo.Service.Services;

public class SecurityService 
{
    private JwtTokenService tokenAccesor;
    private IRepository<UserModel> usersRepository;

    public SecurityService( JwtTokenService tokenAccesor,  IRepository<UserModel> usersRepository) {
        this.tokenAccesor = tokenAccesor;
        this.usersRepository = usersRepository;
    }

    
    public UserItem? CreateUser(UserSignup userSignup) {
        if(FindUser(userSignup.Name) != null) {
            return null;
        }
        var newUser = new UserModel() { Id = Guid.NewGuid(), Name = userSignup.Name, Email = userSignup.Email, Password = userSignup.Password, Role = userSignup.Role, DateCreated = DateTime.Now };
        usersRepository.Create(newUser);
        return newUser.AsUserModel();
    }
    

    public UserItem? GetUser(UserLogin userLogin) {
        var userModel = FindUser(userLogin.Name);
        if(userModel != null && ValidateUser(userLogin, userModel)) {
            return userModel.AsUserModel();
        }
        return null;
    }

    public  String GenerateToken(UserItem user) {
         return tokenAccesor.Generate(user);
    }

    public List<UserModel> GetUsers() {
        return usersRepository.GetAll().ToList();
    }

    private UserModel? FindUser(String userName) {
         return usersRepository.Get(user => user.Name.ToLower() == userName.ToLower());
    }

    private bool ValidateUser(UserLogin userLogin, UserModel userModel) {
        return userLogin.Name == userModel.Name && userLogin.Password == userLogin.Password;
    }

}
