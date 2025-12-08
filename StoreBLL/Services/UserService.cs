namespace StoreBLL.Services;

using ConsoleApp.Controllers;
using Exceptions;
using Helpers;
using StoreDAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using Interfaces;
using Models;
using StoreDAL.Data;
using StoreDAL.Entities;
using StoreDAL.Interfaces;

/// <summary>
/// Service for managing users, including CRUD operations and secure password handling.
/// </summary>
public class UserService : ICrud
{
    private readonly IUserRepository repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserService"/> class with the given database context.
    /// </summary>
    /// <param name="context">The database context used to access user data.</param>
    public UserService(StoreDbContext context)
    {
        this.repository = new UserRepository(context);
    }

    /// <summary>
    /// Adds a new user to the repository with a securely hashed password.
    /// </summary>
    /// <param name="model">The <see cref="UserModel"/> containing user information.</param>
    public void Add(AbstractModel model)
    {
        UserModel userModel = (UserModel)model;
        ArgumentNullException.ThrowIfNull(userModel.Password);

        userModel.Password = PasswordHelper.HashPassword(userModel.Password);

        this.repository.Add(new User(
            userModel.Id,
            userModel.Name,
            userModel.LastName,
            userModel.Login,
            userModel.Password,
            userModel.RoleId));
    }

    /// <summary>
    /// Deletes a user from the repository by ID.
    /// </summary>
    /// <param name="modelId">The ID of the user to delete.</param>
    public void Delete(int modelId)
    {
        var entity = this.repository.GetById(modelId);
        this.repository.Delete(entity);
    }

    /// <summary>
    /// Retrieves all users from the repository.
    /// Note: Passwords are not returned for security reasons.
    /// </summary>
    /// <returns>A collection of <see cref="UserModel"/> without passwords.</returns>
    public IEnumerable<AbstractModel> GetAll()
    {
        return this.repository.GetAll().Select(u => new UserModel(
            u.Id,
            u.Name,
            u.LastName,
            u.Login,
            password: null,
            u.RoleId));
    }

    /// <summary>
    /// Retrieves a user by ID.
    /// Note: Password is not returned for security reasons.
    /// </summary>
    /// <param name="id">The ID of the user.</param>
    /// <returns>The <see cref="UserModel"/> corresponding to the ID.</returns>
    public AbstractModel GetById(int id)
    {
        var u = this.repository.GetById(id);
        return new UserModel(
            u.Id,
            u.Name,
            u.LastName,
            u.Login,
            password: null,
            u.RoleId);
    }

    /// <summary>
    /// Updates a user's information in the repository.
    /// If a new password is provided, it is securely hashed before storage.
    /// </summary>
    /// <param name="model">The <see cref="UserModel"/> containing updated user information.</param>
    public void Update(AbstractModel model)
    {
        UserModel userModel = (UserModel)model;

        if (!string.IsNullOrEmpty(userModel.Password))
        {
            userModel.Password = PasswordHelper.HashPassword(userModel.Password);
        }

        this.repository.Update(new User(
            userModel.Id,
            userModel.Name,
            userModel.LastName,
            userModel.Login,
            userModel.Password!,
            userModel.RoleId));
    }

    /// <summary>
    /// A method that logs in a user.
    /// </summary>
    /// <param name="login">User's login.</param>
    /// <param name="password">User's password.</param>
    /// <returns>Tuple with id and userRole.</returns>
    /// <exception cref="UserNotFound">Threw if user was not found in the system.</exception>
    public (int, UserRoles) Login(string login, string password)
    {
        User? user = this.repository.GetUserByLogin(login);

        if (user == null || !PasswordHelper.VerifyPassword(password, user.Password))
        {
            throw new UserNotFound("Invalid login or password");
        }

        return new ValueTuple<int, UserRoles>(user.Id, StringRoleToEnum(user.Role.RoleName));
    }

    /// <summary>
    /// Generates a new id for a new record.
    /// </summary>
    /// <returns>Returns int which is a new id.</returns>
    public int GenerateNewId()
    {
        return this.GetAll().Last().Id + 1;
    }

    /// <summary>
    /// Checks if a user with such login already exists.
    /// </summary>
    /// <param name="login">Login of a user.</param>
    /// <returns>True if login exists - otherwise false.</returns>
    public bool CheckIfLoginValid(string login)
    {
        User? user = this.repository.GetUserByLogin(login);
        return user == null;
    }

    private static UserRoles StringRoleToEnum(string roleName)
    {
        UserRoles result = UserRoles.Guest;

        switch (roleName)
        {
            case "Admin":
                result = UserRoles.Administrator;
                break;
            case "Registered":
                result = UserRoles.RegistredCustomer;
                break;
        }

        return result;
    }
}