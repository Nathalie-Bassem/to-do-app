

using System;
using System.Collections.Generic;



namespace ToDoApp
{
    class Program
    {
        static Dictionary<string, User> users = new Dictionary<string, User>();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Welcome to To-Do List App");
                Console.WriteLine("1. Existing User");
                Console.WriteLine("2. New User");
                Console.WriteLine("Choose an option: ");
                string option = Console.ReadLine();

                if (option == "1")
                {
                    Login();
                }
                else if (option == "2")
                {
                    Register();
                }
                else
                {
                    Console.WriteLine("Invalid option, try again.");
                    Console.ReadKey();
                }
            }
        }

        static void Register()
        {
            Console.Clear();
            Console.WriteLine("Register a new user");

            Console.WriteLine("Enter a username: ");
            string username = Console.ReadLine();

            if (users.ContainsKey(username))
            {
                Console.WriteLine("Username already exists");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Enter a password (8 characters minimum): ");
            string password = GetPassword();
            
            string userState = GetUserState();

            users[username] = new User(username, password, userState);
            Console.WriteLine("User registered successfully. Return to the Main Menu to Login");
            Console.ReadKey();
        }
            
            
      static string GetPassword()
        {
            while (true)
            {
                string password = Console.ReadLine();


            if (password.Length < 8)
            {
                Console.WriteLine("Password is too short, try again");
            }
            else{
             bool hasUpperCase = false;
             bool hasLowerCase = false;
             bool hasDigit = false;
             bool hasSpecialChar = false;

            foreach (char c in password)
            {
                if (char.IsUpper(c)) hasUpperCase = true;
                if (char.IsLower(c)) hasLowerCase = true;
                if (char.IsDigit(c)) hasDigit = true;
                if (!char.IsLetterOrDigit(c)) hasSpecialChar = true;
            }

            if (hasUpperCase && hasLowerCase && hasDigit && hasSpecialChar)
            {
                return password;
            }
            else
            {
                Console.WriteLine("Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.");
            }
           }
          }
        }
             
          static string GetUserState()
          {
            Console.Clear();
            Console.WriteLine("Choose Free or Premium");
            Console.WriteLine("1. Free User");
            Console.WriteLine("2. Premium User");
            string option = Console.ReadLine();

            if (option == "1")
            {
                return "Free";
            }
            else if (option == "2")
            {
                Console.WriteLine("Please enter your credit card information:");
                string creditCardInfo = Console.ReadLine();

                return "Premium";
            }
            else
            {
                Console.WriteLine("Invalid option, try again.");
                Console.ReadKey();
                return GetUserState();
            }
        }
   
        static void Login()
        {
            Console.Clear();
            Console.WriteLine("User Login");

            Console.WriteLine("Enter username: ");
            string username = Console.ReadLine();

            Console.WriteLine("Enter password: ");
            string password = Console.ReadLine();

            if (users.ContainsKey(username) && users[username].Password == password)
            {
                UserDashboard(users[username]);
            }
            else
            {
                Console.WriteLine("Invalid username or password, try again");
                Console.ReadKey();
            }
        }

      static void UserDashboard(User user)
     {
       while (true)
       {
        Console.Clear();
        Console.WriteLine($"Welcome, {user.Username}");
        Console.WriteLine("Your To-Do List:");
        
        List<Todo> sortedTodos = InsertionSort(user.Todos);
        
        foreach (Todo todo in sortedTodos)
        {
            var status = todo.IsCompleted ;
            if(todo.IsCompleted==true){
                Console.WriteLine($"{todo.Id}. {status} {todo.Description}");
            }
        }
            
        

        Console.WriteLine("Options:");
        Console.WriteLine("1. Add To-Do");
        Console.WriteLine("2. Mark To-Do as Done");
        if (user.UserState == "Premium") {
                    Console.WriteLine("3. Access To-Do Lists of Other Users");
        }

        Console.WriteLine("Choose an option: ");
        string option = Console.ReadLine();

        if (option == "1")
        {
            AddTodo(user);
        }
        else if (option == "2")
        {
            MarkTodoAsDone(user);
        }
        else if (option == "3" && user.UserState == "Premium")
                {
                    AccessOtherUserTodos();
                }
        
        else
        {
            Console.WriteLine("Invalid option, try again.");
            Console.ReadKey();
        }
    }
   
  }

       static void AddTodo(User user)
        {
            if (user.UserState == "Free" && user.Todos.Count >= 3)
            {
                Console.WriteLine("Free users can only create up to 3 To-Do lists.");
                Console.ReadKey();
                return;
            }
            Console.Clear();
            Console.Write("Enter a description for the To-Do: ");
            string description = Console.ReadLine();
        
            

            user.Todos.Add(new Todo(description)
            {
                Id = user.Todos.Count + 1,
                DateAdded = DateTime.Now
                
                
            }) ;

            Console.WriteLine("To-Do added successfully.");
            Console.ReadKey();
        }

        static void MarkTodoAsDone(User user)
        {
            Console.Clear();
            Console.Write("Enter the ID of the To-Do: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                Todo todo = null;
        foreach (Todo t in user.Todos)
        {
            if (t.Id == id)
            {
                todo = t;
                break;
            
            }
        }
                
                if (todo != null)
                {
                    todo.IsCompleted = true;
                    Console.WriteLine("To-Do marked as done.");
                }
                else
                {
                    Console.WriteLine("Invalid ID, try again.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input, try again");
            }
            Console.ReadKey();
        
    }
    
     static void AccessOtherUserTodos()
        {
            Console.Clear();
            Console.WriteLine("Access To-Do Lists of Other Users");

            Console.WriteLine("Enter the username of the user you want to view: ");
            string username = Console.ReadLine();

            if (users.ContainsKey(username))
            {
                User otherUser = users[username];
                Console.WriteLine($"To-Do List of {otherUser.Username}:");

                foreach (Todo todo in otherUser.Todos)
                {
                    
                    Console.WriteLine($"{todo.Id}. {todo.Description}");
                }
            }
            else
            {
                Console.WriteLine("User not found.");
            }
            Console.ReadKey();
        }
    
         
      static List<Todo> InsertionSort(List<Todo> todos)
    {
    for (int i = 1; i < todos.Count; i++)
    {
        Todo current = todos[i];
        int j = i - 1;
        while (j >= 0 && todos[j].DateAdded < current.DateAdded)
        {
            todos[j + 1] = todos[j];
            j--;
        }
        todos[j + 1] = current;
    }
    return todos;
   }
  }

    class User
    {
        public string Username { get; }
        public string Password { get; }
        public List<Todo> Todos { get; }
        public string UserState { get; }


        public User(string username, string password, string userState)
        {
            Username = username;
            Password = password;
            Todos = new List<Todo>();
            UserState = userState;
        }
    }

    class Todo
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public string User{get; set;}
        public DateTime DateAdded{get; set;}
        public Todo(string description){
            Description = description;
            IsCompleted=false;
            DateAdded = DateTime.Now;
        }
        
    }
}
