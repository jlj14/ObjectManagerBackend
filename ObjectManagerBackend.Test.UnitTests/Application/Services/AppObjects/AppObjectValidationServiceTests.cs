using ObjectManagerBackend.Application.DTOs.AppObject.Request;
using ObjectManagerBackend.Application.Services.AppObject;
using ObjectManagerBackend.Domain.Constants;
using ObjectManagerBackend.Domain.Contracts.Repositories;
using ObjectManagerBackend.Domain.Exceptions;
using ObjectManagerBackend.Domain.Models.AppObject;

namespace ObjectManagerBackend.Test.UnitTests.Application.Services.AppObjects
{
    public class AppObjectValidationServiceTests
    {
        private readonly Fixture _fixture = new Fixture();

        [Theory, AutoMoqData]
        public void AppObjectCreateRequest_ValidationErrors_ShouldThrowException(
            [Frozen] Mock<IValidator<AppObjectCreateRequest>> createRequestValidator,
            IEnumerable<ValidationFailure> validationFailures,
            AppObjectValidationService sut)
        {
            // Arrange
            createRequestValidator
                .Setup(x => x.Validate(It.IsAny<AppObjectCreateRequest>()))
                .Returns(new ValidationResult(validationFailures));

            string expectedErrorMessage = string.Join("; ", validationFailures.Select(x => x.ErrorMessage));

            // Act
            var func = async () => await sut.Validate(It.IsAny<AppObjectCreateRequest>());

            // Assert
            func.Should().ThrowAsync<BadRequestException>().WithMessage(expectedErrorMessage);
        }

        [Theory, AutoMoqData]
        public void AppObjectCreateRequest_ParentObjectNotFound_ShouldThrowException(
            [Frozen] Mock<IAppObjectRepository> appObjectRepository,
            AppObjectValidationService sut)
        {
            // Arrange
            appObjectRepository
                .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((AppObjectModel)null);

            // Act
            var func = async () => await sut.Validate(It.IsAny<AppObjectCreateRequest>());

            // Assert
            func.Should().ThrowAsync<NotFoundException>().WithMessage(AppObjectErrorMessages.PARENT_OBJECT_NOT_FOUND);
        }

        [Theory, AutoMoqData]
        public void AppObjectCreateRequest_ParentObjectNotFoundButParentIdIsNull_ShouldNotThrowException(
            [Frozen] Mock<IAppObjectRepository> appObjectRepository,
            AppObjectValidationService sut)
        {
            // Arrange
            appObjectRepository
                .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((AppObjectModel)null);

            AppObjectCreateRequest request = _fixture.Build<AppObjectCreateRequest>()
                .Without(x => x.ParentId)
                .Create();

            // Act
            var func = async () => await sut.Validate(request);

            // Assert
            func.Should().NotThrowAsync();
        }

        [Theory, AutoMoqData]
        public void AppObjectCreateRequest_ParentObjectFound_ShouldNotThrowException(
            [Frozen] Mock<IAppObjectRepository> appObjectRepository,
            AppObjectModel appObject,
            AppObjectValidationService sut)
        {
            // Arrange
            appObjectRepository
                .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(appObject);

            // Act
            var func = async () => await sut.Validate(It.IsAny<AppObjectCreateRequest>());

            // Assert
            func.Should().NotThrowAsync();
        }

        [Theory, AutoMoqData]
        public void AppObjectUpdateRequest_ValidationErrors_ShouldThrowException(
            [Frozen] Mock<IValidator<AppObjectUpdateRequest>> updateRequestValidator,
            IEnumerable<ValidationFailure> validationFailures,
            AppObjectValidationService sut)
        {
            // Arrange
            updateRequestValidator
                .Setup(x => x.Validate(It.IsAny<AppObjectUpdateRequest>()))
                .Returns(new ValidationResult(validationFailures));

            string expectedErrorMessage = string.Join("; ", validationFailures.Select(x => x.ErrorMessage));

            // Act
            var func = async () => await sut.Validate(It.IsAny<AppObjectUpdateRequest>());

            // Assert
            func.Should().ThrowAsync<BadRequestException>().WithMessage(expectedErrorMessage);
        }

        [Theory, AutoMoqData]
        public void AppObjectUpdateRequest_ParentObjectNotFound_ShouldThrowException(
            [Frozen] Mock<IAppObjectRepository> appObjectRepository,
            AppObjectValidationService sut)
        {
            // Arrange
            appObjectRepository
                .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((AppObjectModel)null);

            // Act
            var func = async () => await sut.Validate(It.IsAny<AppObjectUpdateRequest>());

            // Assert
            func.Should().ThrowAsync<NotFoundException>().WithMessage(AppObjectErrorMessages.PARENT_OBJECT_NOT_FOUND);
        }

        [Theory, AutoMoqData]
        public void AppObjectUpdateRequest_ParentObjectNotFoundButParentIdIsNull_ShouldNotThrowException(
            [Frozen] Mock<IAppObjectRepository> appObjectRepository,
            AppObjectValidationService sut)
        {
            // Arrange
            appObjectRepository
                .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((AppObjectModel)null);

            AppObjectUpdateRequest request = _fixture.Build<AppObjectUpdateRequest>()
                .Without(x => x.ParentId)
                .Create();

            // Act
            var func = async () => await sut.Validate(request);

            // Assert
            func.Should().NotThrowAsync();
        }

        [Theory, AutoMoqData]
        public void AppObjectUpdateRequest_ParentObjectFound_ShouldNotThrowException(
            [Frozen] Mock<IAppObjectRepository> appObjectRepository,
            AppObjectModel appObject,
            AppObjectValidationService sut)
        {
            // Arrange
            appObjectRepository
                .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(appObject);

            // Act
            var func = async () => await sut.Validate(It.IsAny<AppObjectUpdateRequest>());

            // Assert
            func.Should().NotThrowAsync();
        }
    }
}
