
namespace Todo.Service.Model.User
{
    public static class Extensions
    {
        public static UserItem AsUserModel(this UserModel userModel)
        {
            return new UserItem() { Id = userModel.Id, Name = userModel.Name, Email = userModel.Email, Role = userModel.Role };
        }

        public static UserDto AsDto(this UserModel userModel)
        {
            return new UserDto(Id: userModel.Id, Name: userModel.Name, Email: userModel.Email, Role: userModel.Role, DateCreated: userModel.DateCreated);
        }

    }
}