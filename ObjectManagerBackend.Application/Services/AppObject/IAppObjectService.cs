using ObjectManagerBackend.Application.DTOs.AppObject.Request;
using ObjectManagerBackend.Application.DTOs.AppObject.Response;
using ObjectManagerBackend.Application.DTOs.PaginatedResponse;
using ObjectManagerBackend.Domain.Exceptions;

namespace ObjectManagerBackend.Application.Services.AppObject
{
    /// <summary>
    /// AppObject service contract
    /// </summary>
    public interface IAppObjectService
    {
        /// <summary>
        /// Gets all objects
        /// </summary>
        /// <returns>A collection of objects</returns>
        Task<IEnumerable<AppObjectResponse>> GetAllObjectsAsync();

        /// <summary>
        /// Gets the objects which haven't got any parent, it means, the root objects
        /// </summary>
        /// <param name="pageNumber">Page number; if it is null, it won't be used</param>
        /// <param name="pageSize">Page size; if it is null, it won't be used</param>
        /// <returns>A paginated response of objects</returns>
        Task<PaginatedResponse<AppObjectResponse>> GetRootObjectsPaginatedAsync(int? pageNumber, int? pageSize);

        /// <summary>
        /// Gets the child objects for a specific parent object
        /// </summary>
        /// <param name="parentId">Id of the parent object</param>z
        /// <returns>A collection of objects</returns>
        Task<IEnumerable<AppObjectResponse>> GetChildObjectsAsync(int parentId);

        /// <summary>
        /// Gets an object by id
        /// </summary>
        /// <param name="id">Object id</param>
        /// <returns>The object or null if it doesn't exists</returns>
        Task<AppObjectResponse> GetByIdAsync(int id);

        /// <summary>
        /// Creates a new object
        /// </summary>
        /// <param name="request">Create request</param>
        /// <returns>The new object created</returns>
        Task<AppObjectResponse> CreateAsync(AppObjectCreateRequest request);

        /// <summary>
        /// Updates a new object
        /// </summary>
        /// <param name="request">Update request</param>
        /// <exception cref="NotFoundException">Object not found</exception>
        Task UpdateAsync(AppObjectUpdateRequest request);

        /// <summary>
        /// Deletes an object
        /// </summary>
        /// <remarks>
        /// If the object has got child objects, these are updated setting its parentId to null
        /// </remarks>
        /// <param name="id">Object identifier</param>
        /// <exception cref="NotFoundException">Object not found</exception>
        Task DeleteAsync(int id);
    }
}
