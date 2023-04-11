using Todo.Service.Interfaces;

namespace Todo.Service.Model.Item;

public class ItemModel : IEntity {

    public Guid Id {get; set;}
        public Guid UserId {get; set;}
    public String Description {get; set;}
    public bool isDone {get; set;}
    public DateTime DateCreated { get; set; } 
}