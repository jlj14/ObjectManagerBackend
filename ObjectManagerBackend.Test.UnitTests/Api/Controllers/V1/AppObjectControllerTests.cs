using Microsoft.AspNetCore.Mvc;
using ObjectManagerBackend.API.Controllers.V1;
using ObjectManagerBackend.Application.DTOs.AppObject.Request;
using ObjectManagerBackend.Application.DTOs.AppObject.Response;
using ObjectManagerBackend.Application.DTOs.PaginatedResponse;
using ObjectManagerBackend.Application.Services.AppObject;

namespace ObjectManagerBackend.Test.UnitTests.Api.Controllers.V1
{
    public class AppObjectControllerTests
    {
        [Theory, AutoMoqData]
        public async Task GetAsync_WorksProperly_ShouldReturnOk(
            [Frozen] Mock<IAppObjectService> appObjectService,
            IEnumerable<AppObjectResponse> response,
            AppObjectController sut)
        {
            //Arrange
            appObjectService
                .Setup(x => x.GetAllObjectsAsync())
                .ReturnsAsync(response);

            //Act
            var result = await sut.GetAsync();

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            ((ObjectResult)result).Value.Should().Be(response);
        }

        [Theory, AutoMoqData]
        public async Task GetPaginatedAsync_InvalidParams_ShouldReturnBadRequest(
            AppObjectController sut)
        {
            //Arrange
            //Act
            var result = await sut.GetRootObjectsPaginatedAsync(0, 0);

            //Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        [Theory, AutoMoqData]
        public async Task GetPaginatedAsync_ValidParams_ShouldReturnOk(
            [Frozen] Mock<IAppObjectService> appObjectService,
            PaginatedResponse<AppObjectResponse> response,
            AppObjectController sut)
        {
            //Arrange
            appObjectService
                .Setup(x => x.GetRootObjectsPaginatedAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(response);

            //Act
            var result = await sut.GetRootObjectsPaginatedAsync(1, 1);

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            ((ObjectResult)result).Value.Should().Be(response);
        }

        [Theory, AutoMoqData]
        public async Task GetByIdAsync_InvalidParams_ShouldReturnBadRequest(
            AppObjectController sut)
        {
            //Arrange
            //Act
            var result = await sut.GetByIdAsync(0);

            //Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        [Theory, AutoMoqData]
        public async Task GetByIdAsync_ServiceReturnNull_ShouldReturnNotFound(
            [Frozen] Mock<IAppObjectService> appObjectService,
            AppObjectController sut)
        {
            //Arrange
            appObjectService
                .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((AppObjectResponse)null);

            //Act
            var result = await sut.GetByIdAsync(1);

            //Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Theory, AutoMoqData]
        public async Task GetByIdAsync_ServiceReturnValue_ShouldReturnOk(
            [Frozen] Mock<IAppObjectService> appObjectService,
            AppObjectResponse appObject,
            AppObjectController sut)
        {
            //Arrange
            appObjectService
                .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(appObject);

            //Act
            var result = await sut.GetByIdAsync(1);

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            ((ObjectResult)result).Value.Should().Be(appObject);
        }

        [Theory, AutoMoqData]
        public async Task GetChildreByIdAsync_InvalidParams_ShouldReturnBadRequest(
            AppObjectController sut)
        {
            //Arrange
            //Act
            var result = await sut.GetChildreByIdAsync(0);

            //Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        [Theory, AutoMoqData]
        public async Task GetChildreByIdAsync_ValidParams_ShouldReturnOk(
            [Frozen] Mock<IAppObjectService> appObjectService,
            IEnumerable<AppObjectResponse> appObjects,
            AppObjectController sut)
        {
            //Arrange
            appObjectService
                .Setup(x => x.GetChildObjectsAsync(It.IsAny<int>()))
                .ReturnsAsync(appObjects);

            //Act
            var result = await sut.GetChildreByIdAsync(1);

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            ((ObjectResult)result).Value.Should().Be(appObjects);
        }

        [Theory, AutoMoqData]
        public async Task CreateAsync_InvalidParams_ShouldReturnBadRequest(
            AppObjectController sut)
        {
            //Arrange
            //Act
            var result = await sut.CreateAsync((AppObjectCreateRequest)null);

            //Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        [Theory, AutoMoqData]
        public async Task CreateAsync_ValidParams_ShouldReturnOk(
            [Frozen] Mock<IAppObjectService> appObjectService,
            AppObjectCreateRequest request,
            AppObjectResponse appObject,
            AppObjectController sut)
        {
            //Arrange
            appObjectService
                .Setup(x => x.CreateAsync(It.IsAny<AppObjectCreateRequest>()))
                .ReturnsAsync(appObject);

            //Act
            var result = await sut.CreateAsync(request);

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            ((ObjectResult)result).Value.Should().Be(appObject);
        }

        [Theory, AutoMoqData]
        public async Task UpdateAsync_InvalidId_ShouldReturnBadRequest(
            AppObjectUpdateRequest request,
            AppObjectController sut)
        {
            //Arrange
            //Act
            var result = await sut.UpdateAsync(0, request);

            //Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        [Theory, AutoMoqData]
        public async Task UpdateAsync_InvalidRequest_ShouldReturnBadRequest(
            AppObjectController sut)
        {
            //Arrange
            //Act
            var result = await sut.UpdateAsync(1, (AppObjectUpdateRequest)null);

            //Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        [Theory, AutoMoqData]
        public async Task UpdateAsync_ValidRequest_ShouldReturnNoContent(
            AppObjectUpdateRequest request,
            AppObjectController sut)
        {
            //Arrange
            //Act
            var result = await sut.UpdateAsync(1, request);

            //Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Theory, AutoMoqData]
        public async Task DeleteAsync_InvalidRequest_ShouldReturnBadRequest(
            AppObjectController sut)
        {
            //Arrange
            //Act
            var result = await sut.DeleteAsync(0);

            //Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        [Theory, AutoMoqData]
        public async Task DeleteAsync_ValidRequest_ShouldReturnNoContent(
            AppObjectController sut)
        {
            //Arrange
            //Act
            var result = await sut.DeleteAsync(1);

            //Assert
            result.Should().BeOfType<NoContentResult>();
        }
    }
}
