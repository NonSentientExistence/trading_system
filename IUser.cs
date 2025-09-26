namespace App;

interface IUser
{
  bool TryLogin(string email, string password);
}