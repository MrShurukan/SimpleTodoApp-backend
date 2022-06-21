using System.ComponentModel.DataAnnotations;
using System.Web.Http.Cors;
using Microsoft.AspNetCore.Mvc;
using SimpleTodoApp.Contracts.Requests;
using SimpleTodoApp.Models;
using SimpleTodoApp.Repositories;

namespace SimpleTodoApp.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoController : Controller
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ITodoItemRepository _todoItemRepository;

    public TodoController(ICategoryRepository categoryRepository, ITodoItemRepository todoItemRepository)
    {
        _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        _todoItemRepository = todoItemRepository ?? throw new ArgumentNullException(nameof(todoItemRepository));
    }
    
    #region Todo Items
    
    [HttpGet("TodoList")]
    public IActionResult GetTodoList()
    {
        var categories = _categoryRepository.GetList().OrderBy(c => c.Id);
        return Ok(categories);
    }

    [HttpPost("TodoItem")]
    public IActionResult PostTodoItem(PostTodoItemRequest request)
    {
        var category = _categoryRepository.Get(category => category.Id == request.CategoryId);
        if (category is null)
            return NotFound($"Category: {request.CategoryId}");

        var todoItem = new TodoItem
        {
            Category = category,
            Text = request.Text,
            IsChecked = false
        };

        _todoItemRepository.Add(todoItem);
        return Created("todoItem", todoItem.Id);
    }
    
    [HttpDelete("TodoItem")]
    public IActionResult DeleteTodoItem([Required] int todoItemId)
    {
        var todoItem = _todoItemRepository.Get(todo => todo.Id == todoItemId);
        if (todoItem is null)
            return NotFound(todoItemId);

        _todoItemRepository.Remove(todoItem);
        return NoContent();
    }
    
    [HttpPut("TodoItem")]
    public IActionResult PutTodoItem(PutTodoItemRequest request)
    {
        var todoItem = _todoItemRepository.Get(todo => todo.Id == request.Id);
        if (todoItem is null)
            return NotFound(request.Id);

        todoItem.Text = request.Text;
        _todoItemRepository.SaveChanges();
        
        return Created("todoItem", todoItem.Id);
    }
    
    [HttpPut("MarkTodoItem")]
    public IActionResult PutMarkTodoItem([Required] int todoItemId)
    {
        var todoItem = _todoItemRepository.Get(todo => todo.Id == todoItemId);
        if (todoItem is null)
            return NotFound(todoItemId);

        _todoItemRepository.MarkTodoItem(todoItem);
        return NoContent();
    }
    
    #endregion

    #region Category

    [HttpPost("Category")]
    public IActionResult PostCategory(PostCategoryRequest request)
    {
        var category = new Category
        {
            Name = request.Name
        };

        _categoryRepository.Add(category);
        return Created("category", category.Id);
    }
    
    [HttpDelete("Category")]
    public IActionResult DeleteCategory([Required] int categoryId)
    {
        var category = _categoryRepository.Get(category => category.Id == categoryId);
        if (category is null)
            return NotFound(categoryId);

        _categoryRepository.Remove(category);
        return NoContent();
    }
    
    [HttpPut("Category")]
    public IActionResult PutCategory(PutCategoryRequest request)
    {
        var category = _categoryRepository.Get(category => category.Id == request.Id);
        if (category is null)
            return NotFound(request.Id);

        category.Name = request.Name;
        _categoryRepository.SaveChanges();
        
        return NoContent();
    }
    
    #endregion
}