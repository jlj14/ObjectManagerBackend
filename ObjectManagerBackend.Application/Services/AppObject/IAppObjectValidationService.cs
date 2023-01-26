using ObjectManagerBackend.Application.DTOs.AppObject.Request;
using ObjectManagerBackend.Domain.Exceptions;

namespace ObjectManagerBackend.Application.Services.AppObject
{
    /// <summary>
    /// AppObject validation service contract
    /// </summary>
    public interface IAppObjectValidationService
    {
        /// <summary>
        /// Validates the creation request
        /// </summary>
        /// <param name="request">Creation request</param>
        /// <exception cref="BadRequestException">Request validation is not valid</exception>
        /// <exception cref="NotFoundException">Parent object not found</exception>
        Task Validate(AppObjectCreateRequest request);

        /// <summary>
        /// Validates the update request
        /// </summary>
        /// <param name="request">Update request</param>
        /// <exception cref="BadRequestException">Request validation is not valid</exception>
        /// <exception cref="NotFoundException">Parent object not found</exception>
        Task Validate(AppObjectUpdateRequest request);
    }
}
