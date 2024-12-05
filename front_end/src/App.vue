<template>
  <div class="container">
    <table class="table table-striped">
      <thead>
        <tr>
          <th>ID</th>
          <th>TITLE</th>
          <th>STATE</th>
          <th>CONTENT</th>
          <th>BUTTONS</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="todo in todos" :key="todo.id">
          <td>{{ todo.id }}</td>
          <td>{{ todo.title }}</td>
          <td>{{ todo.state }}</td>
          <td>{{ todo.content }}</td>
          <td>
            <select @change="changeTaskState(todo.id)" v-model="stringState">
              <option disabled value="">Change state of task</option>
              <option>open</option>
              <option>in progress</option>
              <option>finished</option>
            </select>
            <button
              type="button"
              id="button-hidden"
              @click="dummyDelete(todo.id)"
            >
              Delete
            </button>
          </td>
        </tr>
      </tbody>
    </table>
  </div>

  <div
    class="content"
    style="
      display: flex;
      flex-direction: row;
      align-items: center;
      justify-content: center;
      display: flex;
      justify-content: space-between;
      max-width: 800px;
      margin: 0 auto;

      padding: 10px 0;
    "
  >
    <p>Title</p>
    <hr />
    <input v-model="inputTitle" placeholder="write task title" />
    <hr />
    <p>Content</p>
    <p style="white-space: pre-line">{{ message }}</p>
    <textarea
      v-model="inputContent"
      placeholder="write task content"
    ></textarea>
    <p>State</p>

    <select v-model="inputState">
      <option disabled value="">Please select state of task</option>
      <option>open</option>
      <option>in progress</option>
      <option>finished</option>
    </select>
    <button type="button" id="add-task" @click="addTask">Add</button>
  </div>

  <div
    class="button-container"
    style="
      display: flex;
      flex-direction: row;
      align-items: center;
      justify-content: center;
      display: flex;
      justify-content: space-between;
      max-width: 800px;
      margin: 0 auto;

      padding: 10px 0;
    "
  >
    <button
      style="position: absolute; left: 44%; transform: translateX(-50%)"
      type="button"
      id="button-hid1"
      @click="deleteTaskById()"
      hidden
    >
      Confirm task deletion
    </button>
    <button
      style="position: absolute; left: 56%; transform: translateX(-50%)"
      type="button"
      id="button-hid2"
      @click="cancelDelete()"
      hidden
    >
      Cancel task deletion
    </button>
  </div>
</template>

<script>
import axios from "axios";

export default {
  name: "App",

  data() {
    return {
      todos: [],
      inputId: "1",
      inputTitle: "",
      inputState: "",
      inputContent: "",

      idForDelete: "",

      editedId: "",
      editedTitle: "",
      editedState: "",
      editedContent: "",

      stringState: "",
    };
  },

  methods: {
    async addTask() {
      let headers = {
        accept: " */*",
        "Content-Type": "application/json",
      };

      let intState;
      if (this.inputState === "open") {
        intState = 1;
      } else if (this.inputState === "in progress") {
        intState = 2;
      } else if (this.inputState === "finished") {
        intState = 3;
      }

      const task = JSON.stringify({
        id: this.inputId,
        title: this.inputTitle,
        state: intState,
        content: this.inputContent,
      });
      console.log(task);

      await axios
        .post("http://localhost:8080/api/todo/post-task", task, {
          headers,
        })
        .catch((error) => {
          this.errorMessage = error.message;
          console.error("There was an error!", error);
        });

      let listResponse = await axios.get(
        "http://localhost:8080/api/todo/get-task-list"
      );
      this.todos = listResponse.data;

      this.inputTitle = "";
      this.inputState = "";
      this.inputContent = "";
    },

    dummyDelete(id) {
      let baseId = "";
      console.log("Are you sure you want to delete Task Id: " + id);

      baseId = id;
      if (baseId != "") {
        let element1 = document.getElementById("button-hid1");
        element1.removeAttribute("hidden");
        let element2 = document.getElementById("button-hid2");
        element2.removeAttribute("hidden");
      }
      this.idForDelete = id;
    },

    cancelDelete() {
      let element1 = document.getElementById("button-hid1");
      element1.setAttribute("hidden", "hidden");
      let element2 = document.getElementById("button-hid2");
      element2.setAttribute("hidden", "hidden");

      console.log(
        "Deletion of task ID: " + this.idForDelete + " has been stopped"
      );
    },

    async deleteTaskById() {
      let headers = {
        accept: " */*",
      };

      let element1 = document.getElementById("button-hid1");
      element1.setAttribute("hidden", "hidden");
      let element2 = document.getElementById("button-hid2");
      element2.setAttribute("hidden", "hidden");

      await axios
        .delete(
          "http://localhost:8080/api/todo/delete-task/" + this.idForDelete,
          {
            headers,
          }
        )
        .catch((error) => {
          this.errorMessage = error.message;
          console.error("There was an error!", error);
        });

      console.log("Delete task: " + this.idForDelete);

      let listResponse = await axios.get(
        "http://localhost:8080/api/todo/get-task-list"
      );
      this.todos = listResponse.data;
    },

    /*
    dummyChangeTaskState(id) {
      let intState;
      if (this.stringState === "open") {
        intState = 1;
      } else if (this.stringState === "in progress") {
        intState = 2;
      } else if (this.stringState === "finished") {
        intState = 3;
      }

      console.log("Task state is: " + intState + "; ID: " + id);
    },*/

    async changeTaskState(id) {
      let headers = {
        accept: " */*",
        "Content-Type": "application/json",
      };

      let intState;
      if (this.stringState === "open") {
        intState = 1;
      } else if (this.stringState === "in progress") {
        intState = 2;
      } else if (this.stringState === "finished") {
        intState = 3;
      }

      const task = JSON.stringify({
        id: "",
        title: "",
        state: intState,
        content: "",
      });

      await axios
        .put("http://localhost:8080/api/todo/put-task/" + id, task, {
          headers,
        })
        .catch((error) => {
          this.errorMessage = error.message;
          console.error("There was an error!", error);
        });

      let listResponse = await axios.get(
        "http://localhost:8080/api/todo/get-task-list"
      );
      this.todos = listResponse.data;
      this.stringState = "";
    },
  },

  async mounted() {
    let listResponse = await axios.get(
      "http://localhost:8080/api/todo/get-task-list"
    );
    this.todos = listResponse.data;
  },
};
</script>
