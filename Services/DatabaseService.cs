using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Interfaces;
using Dapper;
using Microsoft.Data.Sqlite;

namespace API.Services
{
    public class DatabaseService : IDatabaseService
    {
        string path = "tasks.db";
        SqliteConnection? con;
        SqliteCommand? cmd;
        SqliteDataReader? dr;
        // var connectionString = app.Configuration.GetConnectionString("DefaultConnection"); cs = connection string


        public TodoItem GetTodoItem(string id)
        {
            var returnedItem = new TodoItem();
            //  var returnString = "";


            Console.WriteLine("Starting Json Database Show");
            var con = new SqliteConnection(@"Data Source=jsonTasks.db");
            con.Open();

            string stm = "SELECT * FROM tasks WHERE id = " + id;
            var cmd = new SqliteCommand(stm, con);
            dr = cmd.ExecuteReader();



            while (dr.Read())
            {
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    Console.WriteLine(dr[i] + ", ");


                }
                // returnString = dr[0].ToString();
                Console.WriteLine("**** ****");

                returnedItem.Id = dr.GetString(0);
                Console.WriteLine(returnedItem.Id);

                returnedItem.Title = dr.GetString(1);
                Console.WriteLine(returnedItem.Title);

                returnedItem.State = dr.GetInt32(2);
                Console.WriteLine(returnedItem.State);

                returnedItem.Content = dr.GetString(3);
                Console.WriteLine(returnedItem.Content);
                Console.WriteLine("**** ****");
                // Console.WriteLine("return string is: " + returnString);
            }

            return returnedItem;
        }
        //**** ****

        public void UpdateTodoItemById(string id, TodoItem item)
        {
            string titleString = "";
            string contentString = "";
            string stateString = "";

            if (item.Title != "")
            {
                titleString = "title = '" + item.Title + "',";
            }

            if (item.Content != "")
            {
                contentString = "content = '" + item.Content + "',";
            }

            if (item.State == 1 || item.State == 2 || item.State == 3)
            {
                stateString = "state = '" + item.State + "'";
            }


            Console.WriteLine("Starting Json Database Update");
            var con = new SqliteConnection(@"Data Source=jsonTasks.db");
            con.Open();

            string stm = $"UPDATE tasks SET {titleString} {contentString} {stateString} WHERE id = " + id;
            Console.WriteLine(stm);


            var cmd = new SqliteCommand(stm, con);
            dr = cmd.ExecuteReader();
            Console.WriteLine("Data update id: " + id + " is done");
        }
        //**** **** 
        /*
        public List<TodoItem> GetTodoItems()
        {
            var TodoItemList = new List<TodoItem>();
            var returnedItem = new TodoItem();



            // **** ****

            Console.WriteLine("Starting Json Database Show");
            var con = new SqliteConnection(@"Data Source=jsonTasks.db");
            con.Open();

            string stm = "SELECT * FROM tasks";
            var cmd = new SqliteCommand(stm, con);
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    Console.WriteLine(dr[i] + ", ");
                }
                Console.WriteLine("****Object ****");
                var whileItem = new TodoItem();

                whileItem.Id = dr.GetString(0);
                Console.WriteLine(returnedItem.Id);

                whileItem.Title = dr.GetString(1);
                Console.WriteLine(returnedItem.Title);

                whileItem.State = dr.GetInt32(2);
                Console.WriteLine(returnedItem.State);

                whileItem.Content = dr.GetString(3);
                Console.WriteLine(returnedItem.Content);
                Console.WriteLine("**** ****");

                TodoItemList.Add(whileItem);
            }
            Console.WriteLine("****List objektů ****");
            foreach (var todoItem in TodoItemList)
            {

                Console.WriteLine("TodoItem: {0},{1},{2},{3}", todoItem.Id, todoItem.Title, todoItem.State, todoItem.Content);
            }
            Console.WriteLine("**** ****");

            // **** ****




            return TodoItemList;
        }*/


        public List<TodoItem> GetFilteredTodoItems(string state = "")
        {
            var TodoItemList = new List<TodoItem>();
            var returnedItem = new TodoItem();

            //SELECT * FROM tasks WHERE state = " + state

            // **** ****

            Console.WriteLine("Starting Json Database Show");
            var con = new SqliteConnection(@"Data Source=jsonTasks.db");
            con.Open();

            if (state == "created")
            {
                string stm = "SELECT * FROM tasks WHERE state = 1";
                var cmd = new SqliteCommand(stm, con);
                dr = cmd.ExecuteReader();
            }
            else if (state == "finished")
            {
                string stm = "SELECT * FROM tasks WHERE state = 3";
                var cmd = new SqliteCommand(stm, con);
                dr = cmd.ExecuteReader();
            }
            else if (state == "all" || state == "")
            {
                string stm = "SELECT * FROM tasks";
                var cmd = new SqliteCommand(stm, con);
                dr = cmd.ExecuteReader();
            }
            /*
            Console.WriteLine("Starting Json Database Show");
            var con = new SqliteConnection(@"Data Source=jsonTasks.db");
            con.Open();*/



            while (dr.Read())
            {
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    Console.WriteLine(dr[i] + ", ");
                }
                Console.WriteLine("****Object ****");
                var whileItem = new TodoItem();

                whileItem.Id = dr.GetString(0);
                Console.WriteLine(returnedItem.Id);

                whileItem.Title = dr.GetString(1);
                Console.WriteLine(returnedItem.Title);

                whileItem.State = dr.GetInt32(2);
                Console.WriteLine(returnedItem.State);

                whileItem.Content = dr.GetString(3);
                Console.WriteLine(returnedItem.Content);
                Console.WriteLine("**** ****");

                TodoItemList.Add(whileItem);
            }
            Console.WriteLine("****List objektů ****");
            foreach (var todoItem in TodoItemList)
            {

                Console.WriteLine("TodoItem: {0},{1},{2},{3}", todoItem.Id, todoItem.Title, todoItem.State, todoItem.Content);
            }
            Console.WriteLine("**** ****");

            // **** ****




            return TodoItemList;

        }

        public void DeleteJsonData(string id)
        {
            Console.WriteLine("Starting Json Database Delete");
            var con = new SqliteConnection(@"Data Source=jsonTasks.db");
            con.Open();

            string stm = "DELETE FROM tasks WHERE id = " + id;
            var cmd = new SqliteCommand(stm, con);
            dr = cmd.ExecuteReader();
            Console.WriteLine("Data id: " + id + " is done");
        }

        public void CreateJsonDb()
        {
            Console.WriteLine("Starting Database Creation API");
            using (var sqlite = new SqliteConnection(@"Data Source=jsonTasks.db"))
            {
                sqlite.Open();
                string sql = "create table tasks(id varchar(200), title varchar(200), state int, content varchar(200) )";
                SqliteCommand command = new SqliteCommand(sql, sqlite);
                command.ExecuteNonQuery();
            }
            Console.WriteLine("JSon database is done");
        }
        /*
        public void InsertData(string id, string title)
        {
            var con = new SqliteConnection(@"Data Source=tasks.db");
            con.Open();
            var cmd = new SqliteCommand("INSERT INTO task(id, title) VALUES(@id, @title)", con);

            // cmd.CommandText = "INSERT INTO task(id, title) VALUES(@id, @title)";
            string ID = id;
            string TITLE = title;

            cmd.Parameters.AddWithValue("@id", ID);
            cmd.Parameters.AddWithValue("@title", TITLE);

            cmd.ExecuteNonQuery();
            Console.WriteLine("Data insertion is done");
        }*/

        public void InsertJsonData(TodoItem todoItem)
        {
            var generatedId = Guid.CreateVersion7();
            var con = new SqliteConnection(@"Data Source=jsonTasks.db");
            con.Open();
            var cmd = new SqliteCommand("INSERT INTO tasks(id, title, state, content) VALUES(@id, @title, @state, @content)", con);

            // cmd.CommandText = "INSERT INTO task(id, title) VALUES(@id, @title)";
            //  string? ID = todoItem.Id; původní s manuálním id
            string? ID = generatedId.ToString();
            string? TITLE = todoItem.Title;
            int STATE = todoItem.State;
            string? CONTENT = todoItem.Content;

            cmd.Parameters.AddWithValue("@id", ID);
            cmd.Parameters.AddWithValue("@title", TITLE);
            cmd.Parameters.AddWithValue("@state", STATE);
            cmd.Parameters.AddWithValue("@content", CONTENT);

            cmd.ExecuteNonQuery();
            Console.WriteLine($"Json insertion is done: {ID + ", " + TITLE + ", " + CONTENT}");
        }
    }
}