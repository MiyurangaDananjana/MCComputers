
using System.Runtime.Serialization;
using MCComputerAPI.Models.Entities;
using MCComputerAPI.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace MCComputerAPI.Data.Implementations;

public class UsersRepositories : IUsersRepositories
{
    private readonly AppDbContext _context;
    public UsersRepositories(AppDbContext context)
    {
        _context = context;
    }

    public User GetUserById(int id)
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

    public User UpdateUserDetails(User user)
    {
        try
        {
            User userInfo = _context.Users.First(x => x.Id == user.Id);

            if (userInfo == null)
            {
                throw new ArgumentException("User not found");
            }

            userInfo.FirstName = user.FirstName;
            userInfo.LastName = user.LastName;
            userInfo.UserName = user.UserName;
            userInfo.Password = user.Password;
            userInfo.CreateDate = DateTime.UtcNow;

            _context.SaveChanges();

            return userInfo;

        }
        catch (Exception ex)
        {
            Log.Error("Update User Details filed! " + ex);
            return user;
        }
    }

    public bool DeleteUser(int id)
    {
        var user = _context.Users.Find(id);

        if (user == null)
        {
            Log.Warning("DeleteUser: No user found with ID {UserId}", id);
            return false;
        }

        _context.Users.Remove(user);
        _context.SaveChanges();

        Log.Information("DeleteUser: Successfully deleted user with ID {UserId}", id);
        return true;
    }

    public User GetUserByUsername(string userName)
    {
        var userDetails = _context.Users
            .AsNoTracking()
            .FirstOrDefault(x => x.UserName == userName);

        if (userDetails == null)
        {
            throw new InvalidOperationException($"User with username '{userName}' not found.");
        }

        return userDetails;
    }
}