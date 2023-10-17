using Todo.Service.Interfaces;

namespace Todo.Service.Model.Item;

public class ListModel : IEntity {

    public Guid Id {get; set;}
    public Guid UserId {get; set;}
    public String Name {get; set;}
    public DateTime DateCreated { get; set; } 
}