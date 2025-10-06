using App;

// Declare list variables for use in code
List<User> users = new List<User>();
List<Item> items = new List<Item>();
List<Trade> trades = new List<Trade>();

//Check if Data directory exists, if ! then create Data dir
if (!Path.Exists("Data"))
{
  Directory.CreateDirectory("Data");
}

//Checks if all necessary csv files exists, otherwise create them and close the file.
if (!Path.Exists(Path.Combine("Data", "users.csv")))
{
  File.Create(Path.Combine("Data", "users.csv")).Close();
}
if (!File.Exists(Path.Combine("Data", "items.csv")))
{
  File.Create(Path.Combine("Data", "items.csv")).Close();
}
if (!File.Exists(Path.Combine("Data", "trades.csv")))
{
  File.Create(Path.Combine("Data", "trades.csv")).Close();
}

//Reads from user file and assign each line as an index in array.
string[] users_string_from_file = File.ReadAllLines(Path.Combine("Data", "users.csv"));

//Check if array from read file users is empty.If not, populate users list with data from file.
if (users_string_from_file != null | users_string_from_file.Length != 0)
  foreach (string user in users_string_from_file)
  {
    string[] split_user_data = user.Split(';');
    users.Add(new User(split_user_data[0], split_user_data[1], split_user_data[2]));
  }

//Declare and assign variable for while loop
bool is_running = true;

// define and set the variable for which menu to use and user_logged_in to false default.
EMenu menu_choice = EMenu.Main;
User? user_logged_in = null;

//while loop for menu

while (is_running)
{
  //Declare user chosen menu and also ensures user_menu_choice var is null if previously used
  string user_menu_choice = null;

  //switch for menu
  switch (menu_choice)
  {
    //main menu and default when loading program
    case EMenu.Main:
      {
        Console.WriteLine("Welcome to the Trading system™!\n");

        //Check if user is logged in. Redirects to login if not, otherwise main menu.
        if (user_logged_in == null)
        {
          menu_choice = EMenu.Login;
        }
        if (user_logged_in != null)
        {
          //Write line for menu choices on main menu
          Console.WriteLine("== Menu == \n\n1. Check Inventory \n2. Add Item \n3. New trade \n4. Pending trades \n5. Trade history \n6. Log out");
          //Sets user chosen input as user_menu choice. Switch to set menu_choice to send user to correct sub menu
          user_menu_choice = Console.ReadLine();
          switch (user_menu_choice) { case "1": { menu_choice = EMenu.Inventory; } break; case "2": { menu_choice = EMenu.NewItem; } break; case "3": { menu_choice = EMenu.NewTrade; } break; case "6": { menu_choice = EMenu.Logout; } break; case "9": { menu_choice = EMenu.Test; } break; }
        }
      }
      break;

    // Login menu, user will be redirected here if !not_logged_in in main menu
    case EMenu.Login:
      {
        Console.WriteLine("== Login Menu ==");
        Console.WriteLine("1. Login");
        Console.WriteLine("2. Register");
        Console.WriteLine("3. Populate system with test data");
        user_menu_choice = Console.ReadLine();

        //Login, Check user_menu_choice, proceed to login, reg or populate test data
        if (user_menu_choice == "1")
        {
          Console.WriteLine("Please enter your email address and password to login!");
          Console.WriteLine("Email: ");
          string user_email = Console.ReadLine();
          Console.WriteLine("Password: ");
          string user_password = Console.ReadLine();
            
          foreach (User user in users)
          {
              if (user.TryLogin(user_email, user_password))
              {
                user_logged_in = user;
                break;
              }
          }
          menu_choice = EMenu.Main;
        }

        //Check for register menu choice
        if(user_menu_choice == "2")
        {
          menu_choice = EMenu.Register;
        }
        //Check for populate with test data choice
        if (user_menu_choice == "3")
        {
          menu_choice = EMenu.AddTestData;
        }
      }
      break;

      //Menu for logout, ask user for confirmation. If true, set user_logged_in to null and meny to login. 
      //Confirms logout. Otherwise, return to main menu.
    case EMenu.Logout:
      {
        Console.WriteLine("Enter q to log out, any other key to return to main menu.");
        user_menu_choice = Console.ReadLine();
        if (user_menu_choice == "q")
        {
          user_logged_in = null;
          menu_choice = EMenu.Login;
          Console.WriteLine("You have been logged out. Press enter to return to login menu");
          Console.ReadLine();
        }
        else
        {
          menu_choice = EMenu.Main;
        }

      }
      break;




    case EMenu.Register:
      {
        Console.WriteLine("Please enter email, choose username and password to register!");
        Console.WriteLine("Email: ");
        string user_email = Console.ReadLine();
        Console.WriteLine("Username: ");
        string user_username = Console.ReadLine();
        Console.WriteLine("Password: ");
        string user_password = Console.ReadLine();

        //Ask user for confirmation
        Console.WriteLine("\nPlease confirm that your registration info is correct");
        Console.WriteLine($"Email: {user_email} \nUsername: {user_username} \nPassword:{user_password}");
        Console.WriteLine("Press enter to register, press any other key to re-enter details");
        user_menu_choice = Console.ReadLine();

        //Adds the user if user_menu_choice is null or "" and then logs in the user.
        if (user_menu_choice == null | user_menu_choice == "")
        {
          //Writes user data to user file
          File.AppendAllText(Path.Combine("Data", "users.csv"), $"{user_username};{user_email};{user_password}" + Environment.NewLine);
          // Adds user to users list
          users.Add(new User(user_username, user_email, user_password));
          //loops through users for login of registerd user
          foreach (User user in users)
          {
            if (user.TryLogin(user_email, user_password))
            {
              user_logged_in = user;
              break;
            }
          }
          Console.WriteLine("You've been registered and logged in, press enter to proceed to main menu...");
          Console.ReadLine();
        }
        //Sets meny variable to main menu
        menu_choice = EMenu.Main;
      }
      break;

    //Inventory menu. Displays items currently owned by the current user.
    case EMenu.Inventory:
      {
        //Reads all lines from item file and assigns to array

        string[] items_string_from_file = File.ReadAllLines(Path.Combine("Data", "items.csv"));

        //Clears Item list and loops through array and adds them to item list
        items.Clear();


        foreach (string item in items_string_from_file)
        {
          string[] split_item_data = item.Split(';');
          items.Add(new Item(split_item_data[0], split_item_data[1], split_item_data[2]));
        }

        //Counter for items owned by current user.
        int item_count = 0;
        //loops item list and writes all items where owner is the current user.
        foreach (Item item in items)
        {
          if (user_logged_in.Email == item.Owner)
          {
            item_count++;
            Console.WriteLine($"{item_count}. {item.Name}");
          }
        }
        if (item_count == 0)
        {
          Console.WriteLine("You don't have any items!");
        }
        Console.WriteLine("\nPress enter to return to main menu...");
        Console.ReadLine();
        menu_choice = EMenu.Main;
      }
      break;

    //Menu for user to create new item
    case EMenu.NewItem:
      {
        //Asks user for item info and sets it to variables
        Console.WriteLine("Please provide required information on item to add inventory");
        Console.Write("Name: ");
        string user_item_name = Console.ReadLine();
        Console.Write("Description: ");
        string user_item_description = Console.ReadLine();

        //asks user to confirm item, otherwise if reenter or return to main
        Console.WriteLine("\nIs the details for the item correct?");
        Console.WriteLine($"Name: {user_item_name}\nDescription: {user_item_description}\n");
        Console.WriteLine("Press enter to add item, n to enter item again. Press any key to return to main menu.");
        user_menu_choice = Console.ReadLine();

        //check user_menu_choice for add, reenter item data or exit to main
        if (user_menu_choice == null | user_menu_choice == "")
        {
          File.AppendAllText(Path.Combine("Data", "items.csv"), $"{user_item_name};{user_item_description};{user_logged_in.Email}" + Environment.NewLine);
          Console.WriteLine("Press enter to return to main menu");
          Console.ReadLine();
          menu_choice = EMenu.Main;
        }
        if (user_menu_choice == "n")
        {
          menu_choice = EMenu.NewItem;
        }
        else
        {
          menu_choice = EMenu.Main;
        }
      }
      break;


    //Menu for putting an item up for trade
    case EMenu.NewTrade:
      {
        int i = 1;
        Console.WriteLine("What would you like to trade?");
        foreach (Item item in items)
        {
          if (user_logged_in.Email == item.Owner)
            Console.WriteLine($"Name: item.Name \n Description: item.description");
        }

      }
      break;
    //Hidden test menu
    case EMenu.Test:
      {
        //Below is only for tests
        Console.WriteLine("Sooooo, you need to test stuff?\n\n");



        Console.WriteLine("Press enter to return to main menu...");
        Console.ReadLine();
        menu_choice = EMenu.Main;

      }
      break;
    case EMenu.AddTestData:
      // Adds users for test. Creates new lists of test users, writes them to user files and then adds to users list
      {
        List<User> test_users = new List<User>();
        test_users.Add(new User("User 1", "user1@e", "pass"));
        test_users.Add(new User("User 2", "user2@e", "pass"));
        test_users.Add(new User("a", "a", "a"));
        foreach (User add in test_users)
        {
          File.AppendAllText(Path.Combine("Data", "users.csv"), $"{add.Username};{add.Email};{add._password}" + Environment.NewLine);
          users.Add(add);
        }

        // Adds items for test 
        List<Item> test_items = new List<Item>();
        test_items.Add(new Item("Shovel", "Great for digging", "user1@e"));
        test_items.Add(new Item("Horse", "Klad Hest", "user2@e"));
        test_items.Add(new Item("MedPack", "Heals damage", "user2@e"));
        test_items.Add(new Item("Armour", "Good if you're gonna take a beating", "user1@e"));
        test_items.Add(new Item("Car", "If you don't wanna walk or if you're american", "user2@e"));
        test_items.Add(new Item("Dog", "You don't need friends if you have a dog", "user1@e"));
        test_items.Add(new Item("Shoes", "Useful if you couldn't afford the car", "user1@e"));
        test_items.Add(new Item("Scepter of god", "Do whatever you want", "a"));
        foreach (Item add in test_items)
        {
          File.AppendAllText(Path.Combine("Data", "items.csv"), $"{add.Name};{add.Description};{add.Owner}" + Environment.NewLine);
          items.Add(add);
        }
        menu_choice = EMenu.Main;
      }
      break;
    //Exit menu
    case EMenu.Exit:
      {
        Console.WriteLine("Thank you for trading! Press any key to exit...");
        Console.ReadLine();
        is_running = false;
      }
      break;
  }

}
