# Task Manager App
ITS203 Object-Oriented Design and Programming
Student: Sarun Bishwokarma
Student ID: S2500120
Institution: National Academy of Professional Studies (NAPS)

---

Project Description
This is a Task Manager application built using C# and Windows Forms.
It helps users keep track of their daily tasks by adding, editing,
deleting and completing tasks. Tasks are saved automatically so they
are not lost when the app is closed.

---

How to Run the App
1. Download or clone this repository from GitHub
2. Open the file TaskManagerApp.sln in Visual Studio 2022
3. Press Ctrl + Shift + B to build the project
4. Press F5 to run the app

---

Set of Instruction
- Microsoft Visual Studio 2022
- .NET 10.0 or higher
- Newtonsoft.Json (installs automatically)

---

App Features
- Add new tasks with title, description, due date and category
- Edit any existing task
- Delete tasks you no longer need
- Mark tasks as complete
- Search tasks by typing a keyword
- Filter tasks by category
- Create Urgent tasks with a priority level
- All tasks are saved to a JSON file automatically

---

OOP Principles Used in This App
- Inheritance: TaskItem and UrgentTask both inherit from BaseTask
- Polymorphism: GetDetails() method works differently for each task type
- Encapsulation: Data is protected using private fields and properties
- Abstraction: ITask interface and abstract BaseTask class hide complexity
- Exception Handling: Try-catch blocks prevent the app from crashing

---

References and Tools Used
- Microsoft Visual Studio 2022
- Newtonsoft.Json library for saving tasks to JSON file
- Microsoft Learn C# Documentation
- Claude AI used for learning support and debugging help
- GitHub for version control and code submission
