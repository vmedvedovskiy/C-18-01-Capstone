using C_18_01_Capstone.Main.DataContext;

namespace C_18_01_Capstone.Services.Services
{
  public interface IUserService
  {
    void Add(User user);

    User FindUser(string login);
  }
}
