using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Todo.Service.Services;
using Todo.Service.Model.User;
using System.Security.Claims;
using Microsoft.Extensions.ObjectPool;
using Todo.Service.Model.Item;
using Todo.Service.Interfaces;
using System.Runtime.Serialization;

namespace Todo.Service.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class ItemController : ControllerBase
{
    private IRepository<ItemModel> itemRepository;

    public ItemController(IRepository<ItemModel> itemRepository)
    {
        this.itemRepository = itemRepository;
    }


    [HttpGet]
    [Authorize]
    public IActionResult Get()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        if(identity == null) {
            return Unauthorized();
        }
        var userId = Guid.Parse(identity.Claims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value);
        var result = itemRepository.GetAll((item) => item.UserId == userId).Select(o => o.AsDto());
        return Ok(result);
    }


    [HttpPost]
    [Authorize]
    public IActionResult Create([FromBody] CreateItemDto item)
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        if(identity == null) {
            return Unauthorized();
        }

        var userId = identity.Claims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value;

        var itemModel = new ItemModel(){Id = Guid.NewGuid(), UserId = Guid.Parse(userId), Description = item.Description, isDone = item.isDone, DateCreated = DateTime.Now};
        itemRepository.Create(itemModel);
        return Ok(itemModel.AsDto());
    }

    [HttpPut]
    [Authorize]
    public IActionResult Edit([FromBody] UpdateItemDto updatedItem)
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        if(identity == null) {
            return Unauthorized();
        }
        var userId = Guid.Parse(identity.Claims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value);
        var editedItem = itemRepository.Get(item => item.UserId == userId && item.Id == updatedItem.Id);
        if(editedItem == null) {
            return BadRequest($"item with ID: {updatedItem.Id} not exist");
        }

        editedItem.Description = updatedItem.Description;
        editedItem.isDone = updatedItem.isDone;
        itemRepository.Update(editedItem);

        return Ok(editedItem.AsDto());
    }

    [HttpDelete("{id}")]
    [Authorize]
    public IActionResult Detete(Guid id)
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        if(identity == null) {
            return Unauthorized();
        }
        var userId = Guid.Parse(identity.Claims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value);
        var itemToDelete = itemRepository.Get(item => item.UserId == userId && item.Id == id);
        if(itemToDelete == null) {
             return BadRequest($"item with ID: {id} not exist");
        }
        itemRepository.Remove(id);
        return Ok();
    }
}
