using System.Linq;
using C_18_01_Capstone.Main.DataAccessLayer;
using C_18_01_Capstone.Main.DataContext;
using C_18_01_Capstone.Services.Services;

namespace C_18_01_Capstone.Services.Implementation.Services
{
    public class UserService : IUserService
    {
        private readonly IDataAccess<User> dataAccess;

        public UserService(IDataAccess<User> dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        public void Add(User user)
            => this.dataAccess.AddEntity(user);

        public User FindUser(string login)
        {
            return this.dataAccess.GetEntities()
                .Where(_ => _.Login == login)
                .SingleOrDefault();
        }
    }
}
