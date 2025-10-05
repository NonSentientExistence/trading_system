using App;

List<User> users = new List<User>();

users.Add(new User("User 1", "user1@e", "pass"));
users.Add(new User("User 2", "user2@e", "pass"));
users.Add(new User("a", "a", "a"));

//create new list of items and adds items to the list

List<Item> items = new List<Item>();

items.Add(new Item("Shovel", "Great for digging", "user1@e"));
items.Add(new Item("Horse", "Klad Hest", "user2@e"));
items.Add(new Item("MedPack", "Heals damage", "user2@e"));
items.Add(new Item("Armour", "Good if you're gonna take a beating", "user1@e"));
items.Add(new Item("Car", "If you don't wanna walk or if you're american", "user2@e"));
items.Add(new Item("Dog", "You don't need friends if you have a dog", "user1@e"));
items.Add(new Item("Shoes", "Useful if you couldn't afford the car", "user1@e"));
items.Add(new Item("Scepter of god", "Do want you want", "a"));

//create variable for while loop
bool is_running = true;

// define and set the variable for which menu to use and user_logged_in to false default.
EMenu menu_choice = EMenu.Main;
User? user_logged_in = null;

//Declare for use on user chosen menus
string user_menu_choice;

//while loop for menu

while (is_running)
{

  //switch for menu
  switch (menu_choice)
  {
    //main menu and default when loading program
    case EMenu.Main:
    {
      //ensure user_menu_choice var is null if previously used
      user_menu_choice = null;

      //Just for a pause in the main menu. Might remove later
      Console.WriteLine("Welcome to the main menu!");
      Console.ReadLine();

      if (user_logged_in == null)
      {
        menu_choice = EMenu.Login;
      }
      if (user_logged_in != null)
      {
        //Write line for menu choices on main menu
        Console.WriteLine("Menu: \n\n1. Check Inventory \n2. Add Item \n3. New trade \n4. Pending trades \n5. Trade history");
        //Sets user chosen input as user_menu choice. Switch to set menu_choice to send user to correct menu
        user_menu_choice = Console.ReadLine();
        switch (user_menu_choice) { case "1": { menu_choice = EMenu.Inventory; } break; case "2": { menu_choice = EMenu.NewItem; } break; case "3": { menu_choice = EMenu.NewTrade; } break; case "9": { menu_choice = EMenu.Test; } break; }
      }

    }
    break;
    // Login menu, if user not logged in then menu_choice will be set to EMenu.Login
    case EMenu.Login:
    {
      //ensure user_menu_choice var is null if previously used
      user_menu_choice = null;

      //Check if user is logged in
      if (user_logged_in == null)
      {
        Console.WriteLine("Press enter to login, press any key to register...");
        user_menu_choice = Console.ReadLine();

          //Login, Check if user_menu_choice is null or empty, true then proceed to login
          if (user_menu_choice == null | user_menu_choice == "")
          {
            Console.WriteLine("Please enter your email address and password to login!");
            Console.WriteLine("Email: ");
            string user_email = Console.ReadLine();
            Console.WriteLine("Password: ");
            string user_password = Console.ReadLine();

            if (!File.Exists(Path.Combine("Data", "users.csv")))
            {
              Console.WriteLine("No users has been added");
              Console.ReadLine();
            }

            
            string[] users_string_from_file = File.ReadAllLines(Path.Combine("Data", "users.csv"));

            foreach (string user in users_string_from_file)
            {
              string[] split_user_data = user.Split(';');
              users.Add(new User(split_user_data[0], split_user_data[1], split_user_data[2]));
            }
            

            foreach (User user in users)
            {
              if (user.TryLogin(user_email, user_password))
              {
                user_logged_in = user;
                break;
              }
            }

            /*
                  foreach (User user in users)
                  {
                    if (user.TryLogin(user_email, user_password))
                    {
                      user_logged_in = user;
                      break;
                    }
                  }
                */
          }
          //Register as new user if input wasn't null or ""
          else
          {
            //ensure user_menu_choice var is null if previously used
            user_menu_choice = null;

            Console.WriteLine("Please enter your email address, preferred username and a password to register!");
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
              //users.Add(new User(user_username, user_email, user_password));

              //Check if Data directory exists, if ! then create Data dir
              if (!Path.Exists(Path.Combine("Data", "users.csv")))
              {
                if (!Path.Exists("Data"))
                {
                  Directory.CreateDirectory("Data");
                }
                File.Create(Path.Combine("Data", "users.csv")).Close();
              }

              string[] users_string_from_file = File.ReadAllLines(Path.Combine("Data", "users.csv"));

              foreach (string user in users_string_from_file)
              {
                string[] split_user_data = user.Split(';');
                users.Add(new User(split_user_data[0], split_user_data[1], split_user_data[2]));
              }

              File.AppendAllText(Path.Combine("Data", "users.csv"), $"{user_username};{user_email};{user_password}" + Environment.NewLine);
              // Call FileWrite method to add user to text file
              //FileFunction.FileWrite($"{user_username};{user_email};{user_password}", "u");

              foreach (User user in users)
              {
                if (user.TryLogin(user_email, user_password))
                {
                  user_logged_in = user;
                  break;
                }
              }
            }




          }

      }
      menu_choice = EMenu.Main;
    }
    break;

    //Inventory menu. Displays items currently owned by the current user.
    case EMenu.Inventory:
    {
        if (File.Exists(Path.Combine("Data", "items.csv")))
        {
          //Counter for items
          int item_count = 0;
          foreach (Item item in items)
          {
            if (user_logged_in.Email == item.Owner)
            {
              Console.WriteLine(item.Name);
              item_count++;
            }
          }
        }
        
        if (!File.Exists(Path.Combine("Data", "items.csv")))
        {
          Console.WriteLine("You have no items");
          Console.ReadLine();
        }
        
        
      Console.WriteLine("\nPress enter to return to main menu...");
      Console.ReadLine();
      menu_choice = EMenu.Main;
    }
    break;

    //Menu for user to create new item
    case EMenu.NewItem:
    {
      //ensure user_menu_choice var is null if previously used
      user_menu_choice = null;

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

      //check user_menu_choice input and add, reenter or exit to main
      if (user_menu_choice == null | user_menu_choice == "")
      {
          // Call FileWrite method to add item to text file
          //FileFunction.FileWrite($"{user_item_name};{user_item_description}", "i");

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
