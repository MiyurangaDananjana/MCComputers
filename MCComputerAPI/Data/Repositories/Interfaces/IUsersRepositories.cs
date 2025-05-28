
using MCComputerAPI.Models.Entities;


namespace MCComputerAPI.Data.Interfaces;

public interface IUsersRepositories
{
    User GetUserById(int id);

    bool SaveUserDetails(User user);

    User UpdateUserDetails(User user);

    bool DeleteUser(int id);

    User GetUserByUsername(string userName);
}