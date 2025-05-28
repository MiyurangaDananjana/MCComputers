
using MCComputerAPI.Models.Entities;

namespace MCComputerAPI.Data.Interfaces;

public interface IUsersRepositories
{
    User GetUserDetails(int id);

    bool SaveUserDetails(User user);
}