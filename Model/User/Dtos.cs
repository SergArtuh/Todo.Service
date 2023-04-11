
namespace Todo.Service.Model.User;

public record UserLoginDto(String UserName, String UserPass);
public record UserSignupDto(String UserName, String UserPass, String Email);
public record UserDto(Guid Id, String Name, String Email, String Role, DateTime DateCreated);
