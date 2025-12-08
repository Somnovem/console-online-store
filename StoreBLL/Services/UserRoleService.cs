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
    /// Service class for managing user roles.
    /// Implements CRUD operations via <see cref="ICrud"/>.
    /// </summary>
    public class UserRoleService : ICrud
    {
        private readonly IUserRoleRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRoleService"/> class.
        /// </summary>
        /// <param name="repository">Repository for accessing user roles.</param>
        /// <exception cref="ArgumentNullException">Thrown if repository is null.</exception>
        public UserRoleService(IUserRoleRepository repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <inheritdoc/>
        public void Add(AbstractModel model)
        {
            if (model is not UserRoleModel userRoleModel)
            {
                throw new ArgumentException("Model is not a UserRoleModel.", nameof(model));
            }

            var entity = MapToEntity(userRoleModel);
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
            return this.repository.GetAll()
                .Select(MapToModel)
                .Where(m => m != null)
                .Cast<AbstractModel>()
                .ToList();
        }

        /// <inheritdoc/>
        public AbstractModel GetById(int id)
        {
            var entity = this.repository.GetById(id)
                ?? throw new InvalidOperationException($"UserRole with ID {id} not found.");

            return MapToModel(entity) !;
        }

        /// <inheritdoc/>
        public void Update(AbstractModel model)
        {
            if (model is not UserRoleModel userRoleModel)
            {
                throw new ArgumentException("Model is not a UserRoleModel.", nameof(model));
            }

            var entity = MapToEntity(userRoleModel);
            this.repository.Update(entity);
        }

        /// <summary>
        /// Maps a <see cref="UserRole"/> entity to a <see cref="UserRoleModel"/>.
        /// </summary>
        private static UserRoleModel? MapToModel(UserRole? entity)
        {
            if (entity == null) return null;
            return new UserRoleModel(entity.Id, entity.UserRoleName);
        }

        /// <summary>
        /// Maps a <see cref="UserRoleModel"/> to a <see cref="UserRole"/> entity.
        /// </summary>
        private static UserRole MapToEntity(UserRoleModel model)
        {
            return new UserRole(model.Id, model.UserRoleName);
        }
    }
}
