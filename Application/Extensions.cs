using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

using Todo.Service.Config;
using Todo.Service.Interfaces;
using Todo.Service.Model.User;

namespace Todo.Service.Application
{
    public static class Extensions
    {
        public static IApplicationBuilder UseAdminUser(this IApplicationBuilder app)
        {
            var usersRepository = app.ApplicationServices.GetService<IRepository<UserModel>>();
            if(null == usersRepository.Get(user => user.Name.ToLower() == "admin")) {
                var adminUser = new UserModel() { Id = Guid.NewGuid(), Name = "admin", Email = "admin@mail.com", Password = "admin", Role = "Administrator", DateCreated = DateTime.Now };
                usersRepository.Create(adminUser);
            }
            return app;
        }
    }
}