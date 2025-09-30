namespace App;

// new class for creating users
public class User
{
  //email will be used for login unique id. Username for display purpose.
  public string Username;
  public string Email;
  string _password;

  public User(string username, string email, string password)
  {
    Username = username;
    Email = email;
    _password = password;
  }

  //Function to validate login info
  public bool TryLogin(string email, string password)
  {
    return email == Email && password == _password;
  }

}