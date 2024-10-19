using Microsoft.AspNetCore.Mvc;
using ToDoApi.Models;

[Route("api/[controller]")]
[ApiController]
public class ToDoItemsController : ControllerBase
{
    private static List<ToDoItem> ToDoItems = new List<ToDoItem>();

    [HttpGet]
    public IActionResult GetAll() => Ok(ToDoItems);

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var item = ToDoItems.FirstOrDefault(x => x.Id == id);
        if (item == null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    public IActionResult Create(ToDoItem item)
    {
        item.Id = ToDoItems.Count + 1;
        ToDoItems.Add(item);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, ToDoItem updatedItem)
    {
        var item = ToDoItems.FirstOrDefault(x => x.Id == id);
        if (item == null) return NotFound();
        item.Title = updatedItem.Title;
        item.IsCompleted = updatedItem.IsCompleted;
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var item = ToDoItems.FirstOrDefault(x => x.Id == id);
        if (item == null) return NotFound();
        ToDoItems.Remove(item);
        return NoContent();
    }
}
