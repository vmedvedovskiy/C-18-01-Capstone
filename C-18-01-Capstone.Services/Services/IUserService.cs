namespace C_18_01_Capstone.Services.Services
{
    public interface IUserService
    {
        void Add(CreateUserModel user);

        UserModel FindUser(string login);
    }
}
