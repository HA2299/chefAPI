using Repository.Entities;
using Service.Dto;

public interface ILogin
{
    User Authenticate(UserLogin user);
    bool Register(UserRegister userRegister);
    User GetUserByToken(string token); 
}
