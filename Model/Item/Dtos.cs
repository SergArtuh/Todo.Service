using System.ComponentModel.DataAnnotations;

namespace Todo.Service.Model.Item;

public record ListDto(Guid Id, String Name, DateTime dateCreated);
public record CreateListDto([Required] String Name);
public record UpdateListDto([Required] Guid Id, [Required] String Name);

public record ItemDto(Guid Id, String Description, bool isDone, DateTime dateCreated);
public record CreateItemDto([Required] Guid listId, [Required] String Description, bool isDone = false);
public record UpdateItemDto([Required] Guid Id, [Required] String Description, [Required] bool isDone);