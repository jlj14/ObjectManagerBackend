using FluentValidation;
using ObjectManagerBackend.Application.DTOs.AppObject.Request;
using ObjectManagerBackend.Domain.Constants;

namespace ObjectManagerBackend.Application.Validators.AppObject
{
    /// <summary>
    /// AppObject update request valdiator
    /// </summary>
    public class AppObjectUpdateRequestValidator : AbstractValidator<AppObjectUpdateRequest>
    {
        /// <summary>
        /// Constructor: Creates a new instance of <see cref="AppObjectCreateRequestValidator"/> and validate the <see cref="AppObjectUpdateRequest"/>
        /// </summary>
        public AppObjectUpdateRequestValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage(AppObjectErrorMessages.INVALID_OBJECT_ID);
            RuleFor(x => x.Name).NotEmpty().WithMessage(AppObjectErrorMessages.OBJECT_NAME_IS_REQUIRED);
            RuleFor(x => x.Description).NotEmpty().WithMessage(AppObjectErrorMessages.OBJECT_DESCRIPTION_IS_REQUIRED);
            RuleFor(x => x.Type).NotEmpty().WithMessage(AppObjectErrorMessages.OBJECT_TYPE_IS_REQUIRED);
        }
    }
}
