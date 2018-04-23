namespace C_18_01_Capstone.Services.Services
{
  public interface IEncryptionService
  {
    string CreateSalt();
    string EncryptPassword(string password, string salt);
  }
}
