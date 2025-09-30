using App;

List<User> users = new List<User>();

users.Add(new User("User 1", "user1@e", "pass"));
users.Add(new User("User 2", "user2@e", "pass"));
users.Add(new User("a", "a", "a"));

//create new list of items and adds items to the list

List<Item> items = new List<Item>();

items.Add(new Item("Shovel", "user1@e", "Great for digging"));
items.Add(new Item("Horse", "user2@e", "Klad Hest"));
items.Add(new Item("MedPack", "user2@e", "Heals damage"));
items.Add(new Item("Armour", "user1@e", "Good if you're gonna take a beating"));
items.Add(new Item("Car", "user2@e", "If you don't wanna walk or if you're american"));
items.Add(new Item("Dog", "user1@e", "You don't need friends if you have a dog"));
items.Add(new Item("Shoes", "user1@e", "Useful if you couldn't afford the car"));
items.Add(new Item("Scepter of god", "a", "Do want you want"));

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
          Console.WriteLine("Menu: \n\n1. Check Inventory \n2. New trade \n3. Pending trades \n4. Trade history");
          //Sets user chosen input as user_menu choice. Switch to set menu_choice to send user to correct menu
          user_menu_choice = Console.ReadLine();
          switch (user_menu_choice) { case "1": { menu_choice = EMenu.Inventory; } break; case "2": { menu_choice = EMenu.NewTrade; } break; case "9": { menu_choice = EMenu.Test; } break; }
        }

      }
      break;
    // Login menu, if user not logged in then menu_choice will be set to EMenu.Login
    case EMenu.Login:
      {
        //Check if user is logged in
        if (user_logged_in == null)
        {
          Console.WriteLine("Please enter your email address and password!");
          Console.WriteLine("Email: ");
          string user_email = Console.ReadLine();
          Console.WriteLine("Password: ");
          string user_password = Console.ReadLine();

          //loops available users, matching email and password.
          foreach (User user in users)
          {
            if (user.TryLogin(user_email, user_password))
            {
              user_logged_in = user;
              break;
            }
          }
        }
        menu_choice = EMenu.Main;
      }

      break;

    //Inventory menu. Displays items currently owned by the current user.
    case EMenu.Inventory:
      {
        foreach (Item item in items)
        {
          if (user_logged_in.Email == item.Owner)
            Console.WriteLine(item.Name);
        }
        Console.WriteLine("\nPress enter to return to main menu...");
        Console.ReadLine();
        menu_choice = EMenu.Main;
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

    //Hidden test menu
    case EMenu.Test:
      {
        //Below is only for tests
        Console.WriteLine("Sooooo, you need to test stuff?");
        Console.WriteLine(user_logged_in.Email);

        Console.ReadLine();

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
