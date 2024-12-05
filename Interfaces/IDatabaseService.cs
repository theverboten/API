using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IDatabaseService
    {

        public TodoItem GetTodoItem(string id);

        public void UpdateTodoItemById(string id, TodoItem item);

        public List<TodoItem> GetFilteredTodoItems(string state);

        public void CreateJsonDb();

        public void InsertJsonData(TodoItem todoItem);

        public void DeleteJsonData(string id);
    }
}