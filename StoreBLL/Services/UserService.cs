namespace StoreBLL.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using StoreBLL.Interfaces;
    using StoreBLL.Models;
    using StoreDAL.Entities;
    using StoreDAL.Interfaces;

    /// <summary>
    /// Service class for managing users. Implements CRUD operations via <see cref="ICrud"/>.
    /// </summary>
    public class UserService : ICrud
    {
        private readonly IUserRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="repository">Repository for accessing users.</param>
        /// <exception cref="ArgumentNullException">Thrown if repository is null.</exception>
        public UserService(IUserRepository repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>
        /// Attempts to log in a user with the given login and password.
        /// </summary>
        public UserModel? Login(string login, string password)
        {
            var entity = this.repository.GetAll().FirstOrDefault(u => u.Login == login);
            if (entity != null && entity.Password == password)
            {
                return MapToModel(entity);
            }
            return null;
        }

        /// <inheritdoc/>
        public void Add(AbstractModel model)
        {
            if (model is not UserModel userModel)
                throw new ArgumentException("Model is not a UserModel.", nameof(model));

            var entity = MapToEntity(userModel);
            this.repository.Add(entity);
        }

        /// <inheritdoc/>
        public void Delete(int modelId)
        {
            this.repository.DeleteById(modelId);
        }

        /// <inheritdoc/>
        public IEnumerable<AbstractModel> GetAll()
        {
            return this.repository
                .GetAll()
                .Select(MapToModel)
                .Where(m => m != null)
                .Cast<AbstractModel>()
                .ToList();
        }

        /// <inheritdoc/>
        public AbstractModel GetById(int id)
        {
            var entity = this.repository.GetById(id)
                ?? throw new InvalidOperationException($"User with ID {id} not found.");

            return MapToModel(entity) !;
        }

        /// <inheritdoc/>
        public void Update(AbstractModel model)
        {
            if (model is not UserModel userModel)
                throw new ArgumentException("Model is not a UserModel.", nameof(model));

            var entity = MapToEntity(userModel);
            this.repository.Update(entity);
        }

        /// <summary>
        /// Maps a <see cref="User"/> entity to a <see cref="UserModel"/>.
        /// </summary>
        private static UserModel MapToModel(User entity)
        {
            return new UserModel(
                entity.Id,
                entity.UserRoleId,
                entity.FirstName,
                entity.LastName,
                entity.Login,
                entity.Password,
                entity.UserRole?.UserRoleName);
        }

        /// <summary>
        /// Maps a <see cref="UserModel"/> to a <see cref="User"/> entity.
        /// </summary>
        private static User MapToEntity(UserModel model)
        {
            return new User(
                model.Id,
                model.FirstName,
                model.LastName,
                model.Login,
                model.Password,
                model.UserRoleId);
        }
    }
}
