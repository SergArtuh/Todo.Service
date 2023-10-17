using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

using Todo.Service.MongoDB;
using Todo.Service.Model.User;
using Todo.Service.Model.Item;
using Todo.Service.Services;
using Todo.Service.Config;
using Todo.Service.Application;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

builder.Services.AddSecurity(builder.Configuration);
builder.Services.AddMongo(builder.Configuration)
    .AddMongoRepository<UserModel>("Users")
    .AddMongoRepository<ListModel>("Lists")
    .AddMongoRepository<ItemModel>("Items");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseAdminUser();
}

app.UseAuthentication();

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
