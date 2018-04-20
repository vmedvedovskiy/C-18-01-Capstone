using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C_18_01_Capstone.Main.DataAccessLayer;
using C_18_01_Capstone.Main.DataContext;
using C_18_01_Capstone.Services.Services;

namespace C_18_01_Capstone.Services.Implementation.Services
{
  public class UserService : IUserService
  {
    public void Add(User user)
    {
            DataAccess<User> dataAccess = new DataAccess<User>();

            dataAccess.AddEntity(user);
    }
  }
}
