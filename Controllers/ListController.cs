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
public class ListController : ControllerBase
{
    private IRepository<ListModel> listRepository;
    private IRepository<ItemModel> itemRepository;
    

    public ListController(IRepository<ListModel> listRepository, IRepository<ItemModel> itemRepository)
    {
        this.listRepository = listRepository;
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
        var result = listRepository.GetAll((item) => item.UserId == userId).Select(o => o.AsDto());
        return Ok(result);
    }

    [HttpPost]
    [Authorize]
    public IActionResult Create([FromBody] CreateListDto item)
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        if(identity == null) {
            return Unauthorized();
        }

        var userId = identity.Claims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value;

        var listModel = new ListModel(){Id = Guid.NewGuid(), UserId = Guid.Parse(userId), Name = item.Name, DateCreated = DateTime.Now};
        listRepository.Create(listModel);
        return Ok(listModel.AsDto());
    }

    [HttpPut]
    [Authorize]
    public IActionResult Edit([FromBody] UpdateListDto updatedItem)
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        if(identity == null) {
            return Unauthorized();
        }
        var userId = Guid.Parse(identity.Claims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value);
        var editedItem = listRepository.Get(item => item.UserId == userId && item.Id == updatedItem.Id);
        if(editedItem == null) {
            return BadRequest($"item with ID: {updatedItem.Id} not exist");
        }

        editedItem.Name = updatedItem.Name;
        listRepository.Update(editedItem);

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
        var listToDelete = listRepository.Get(item => item.UserId == userId && item.Id == id);

        if(listToDelete == null) {
             return BadRequest($"item with ID: {id} not exist");
        }
        
        listRepository.Remove(id);
        itemRepository.RemoveAll(item => item.ListId == listToDelete.Id);
        return Ok();
    }
}
