//namespace App;

public class FileFunction
{
  public string Data;
  public string Type;

  //Function for write to file
  public static void FileWrite(string data, string type)
  {

    //Check if the Data folder exists, otherwise create it
    if (!Path.Exists("Data"))
    {
      Directory.CreateDirectory("Data");
    }

    //Sets file path depending on which data is sent to FileWrite
    if (type == "u")
    {
      //Writes data to file
      File.AppendAllText(Path.Combine("Data", "users.csv"), data);
    }
    if (type == "i")
    {
      //Writes data to file
      File.AppendAllText(Path.Combine("Data", "items.csv"), data);

    }
    if (type == "l")
    {
      File.AppendAllText(Path.Combine("Data", "log.csv"), data);
    }
  }

  //Takes type of data to return as parameter, returns requested data from file.
  public string[] FileRead(string type)
  {
    //Check if the file exists
    if (type == "u")
    {

      if (!File.Exists(Path.Combine("Data", "users.csv")))
      {
        Console.WriteLine("No users has been added");
        Console.ReadLine();
        return null;
      }

      string[] users_string_from_file;
      users_string_from_file = File.ReadAllLines(Path.Combine("Data", "users.csv"));

      return users_string_from_file;

    }
    else
    {
      return null;
    }
/*
        if (type == "i")
        {
          if (!File.Exists(Path.Combine("Data", "items.csv")))

          {
            Console.WriteLine("You have no items in your inventory!");
            Console.ReadLine();
            return null;
          }
          List<User> users = new List<User>();
          string[] users_string_from_file = File.ReadAllLines(Path.Combine("Data", "users.csv"));

        }

    */
  }
}  