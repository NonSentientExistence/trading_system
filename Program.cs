using App;

//create variable for while loop
bool is_running = true;

// create and set the variable for which menu to use

EMenu menu_choice = EMenu.Main;

//while loop for menu

while (is_running)
{

  //switch for menu
  switch (menu_choice)
  {
    //main menu and default when loading program
    case EMenu.Main:
      {
        Console.WriteLine("Welcome to the main menu!");
        Console.ReadLine();
      }
      break;

    //Hidden test menu
    case EMenu.Test:
      {
        Console.WriteLine("Sooooo, you need to test stuff?");
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
