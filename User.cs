namespace App;

public class User : IUser
{
  public string Username;
  public string Email;
  string _password;

  public User(string username, string email, string password)
  {
    username = Username;
    email = Email;
    _password = password;
  }

  public bool TryLogin(string email, string password)
  {
    return email == Email & password == _password;
  }

}