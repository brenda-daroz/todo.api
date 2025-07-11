using TodoAPI.Controllers;
using TodoAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using TodoAPI.AppDataContext;

public class TodoService
{
    private readonly TodoDbContext _context;

    public TodoService(TodoDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Todo> GetTodos()
    {
        return _context.Todos.ToList();
    }

    public async Task<ServiceResponse<Todo>> CreateTodoRequest(string title, string description, DateTime dueDate, int priority)
    {
        var response = new ServiceResponse<Todo>();
        try
        {
            var todo = new Todo
            {
                Id = Guid.NewGuid(),
                Title = title,
                Description = description,
                DueDate = dueDate,
                Priority = priority,
                IsComplete = false,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();

            response.Data = todo;
            response.Message = "Todo created successfully.";
        }

        catch (Exception ex)
        {
            response.Success = false;
            response.Message = "Failed to create todo.";
            response.Errors = new List<string> { ex.Message };
        }

        return response;
    }

    public async Task<ServiceResponse<Todo>> UpdateTodoAsync(Guid id, string title, string description, DateTime dueDate, int priority, bool isComplete)
    {
        var response = new ServiceResponse<Todo>();

        try
        {
            var todo = await _context.Todos.FindAsync(id);

            if (todo == null)
            {
                response.Success = false;
                response.Message = "Todo not found.";
                return response;
            }

            todo.Title = title;
            todo.Description = description;
            todo.DueDate = dueDate;
            todo.Priority = priority;
            todo.IsComplete = isComplete;
            todo.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            response.Data = todo;
            response.Message = "Todo updated successfully.";
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = "Failed to update todo.";
            response.Errors = new List<string> { ex.Message };
        }

        return response;
    }

}