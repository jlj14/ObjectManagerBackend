using AutoMapper;
using ObjectManagerBackend.Application.DTOs.AppObject.Request;
using ObjectManagerBackend.Application.DTOs.AppObject.Response;
using ObjectManagerBackend.Application.DTOs.PaginatedResponse;
using ObjectManagerBackend.Application.Services.Abstract;
using ObjectManagerBackend.Domain.Constants;
using ObjectManagerBackend.Domain.Contracts.Repositories;
using ObjectManagerBackend.Domain.Exceptions;
using ObjectManagerBackend.Domain.Models.AppObject;

namespace ObjectManagerBackend.Application.Services.AppObject
{
    /// <summary>
    /// AppObject service implementation
    /// </summary>
    public class AppObjectService : AppServiceBase, IAppObjectService
    {
        private readonly IMapper _mapper;
        private readonly IAppObjectRepository _appObjectRepository;
        private readonly IAppObjectValidationService _appObjectValidationService;

        /// <summary>
        /// Constructor: Creates a new instace of <see cref="AppObjectService"/>
        /// </summary>
        /// <param name="mapper">Mapper</param>
        /// <param name="appObjectRepository">AppObhect repository</param>
        /// <param name="appObjectValidationService">AppObject validation service</param>
        public AppObjectService(IMapper mapper, IAppObjectRepository appObjectRepository, IAppObjectValidationService appObjectValidationService)
        {
            _mapper = mapper;
            _appObjectRepository = appObjectRepository;
            _appObjectValidationService = appObjectValidationService;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<AppObjectResponse>> GetAllObjectsAsync()
        {
            IEnumerable<AppObjectModel> appObjects = await _appObjectRepository.GetAllObjectsAsync();
            return _mapper.Map<IEnumerable<AppObjectResponse>>(appObjects);
        }

        /// <inheritdoc/>
        public async Task<PaginatedResponse<AppObjectResponse>> GetRootObjectsPaginatedAsync(int? pageNumber, int? pageSize)
        {
            int? skip = CheckPaginationParams(pageNumber, pageSize);
            Task<IEnumerable<AppObjectModel>> getRootObjectsTask = _appObjectRepository.GetRootObjectsAsync(skip, pageSize);
            Task<int> getRootObjectsTotalCountTask = _appObjectRepository.GetRootObjectsTotalCountAsync();
            await Task.WhenAll(getRootObjectsTask, getRootObjectsTotalCountTask);

            IEnumerable<AppObjectModel> rootObjects = await getRootObjectsTask;
            int rootObjectsTotalCount = await getRootObjectsTotalCountTask;

            return new PaginatedResponse<AppObjectResponse>
            {
                PageNumber = pageNumber ?? 1,
                PageSize = pageSize ?? rootObjectsTotalCount,
                TotalCount = rootObjectsTotalCount,
                Data = _mapper.Map<IEnumerable<AppObjectResponse>>(rootObjects)
            };
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<AppObjectResponse>> GetChildObjectsAsync(int parentId)
        {
            IEnumerable<AppObjectModel> children = await _appObjectRepository.GetChildObjectsAsync(parentId);
            return _mapper.Map<IEnumerable<AppObjectResponse>>(children);
        }

        /// <inheritdoc/>
        public async Task<AppObjectResponse> GetByIdAsync(int id)
        {
            AppObjectModel appObject = await _appObjectRepository.GetByIdAsync(id);
            return _mapper.Map<AppObjectResponse>(appObject);
        }

        /// <inheritdoc/>
        public async Task<AppObjectResponse> CreateAsync(AppObjectCreateRequest request)
        {
            await _appObjectValidationService.Validate(request);
            AppObjectModel model = _mapper.Map<AppObjectModel>(request);
            model.Id = await _appObjectRepository.CreateAsync(model);
            return _mapper.Map<AppObjectResponse>(model);
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(AppObjectUpdateRequest request)
        {
            await _appObjectValidationService.Validate(request);

            AppObjectModel appObject = await _appObjectRepository.GetByIdAsync(request.Id);
            if (appObject is null)
                throw new NotFoundException(AppObjectErrorMessages.OBJECT_NOT_FOUND);

            AppObjectModel model = _mapper.Map(request, appObject);
            await _appObjectRepository.UpdateAsync(model);
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(int id)
        {
            AppObjectModel appObject = await _appObjectRepository.GetByIdAsync(id);
            if (appObject is null)
                throw new NotFoundException(AppObjectErrorMessages.OBJECT_NOT_FOUND);

            var children = await _appObjectRepository.GetChildObjectsAsync(appObject.Id);
            if (children != null && children.Any())
                throw new KeyConflictException(AppObjectErrorMessages.OBJECT_DELETE_CASCADE_NOT_ALLOWED);

            await _appObjectRepository.DeleteAsync(id);
        }
    }
}
