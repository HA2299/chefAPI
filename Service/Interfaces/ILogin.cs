using Repository.Entities;
using Service.Dto;

public interface ILogin
{
    Task<User> Authenticate(UserLogin user);
    Task<bool> Register(UserRegister userRegister);
    Task<User> GetUserByToken(string token); 
}
