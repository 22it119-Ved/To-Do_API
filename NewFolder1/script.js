// Base URL for the .NET API
const apiUrl = "https://localhost:5001/api/todoitems";

// Fetch all ToDo items and display them on the page
async function fetchToDos() {
    const response = await fetch(apiUrl);
    const todos = await response.json();

    const todoList = document.getElementById("todo-list");
    todoList.innerHTML = "";  // Clear the current list

    todos.forEach(todo => {
        const todoItem = document.createElement("li");
        todoItem.innerHTML = `
            ${todo.title} 
            <button onclick="deleteToDo(${todo.id})">Delete</button>
        `;
        todoList.appendChild(todoItem);
    });
}

// Add a new ToDo item
async function addToDo() {
    const newToDoInput = document.getElementById("new-todo");
    const newToDo = {
        title: newToDoInput.value,
        isCompleted: false
    };

    await fetch(apiUrl, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(newToDo)
    });

    newToDoInput.value = '';  // Clear the input field
    fetchToDos();  // Refresh the list
}

// Delete a ToDo item
async function deleteToDo(id) {
    await fetch(`${apiUrl}/${id}`, {
        method: "DELETE"
    });

    fetchToDos();  // Refresh the list
}

// Initialize the ToDo list when the page loads
fetchToDos();
