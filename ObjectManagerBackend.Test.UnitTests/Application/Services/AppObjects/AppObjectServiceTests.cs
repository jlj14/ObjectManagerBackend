using AutoMapper;
using ObjectManagerBackend.Application.DTOs.AppObject.Request;
using ObjectManagerBackend.Application.DTOs.AppObject.Response;
using ObjectManagerBackend.Application.Services.AppObject;
using ObjectManagerBackend.Domain.Constants;
using ObjectManagerBackend.Domain.Contracts.Repositories;
using ObjectManagerBackend.Domain.Exceptions;
using ObjectManagerBackend.Domain.Models.AppObject;

namespace ObjectManagerBackend.Test.UnitTests.Application.Services.AppObjects
{
    public class AppObjectServiceTests
    {
        [Theory, AutoMoqData]
        public async Task GetAllObjectsAsync_WorksProperly_ShouldReturnExpectedData(
            [Frozen] Mock<IAppObjectRepository> appObjectRepository,
            [Frozen] Mock<IMapper> mapper,
            IEnumerable<AppObjectModel> appObjects,
            IEnumerable<AppObjectResponse> expectedResponse,
            AppObjectService sut)
        {
            // Arrange
            appObjectRepository
                .Setup(x => x.GetAllObjectsAsync())
                .ReturnsAsync(appObjects);

            mapper
                .Setup(x => x.Map<IEnumerable<AppObjectResponse>>(appObjects))
                .Returns(expectedResponse);

            // Act
            var result = await sut.GetAllObjectsAsync();

            // Assert
            result.Should().BeEquivalentTo(expectedResponse);
        }

        [Theory]
        [InlineAutoMoqData(null, 1)]
        [InlineAutoMoqData(1, null)]
        [InlineAutoMoqData(1, 0)]
        [InlineAutoMoqData(0, 1)]
        public async Task GetRootObjectsPaginatedAsync_InvalidPaginationParams_ShouldThrowException(
            int? pageNumber,
            int? pageSize,
            AppObjectService sut)
        {
            // Arrange
            // Act
            var func = async () => await sut.GetRootObjectsPaginatedAsync(pageNumber, pageSize);

            // Assert
            func.Should().ThrowAsync<BadRequestException>().WithMessage(CommonErrorMessages.INVALID_PAGINATION_PARAMS);
        }

        [Theory, AutoMoqData]
        public async Task GetRootObjectsPaginatedAsync_WithoutPaginationParams_ShouldReturnExpectedData(
            [Frozen] Mock<IAppObjectRepository> appObjectRepository,
            [Frozen] Mock<IMapper> mapper,
            IEnumerable<AppObjectModel> appObjects,
            IEnumerable<AppObjectResponse> expectedResponse,
            AppObjectService sut)
        {
            // Arrange
            appObjectRepository
                .Setup(x => x.GetRootObjectsAsync(null, null))
                .ReturnsAsync(appObjects);

            appObjectRepository
                .Setup(x => x.GetRootObjectsTotalCountAsync())
                .ReturnsAsync(appObjects.Count());

            mapper
                .Setup(x => x.Map<IEnumerable<AppObjectResponse>>(appObjects))
                .Returns(expectedResponse);

            // Act
            var result = await sut.GetRootObjectsPaginatedAsync(null, null);

            // Assert
            result.PageNumber.Should().Be(1);
            result.PageSize.Should().Be(appObjects.Count());
            result.TotalCount.Should().Be(appObjects.Count());
            result.Data.Should().BeEquivalentTo(expectedResponse);
        }

        [Theory, AutoMoqData]
        public async Task GetRootObjectsPaginatedAsync_WithPaginationParams_ShouldReturnExpectedData(
            [Frozen] Mock<IAppObjectRepository> appObjectRepository,
            [Frozen] Mock<IMapper> mapper,
            int pageNumber, 
            int pageSize,
            IEnumerable<AppObjectModel> appObjects,
            IEnumerable<AppObjectResponse> expectedResponse,
            AppObjectService sut)
        {
            // Arrange
            appObjectRepository
                .Setup(x => x.GetRootObjectsAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(appObjects);

            appObjectRepository
                .Setup(x => x.GetRootObjectsTotalCountAsync())
                .ReturnsAsync(appObjects.Count());

            mapper
                .Setup(x => x.Map<IEnumerable<AppObjectResponse>>(appObjects))
                .Returns(expectedResponse);

            // Act
            var result = await sut.GetRootObjectsPaginatedAsync(pageNumber, pageSize);

            // Assert
            result.PageNumber.Should().Be(pageNumber);
            result.PageSize.Should().Be(pageSize);
            result.TotalCount.Should().Be(appObjects.Count());
            result.Data.Should().BeEquivalentTo(expectedResponse);
        }

        [Theory, AutoMoqData]
        public async Task GetChildObjectsAsync_WorksProperly_ShouldReturnExpectedData(
            [Frozen] Mock<IAppObjectRepository> appObjectRepository,
            [Frozen] Mock<IMapper> mapper,
            IEnumerable<AppObjectModel> appObjects,
            IEnumerable<AppObjectResponse> expectedResponse,
            AppObjectService sut)
        {
            // Arrange
            appObjectRepository
                .Setup(x => x.GetChildObjectsAsync(It.IsAny<int>()))
                .ReturnsAsync(appObjects);

            mapper
                .Setup(x => x.Map<IEnumerable<AppObjectResponse>>(appObjects))
                .Returns(expectedResponse);

            // Act
            var result = await sut.GetChildObjectsAsync(It.IsAny<int>());

            // Assert
            result.Should().BeEquivalentTo(expectedResponse);
        }

        [Theory, AutoMoqData]
        public async Task GetByIdAsync_WorksProperly_ShouldReturnExpectedData(
            [Frozen] Mock<IAppObjectRepository> appObjectRepository,
            [Frozen] Mock<IMapper> mapper,
            AppObjectModel appObject,
            AppObjectResponse expectedResponse,
            AppObjectService sut)
        {
            // Arrange
            appObjectRepository
                .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(appObject);

            mapper
                .Setup(x => x.Map<AppObjectResponse>(appObject))
                .Returns(expectedResponse);

            // Act
            var result = await sut.GetByIdAsync(It.IsAny<int>());

            // Assert
            result.Should().BeEquivalentTo(expectedResponse);
        }

        [Theory, AutoMoqData]
        public async Task CreateAsync_WorksProperly_ShouldReturnExpectedData(
            [Frozen] Mock<IAppObjectRepository> appObjectRepository,
            [Frozen] Mock<IMapper> mapper,
            AppObjectCreateRequest request,
            AppObjectModel appObject,
            AppObjectResponse expectedResponse,
            AppObjectService sut)
        {
            // Arrange
            mapper
                .Setup(x => x.Map<AppObjectModel>(request))
                .Returns(appObject);

            mapper
                .Setup(x => x.Map<AppObjectResponse>(appObject))
                .Returns(expectedResponse);

            // Act
            var result = await sut.CreateAsync(request);

            // Assert
            appObjectRepository.Verify(x => x.CreateAsync(appObject), Times.Once());
            result.Should().BeEquivalentTo(expectedResponse);
        }

        [Theory, AutoMoqData]
        public void UpdateAsync_RepositoryReturnsNull_ShouldThrowException(
            [Frozen] Mock<IAppObjectRepository> appObjectRepository,
            AppObjectUpdateRequest request,
            AppObjectService sut)
        {
            // Arrange
            appObjectRepository
                .Setup(x => x.GetByIdAsync(request.Id))
                .ReturnsAsync((AppObjectModel)null);

            // Act
            var func = async () => await sut.UpdateAsync(request);

            // Assert
            appObjectRepository.Verify(x => x.UpdateAsync(It.IsAny<AppObjectModel>()), Times.Never());
            func.Should().ThrowAsync<NotFoundException>().WithMessage(AppObjectErrorMessages.OBJECT_NOT_FOUND);
        }

        [Theory, AutoMoqData]
        public async Task UpdateAsync_WorksProperly_ShouldNotThrowException(
            [Frozen] Mock<IAppObjectRepository> appObjectRepository,
            [Frozen] Mock<IMapper> mapper,
            AppObjectUpdateRequest request,
            AppObjectModel appObject,
            AppObjectModel appObjectUpdated,
            AppObjectService sut)
        {
            // Arrange
            appObjectRepository
                .Setup(x => x.GetByIdAsync(request.Id))
                .ReturnsAsync(appObject);

            mapper
                .Setup(x => x.Map(request, appObject))
                .Returns(appObjectUpdated);

            // Act
            await sut.UpdateAsync(request);

            // Assert
            appObjectRepository.Verify(x => x.UpdateAsync(appObjectUpdated), Times.Once());
        }

        [Theory, AutoMoqData]
        public void DeleteAsync_RepositoryReturnsNull_ShouldThrowException(
            [Frozen] Mock<IAppObjectRepository> appObjectRepository,
            int objectId,
            AppObjectService sut)
        {
            // Arrange
            appObjectRepository
                .Setup(x => x.GetByIdAsync(objectId))
                .ReturnsAsync((AppObjectModel)null);

            // Act
            var func = async () => await sut.DeleteAsync(objectId);

            // Assert
            appObjectRepository.Verify(x => x.DeleteAsync(It.IsAny<int>()), Times.Never());
            func.Should().ThrowAsync<NotFoundException>().WithMessage(AppObjectErrorMessages.OBJECT_NOT_FOUND);
        }

        [Theory, AutoMoqData]
        public void DeleteAsync_RepositoryReturnsChildObjects_ShouldThrowException(
            [Frozen] Mock<IAppObjectRepository> appObjectRepository,
            int objectId,
            AppObjectModel appObject,
            IEnumerable<AppObjectModel> children,
            AppObjectService sut)
        {
            // Arrange
            appObjectRepository
                .Setup(x => x.GetByIdAsync(objectId))
                .ReturnsAsync(appObject);

            appObjectRepository
                .Setup(x => x.GetChildObjectsAsync(objectId))
                .ReturnsAsync(children);

            // Act
            var func = async () => await sut.DeleteAsync(objectId);

            // Assert
            appObjectRepository.Verify(x => x.DeleteAsync(It.IsAny<int>()), Times.Never());
            func.Should().ThrowAsync<NotFoundException>().WithMessage(AppObjectErrorMessages.OBJECT_DELETE_CASCADE_NOT_ALLOWED);
        }

        [Theory, AutoMoqData]
        public async Task DeleteAsync_WorksProperly_ShouldNotThrowException(
            [Frozen] Mock<IAppObjectRepository> appObjectRepository,
            int objectId,
            AppObjectModel appObject,
            AppObjectService sut)
        {
            // Arrange
            appObjectRepository
                .Setup(x => x.GetByIdAsync(objectId))
                .ReturnsAsync(appObject);

            appObjectRepository
                .Setup(x => x.GetChildObjectsAsync(objectId))
                .ReturnsAsync(Enumerable.Empty<AppObjectModel>());

            // Act
            await sut.DeleteAsync(objectId);

            // Assert
            appObjectRepository.Verify(x => x.DeleteAsync(objectId), Times.Once());
        }
    }
}
