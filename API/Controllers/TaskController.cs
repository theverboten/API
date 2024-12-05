using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API;
using API.Interfaces;


namespace API.Controllers
{
    [ApiController]
    [Route("api/todo/")]
    public class TaskController : ControllerBase
    {
        private readonly IDatabaseService _databaseService;

        public TaskController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [HttpGet("get-task-list")]
        public ActionResult<List<TodoItem>> GetFilteredTodoList(string id = "")
        {
            var returnList = new List<TodoItem>();

            returnList = _databaseService.GetFilteredTodoItems(id);
            Console.WriteLine("Getting todo-list is done");
            return returnList;
        }



        [HttpGet("get-task/{id}")]
        public ActionResult<TodoItem> GetTaskById(string id)
        {
            string inputId = "'" + id + "'";
            var returnedItem = new TodoItem();


            returnedItem = _databaseService.GetTodoItem(inputId);
            Console.WriteLine("Read Json with id: " + inputId);
            Console.WriteLine("Content is: " + returnedItem.Content);
            return returnedItem;
        }

        [HttpDelete("delete-task/{id}")]
        public ActionResult<IStatusCodeHttpResult> DeleteTaskById(string id)
        {

            string inputId = "'" + id + "'";

            _databaseService.DeleteJsonData(inputId);
            Console.WriteLine("Delete Json with id: " + inputId);

            return Ok();
        }


        [HttpPost("post-task")]
        public ActionResult<IStatusCodeHttpResult> PostTask(TodoItem todoItem)
        {
            _databaseService.InsertJsonData(todoItem);
            Console.WriteLine("Json Post Controller is done");
            return Created();
        }

        [HttpPut("put-task/{id}")]

        public ActionResult<IStatusCodeHttpResult> PutTaskById(string id, TodoItem todoItem)
        {
            string inputId = "'" + id + "'";
            todoItem.Id = inputId;

            _databaseService.UpdateTodoItemById(inputId, todoItem);

            Console.WriteLine("Json put Controller is done ID: " + todoItem.Id);
            return Ok();
        }

        /*
        [HttpPost("create-json-database")]
        public ActionResult CreateJsonDatabase()
        {
            _databaseService.CreateJsonDb();
            Console.WriteLine("Json database is done");
            return Ok();
        }*/


    }
}