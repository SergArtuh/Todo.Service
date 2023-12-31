
namespace Todo.Service.Model.Item;

public static class Extensions
{
    public static ItemDto AsDto(this ItemModel itemModel)
    {
        return new ItemDto(Id: itemModel.Id, Description: itemModel.Description, isDone: itemModel.isDone, dateCreated: itemModel.DateCreated);
    }

    public static ListDto AsDto(this ListModel listModel)
    {
        return new ListDto(Id: listModel.Id, Name: listModel.Name, dateCreated: listModel.DateCreated);
    }
}