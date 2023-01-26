using FluentValidation;
using ObjectManagerBackend.Application.DTOs.AppObject.Request;
using ObjectManagerBackend.Domain.Constants;

namespace ObjectManagerBackend.Application.Validators.AppObject
{
    /// <summary>
    /// AppObject create request valdiator
    /// </summary>
    public class AppObjectCreateRequestValidator : AbstractValidator<AppObjectCreateRequest>
    {
        /// <summary>
        /// Constructor: Creates a new instance of <see cref="AppObjectCreateRequestValidator"/> and validate the <see cref="AppObjectCreateRequest"/>
        /// </summary>
        public AppObjectCreateRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(AppObjectErrorMessages.OBJECT_NAME_IS_REQUIRED);
            RuleFor(x => x.Description).NotEmpty().WithMessage(AppObjectErrorMessages.OBJECT_DESCRIPTION_IS_REQUIRED);
            RuleFor(x => x.Type).NotEmpty().WithMessage(AppObjectErrorMessages.OBJECT_TYPE_IS_REQUIRED);
        }
    }
}
