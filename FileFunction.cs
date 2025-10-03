namespace App;

public class FileFunction
{
  public string Data;
  public string Type;

  //Function for write to file
  public static void FileWrite(string data, string type)
  {
    string filePath;

    //Sets file path depending on which data is sent to FileWrite
    if (type == "u")
    {
      //Check if the Data folder exists, otherwise create it
      if (!Path.Exists("Data"))
      {
        Directory.CreateDirectory("Data");
      }
      //Writes data to file
      File.AppendAllText(Path.Combine("Data", "users.txt"), data);

    }
    if (type == "i")
    {
      filePath = Path.Combine("Data", "items.txt");
      //Check if the Data folder exists, otherwise create it
      if (!Path.Exists("Data"))
      {
        Directory.CreateDirectory("Data");
      }
      //Writes data to file
      File.AppendAllText(Path.Combine("Data", "items.txt"), data);

    }
    if (type == "l")
    {
      filePath = Path.Combine("Data", "log.txt");
      //Check if the Data folder exists, otherwise create it
      if (!Path.Exists("Data"))
      {
        Directory.CreateDirectory("Data");
      }
      File.AppendAllText(Path.Combine("Data", "log.txt"), data);

    }



  }
}