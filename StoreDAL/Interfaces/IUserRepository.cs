namespace StoreDAL.Interfaces;
using Entities;

public interface IUserRepository : IRepository<User>
{
    public User? GetUserByLogin(string? login);
}
