
using MCComputerAPI.Models.Entities;


namespace MCComputerAPI.Repositories.Data.Interfaces;

public interface IUsersRepositories
{
    User GetUserDetails(int id);

    bool SaveUserDetails(User user);
}