using Todo.Service.Interfaces;

namespace Todo.Service.Model.User;

public class UserModel : IEntity {

    public Guid Id {get; set;}
    public String Name {get; set;}

    public String Password {get; set;}

    public String Email {get; set;}

    public string Role { get; set; } 

    public DateTime DateCreated { get; set; } 
}