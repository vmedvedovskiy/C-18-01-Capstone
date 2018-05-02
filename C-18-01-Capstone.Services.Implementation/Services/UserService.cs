using System;
using System.Linq;
using C_18_01_Capstone.Main.DataAccessLayer;
using C_18_01_Capstone.Main.DataContext;
using C_18_01_Capstone.Services.Services;

namespace C_18_01_Capstone.Services.Implementation.Services
{
    public class UserService : IUserService
    {
        private readonly IEncryptionService encryptionService;
        private readonly IDataAccess<User> dataAccess;

        public UserService(
            IEncryptionService encryptionService,
            IDataAccess<User> dataAccess)
        {
            this.encryptionService = encryptionService;
            this.dataAccess = dataAccess;
        }

        public void Add(CreateUserModel user)
        {
            string salt = this.encryptionService
                .CreateSalt();

            var password = this.encryptionService
                    .EncryptPassword(user.Password, salt);

            this.dataAccess.AddEntity(this.Convert(
                user, salt, password));
        }

        public UserModel FindUser(string login)
        {
            return this.dataAccess.GetEntities()
                .Where(_ => _.Login == login)
                .Select(this.Convert)
                .SingleOrDefault();
        }

        private UserModel Convert(User user)
        {
            return new UserModel
            {
                BirthDate = user.BirthDate,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Login = user.Login,
                HashedPassword = user.HashedPassword,
                Salt = user.Salt
                Country = new CountryModel
                {
                    CountryIsoCode3 = user.Country.CountryIsoCode3,
                    Name = user.Country.Name
                }
            };
        }

        private User Convert(
            CreateUserModel user,
            string salt,
            string hashedPassword)
        {
            return new User
            {
                UserId = Guid.NewGuid(),
                BirthDate = user.BirthDate,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Login = user.Login,
                CountryId = user.CountryIsoCode3,
                Salt = salt,
                HashedPassword = hashedPassword
            };
        }
    }
}
