using Microsoft.AspNetCore.Mvc;

namespace TodoAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoController : ControllerBase
{
    private readonly ILogger<TodoController> _logger;
    private readonly TodoService _todoService;

    public TodoController(
        ILogger<TodoController> logger,
        TodoService todoService)
    {
        _logger = logger;
        _todoService = todoService;
    }

    [HttpGet(Name = "GetTodos")]
    public IEnumerable<Todo> Get()
    {
        return _todoService.GetTodos();
    }

    [HttpPost(Name = "CreateTodos")]
    public async Task<ActionResult<Todo>> Post([FromBody] CreateTodoRequest request)
    {

        var createdTodo = await _todoService.CreateTodoRequest(request.Title, request.Description, request.DueDate, request.Priority);
        return Ok(createdTodo);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTodoRequest request)
    {
        var result = await _todoService.UpdateTodoAsync(id, request.Title, request.Description, request.DueDate, request.Priority, request.IsComplete);

        if (!result.Success)
            return NotFound(result);

        return Ok(result);
    }
}