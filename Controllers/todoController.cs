using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;

[ApiController]
[Route("api/[controller]")]
public class TodoController : ControllerBase
{
    private static List<TodoItem> Todos = new List<TodoItem>();

    [HttpGet]
    public ActionResult<List<TodoItem>> GetAll()
    {
        return Ok(Todos);
    }

    [HttpGet("{id}")]
    public ActionResult<TodoItem> Get(int id)
    {
        var todo = Todos.FirstOrDefault(t => t.Id == id);
        if (todo == null) return NotFound();
        return Ok(todo);
    }

    [HttpPost]
    public ActionResult<TodoItem> Create(TodoItem todo)
    {
        if (string.IsNullOrWhiteSpace(todo.Title))
        {
            return BadRequest(new { error = "Title is required" });
        }
        todo.Id = Todos.Count > 0 ? Todos.Max(t => t.Id) + 1 : 1;
        Todos.Add(todo);
        return CreatedAtAction(nameof(Get), new { id = todo.Id }, todo);
    }

   [HttpPut("{id}")]
public ActionResult<TodoItem> Update(int id, TodoItem updatedTodo)
{
    var todo = Todos.FirstOrDefault(t => t.Id == id);
    if (todo == null) return NotFound();

    todo.Title = updatedTodo.Title;
    todo.IsDone = updatedTodo.IsDone;

    return Ok(todo);
}


    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var todo = Todos.FirstOrDefault(t => t.Id == id);
        if (todo == null) return NotFound();

        Todos.Remove(todo);
        return NoContent();
    }
}
