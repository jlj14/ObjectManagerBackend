using FluentValidation;
using ObjectManagerBackend.Application.DTOs.AppObject.Request;
using ObjectManagerBackend.Application.Services.Abstract;
using ObjectManagerBackend.Domain.Constants;
using ObjectManagerBackend.Domain.Contracts.Repositories;
using ObjectManagerBackend.Domain.Exceptions;
using ObjectManagerBackend.Domain.Models.AppObject;

namespace ObjectManagerBackend.Application.Services.AppObject
{
    /// <summary>
    /// AppObject validation service implementation
    /// </summary>
    public class AppObjectValidationService : ValidatorServiceBase, IAppObjectValidationService
    {
        private readonly IAppObjectRepository _appObjectRepository;
        private readonly IValidator<AppObjectCreateRequest> _createRequestValidator;
        private readonly IValidator<AppObjectUpdateRequest> _updateRequestValidator;

        /// <summary>
        /// Constructor: Creates a new instance of <see cref="AppObjectValidationService"/>
        /// </summary>
        /// <param name="appObjectRepository"></param>
        /// <param name="createRequestValidator"></param>
        /// <param name="updateRequestValidator"></param>
        public AppObjectValidationService(
            IAppObjectRepository appObjectRepository, 
            IValidator<AppObjectCreateRequest> createRequestValidator, 
            IValidator<AppObjectUpdateRequest> updateRequestValidator)
        {
            _appObjectRepository = appObjectRepository;
            _createRequestValidator = createRequestValidator;
            _updateRequestValidator = updateRequestValidator;
        }

        /// <inheritdoc/>
        public async Task Validate(AppObjectCreateRequest request)
        {
            HandleValidationResult(_createRequestValidator.Validate(request));
            
            if (request.ParentId.HasValue)
            {
                AppObjectModel parent = await _appObjectRepository.GetByIdAsync(request.ParentId.Value);
                if (parent is null)
                    throw new NotFoundException(AppObjectErrorMessages.PARENT_OBJECT_NOT_FOUND);
            }
        }

        /// <inheritdoc/>
        public async Task Validate(AppObjectUpdateRequest request)
        {
            HandleValidationResult(_updateRequestValidator.Validate(request));

            if (request.ParentId.HasValue)
            {
                AppObjectModel parent = await _appObjectRepository.GetByIdAsync(request.ParentId.Value);
                if (parent is null)
                    throw new NotFoundException(AppObjectErrorMessages.PARENT_OBJECT_NOT_FOUND);
            }
        }
    }
}
