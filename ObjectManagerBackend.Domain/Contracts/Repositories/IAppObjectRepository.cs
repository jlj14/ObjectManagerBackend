using ObjectManagerBackend.Domain.Models.AppObject;

namespace ObjectManagerBackend.Domain.Contracts.Repositories
{
    /// <summary>
    /// AppObject repository contract
    /// </summary>
    public interface IAppObjectRepository
    {
        /// <summary>
        /// Gets all objects
        /// </summary>
        /// <returns>A collection of objects</returns>
        Task<IEnumerable<AppObjectModel>> GetAllObjectsAsync();

        /// <summary>
        /// Gets the objects which haven't got any parent, it means, the root objects
        /// </summary>
        /// <param name="skip">Optional parameter: if it has value, the N first values are skipped; if not, noone is skipped</param>
        /// <param name="take">Optional parameter: if it has value, N values are taken; if not, all values all taken</param>
        /// <returns>A collection of objects</returns>
        Task<IEnumerable<AppObjectModel>> GetRootObjectsAsync(int? skip = null, int? take = null);

        /// <summary>
        /// Gets the total count of objects which haven't got any parent, it means, the root objects
        /// </summary>
        /// <returns>The total count</returns>
        Task<int> GetRootObjectsTotalCountAsync();

        /// <summary>
        /// Gets the child objects for a specific parent object
        /// </summary>
        /// <param name="parentId">Id of the parent object</param>
        /// <returns>A collection of objects</returns>
        Task<IEnumerable<AppObjectModel>> GetChildObjectsAsync(int parentId);

        /// <summary>
        /// Gets an object by id
        /// </summary>
        /// <param name="id">Object id</param>
        /// <returns>The object or null if it doesn't exists</returns>
        Task<AppObjectModel> GetByIdAsync(int id);

        /// <summary>
        /// Creates a new object
        /// </summary>
        /// <param name="model">Object to create</param>
        /// <returns>The id of the new object</returns>
        Task<int> CreateAsync(AppObjectModel model);

        /// <summary>
        /// Updates a new object
        /// </summary>
        /// <param name="model">Object to update</param>
        /// <exception cref="NotFoundException">Entity to update not found</exception>
        Task UpdateAsync(AppObjectModel model);

        /// <summary>
        /// Deletes an object
        /// </summary>
        /// <param name="id">Object id</param>
        /// <exception cref="NotFoundException">Entity to delete not found</exception>
        Task DeleteAsync(int id);
    }
}
