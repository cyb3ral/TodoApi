using System.Collections.Generic;
using TodoApi.Models;
using TodoApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoService _todoService;

        public TodoController(TodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        public ActionResult<List<Todo>> Get()
        {
            return _todoService.Get();
        }

        [HttpGet("{id:length(24)}", Name = "GetBook")]
        public ActionResult<Todo> Get(string Id)
        {
            var todo = _todoService.Get(Id);

            if (todo == null)
            {
                return NotFound();
            }


            return todo;
        }
        [HttpPost]
        public ActionResult<Todo> Create(Todo todo)
        {
            _todoService.Create(todo);
            return CreatedAtRoute("Getbook", new { id = todo.Id.ToString() }, todo);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Todo newTodo)
        {
            var todo = _todoService.Get(id);

            if (todo == null)
            {
                return NotFound();
            }

            _todoService.Update(id, newTodo);
            return NoContent();

        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var todo = _todoService.Get(id);

            if (todo == null)
            {
                return NotFound();
            }

            _todoService.Remove(id);
            return NoContent();
        }

    }
}


