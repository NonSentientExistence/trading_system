namespace App;

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
      File.AppendAllText(Path.Combine("Data", "users.txt"), data);
    }
    if (type == "i")
    {
      //Writes data to file
      File.AppendAllText(Path.Combine("Data", "items.txt"), data);

    }
    if (type == "l")
    {
      File.AppendAllText(Path.Combine("Data", "log.txt"), data);
    }
  }

  


}