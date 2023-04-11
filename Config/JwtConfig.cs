
namespace Todo.Service.Config;

public class JwtConfig 
{
    public string Secret {get; set;}
    public double ExpirationInMinutes {get; set;}
    public String Issuer {get; set;}
    public String Audience {get; set;}
}
