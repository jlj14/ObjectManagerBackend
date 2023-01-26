using ObjectManagerBackend.Domain.Constants;
using ObjectManagerBackend.Domain.Exceptions;

namespace ObjectManagerBackend.Application.Services.Abstract
{
    /// <summary>
    /// App service base
    /// </summary>
    public abstract class AppServiceBase
    {
        /// <summary>
        /// Checks if the pagination params are valid and calculates the skip parameter
        /// </summary>
        /// <remarks>
        /// They are not mandatory but if one comes the other must also come and both of them have to be greater than 0</remarks>
        /// <param name="pageNumber">Page number</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>A nullable integer which represents the skip param</returns>
        /// <exception cref="BadRequestException">Bad request exception</exception>
        protected int? CheckPaginationParams(int? pageNumber, int? pageSize)
        {
            bool badRequest = (pageNumber.HasValue && !pageSize.HasValue) ||
                              (!pageNumber.HasValue && pageSize.HasValue) ||
                              (pageNumber.HasValue && pageNumber.Value <= 0) ||
                              (pageSize.HasValue && pageSize.Value <= 0);

            if (badRequest)
                throw new BadRequestException(CommonErrorMessages.INVALID_PAGINATION_PARAMS);

            int? skip = pageNumber.HasValue && pageSize.HasValue ? (pageNumber.Value - 1) * pageSize.Value : null;
            return skip;
        }
    }
}
