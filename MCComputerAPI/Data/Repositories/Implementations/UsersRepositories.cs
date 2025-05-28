
using System.Runtime.Serialization;
using MCComputerAPI.Models.Entities;
using MCComputerAPI.Repositories.Data.Interfaces;
using Serilog;

namespace MCComputerAPI.Data.Implementations;

public class UsersRepositories : IUsersRepositories
{
    private readonly AppDbContext _context;
    public UsersRepositories(AppDbContext context)
    {
        _context = context;
    }

    public User GetUserDetails(int id)
    {
        User user = _context.Users.Where(x => x.Id == id).First();
        return user;
    }

    public bool SaveUserDetails(User user)
    {
        try
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {

            Log.Error("User details were not saved! " + ex);
            return false;
        }
        
    }
}