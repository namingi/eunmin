using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[ApiController]

[Route("api/[controller]")]

public class TodoController
: ControllerBase
{

    private readonly AppDbContext _db;

    public TodoController(
    AppDbContext db
    )
    {
        _db = db;
    }

    [HttpGet]

    public async Task<IEnumerable<TodoItem>>
    Get()
    {

        return await _db.Todos
        .ToListAsync();

    }

    [HttpPost]

    public async Task<IActionResult>
    Create(
    TodoItem todo
    )
    {

        _db.Todos.Add(todo);

        await _db.SaveChangesAsync();

        return Ok(todo);

    }

    [HttpPut("{id}")]

    public async Task<IActionResult>
    Toggle(int id)
    {

        var todo =
        await _db.Todos.FindAsync(id);

        if (todo == null)
            return NotFound();

        todo.IsCompleted =
        !todo.IsCompleted;

        await _db.SaveChangesAsync();

        return Ok(todo);

    }

    [HttpDelete("{id}")]

    public async Task<IActionResult>
    Delete(int id)
    {

        var todo =
        await _db.Todos.FindAsync(id);

        if (todo == null)
            return NotFound();

        _db.Remove(todo);

        await _db.SaveChangesAsync();

        return Ok();

    }

}