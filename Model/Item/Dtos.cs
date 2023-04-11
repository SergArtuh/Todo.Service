using System.ComponentModel.DataAnnotations;

namespace Todo.Service.Model.Item;


public record ItemDto(Guid Id, String Description, bool isDone, DateTime dateCreated);
public record CreateItemDto([Required] String Description, bool isDone = false);
public record UpdateItemDto([Required] Guid Id, [Required] String Description, [Required] bool isDone);