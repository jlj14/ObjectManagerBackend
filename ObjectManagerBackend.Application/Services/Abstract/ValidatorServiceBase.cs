using FluentValidation.Results;
using ObjectManagerBackend.Domain.Exceptions;

namespace ObjectManagerBackend.Application.Services.Abstract
{
    /// <summary>
    /// Validator service base
    /// </summary>
    public abstract class ValidatorServiceBase
    {
        /// <summary>
        /// Handles the validation result, throwing an exception if it's not valid
        /// </summary>
        /// <param name="validationResult">Validation result</param>
        /// <exception cref="BadRequestException">Bad request exception</exception>
        protected void HandleValidationResult(ValidationResult validationResult)
        {
            if (!validationResult.IsValid)
            {
                throw new BadRequestException(string.Join("; ", validationResult.Errors));
            }
        }
    }
}
