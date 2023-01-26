using ObjectManagerBackend.Application.DTOs.AppObject.Request;
using ObjectManagerBackend.Application.DTOs.AppObject.Response;
using ObjectManagerBackend.Application.DTOs.PaginatedResponse;
using System.Net;

namespace ObjectManagerBackend.Test.IntegrationTests.Api.V1
{
    //[Collection(nameof(IntegrationTestDefinition))]
    public class AppObjectControllerTests : ApiTestBase, IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly Fixture _fixture = new Fixture();
        private const string BASE_URL = "/api/v1/AppObject";

        public AppObjectControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetRootsPaginated_WorksProperly_ShouldReturnTheExpectedData()
        {
            // Arrange
            var client = _factory.CreateClient();

            IEnumerable<AppObjectCreateRequest> postRequests = _fixture.Build<AppObjectCreateRequest>()
                .Without(x => x.ParentId)
                .CreateMany(5);

            // Act
            await Task.WhenAll(postRequests.Select(req => PostAndReadResponseAsync<AppObjectResponse, AppObjectCreateRequest>(client, BASE_URL, req)));
            var withoutPagination = await GetAndReadResponseAsync<PaginatedResponse<AppObjectResponse>>(client, $"{BASE_URL}/roots");
            var pageNumber_1_pageSize_5 = await GetAndReadResponseAsync<PaginatedResponse<AppObjectResponse>>(client, BASE_URL + "/roots?pageNumber=1&pageSize=5");
            var pageNumber_1_pageSize_2 = await GetAndReadResponseAsync<PaginatedResponse<AppObjectResponse>>(client, BASE_URL + "/roots?pageNumber=1&pageSize=2");
            var pageNumber_2_pageSize_2 = await GetAndReadResponseAsync<PaginatedResponse<AppObjectResponse>>(client, BASE_URL + "/roots?pageNumber=2&pageSize=2");
            var pageNumber_3_pageSize_2 = await GetAndReadResponseAsync<PaginatedResponse<AppObjectResponse>>(client, BASE_URL + "/roots?pageNumber=3&pageSize=2");

            // Assert
            withoutPagination.Data.Count().Should().Be(5);
            withoutPagination.PageNumber.Should().Be(1);
            withoutPagination.PageSize.Should().Be(5);
            withoutPagination.TotalPages.Should().Be(1);
            withoutPagination.TotalCount.Should().Be(5);

            pageNumber_1_pageSize_5.Data.Count().Should().Be(5);
            pageNumber_1_pageSize_5.PageNumber.Should().Be(1);
            pageNumber_1_pageSize_5.PageSize.Should().Be(5);
            pageNumber_1_pageSize_5.TotalPages.Should().Be(1);
            pageNumber_1_pageSize_5.TotalCount.Should().Be(5);

            pageNumber_1_pageSize_2.Data.Count().Should().Be(2);
            pageNumber_1_pageSize_2.PageNumber.Should().Be(1);
            pageNumber_1_pageSize_2.PageSize.Should().Be(2);
            pageNumber_1_pageSize_2.TotalPages.Should().Be(3);
            pageNumber_1_pageSize_2.TotalCount.Should().Be(5);

            pageNumber_2_pageSize_2.Data.Count().Should().Be(2);
            pageNumber_2_pageSize_2.PageNumber.Should().Be(2);
            pageNumber_2_pageSize_2.PageSize.Should().Be(2);
            pageNumber_2_pageSize_2.TotalPages.Should().Be(3);
            pageNumber_2_pageSize_2.TotalCount.Should().Be(5);

            pageNumber_3_pageSize_2.Data.Count().Should().Be(1);
            pageNumber_3_pageSize_2.PageNumber.Should().Be(3);
            pageNumber_3_pageSize_2.PageSize.Should().Be(2);
            pageNumber_3_pageSize_2.TotalPages.Should().Be(3);
            pageNumber_3_pageSize_2.TotalCount.Should().Be(5);
        }

        [Fact]
        public async Task PostAndPut_WorksProperly_ShouldReturnTheExpectedData()
        {
            // Arrange
            var client = _factory.CreateClient();

            AppObjectCreateRequest postRequest = _fixture.Build<AppObjectCreateRequest>()
                .Without(x => x.ParentId)
                .Create();

            AppObjectUpdateRequest putRequest = _fixture.Build<AppObjectUpdateRequest>()
                .Without(x => x.ParentId)
                .Create();

            // Act
            AppObjectResponse postResult = await PostAndReadResponseAsync<AppObjectResponse, AppObjectCreateRequest>(client, BASE_URL, postRequest);
            AppObjectResponse objectCreated = await GetAndReadResponseAsync<AppObjectResponse>(client, $"{BASE_URL}/1");
            await PutAsync<AppObjectUpdateRequest>(client, $"{BASE_URL}/1", putRequest);
            AppObjectResponse objectUpdated = await GetAndReadResponseAsync<AppObjectResponse>(client, $"{BASE_URL}/1");

            // Assert
            postResult.Id.Should().Be(1);
            postResult.ParentId.Should().BeNull();
            postResult.Name.Should().Be(postRequest.Name);
            postResult.Description.Should().Be(postRequest.Description);
            postResult.Type.Should().Be(postRequest.Type);

            postResult.Should().BeEquivalentTo(objectCreated);

            objectUpdated.Id.Should().Be(postResult.Id);
            objectUpdated.ParentId.Should().BeNull();
            objectUpdated.Name.Should().Be(putRequest.Name);
            objectUpdated.Description.Should().Be(putRequest.Description);
            objectUpdated.Type.Should().Be(putRequest.Type);
        }

        [Fact]
        public async Task Delete_WorksProperly_ShouldReturnTheExpectedData()
        {
            // Arrange
            var client = _factory.CreateClient();

            AppObjectCreateRequest postRequest = _fixture.Build<AppObjectCreateRequest>()
                .Without(x => x.ParentId)
                .Create();

            // Act
            AppObjectResponse postResult = await PostAndReadResponseAsync<AppObjectResponse, AppObjectCreateRequest>(client, BASE_URL, postRequest);
            AppObjectResponse objectCreated = await GetAndReadResponseAsync<AppObjectResponse>(client, $"{BASE_URL}/1");
            HttpStatusCode deleteStatusCode = await DeleteAsync(client, $"{BASE_URL}/1");
            HttpStatusCode getStatusCode = await GetAsync(client, $"{BASE_URL}/1");

            // Assert
            objectCreated.Should().NotBeNull();
            deleteStatusCode.Should().Be(HttpStatusCode.NoContent);
            getStatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task CheckingRelationships_WorksProperly_ShouldReturnTheExpectedData()
        {
            // Arrange
            var client = _factory.CreateClient();

            AppObjectCreateRequest parentPostRequest = _fixture.Build<AppObjectCreateRequest>()
                .Without(x => x.ParentId)
                .Create();

            AppObjectCreateRequest childPostRequest = _fixture.Build<AppObjectCreateRequest>()
                .With(x => x.ParentId, 1)
                .Create();

            AppObjectCreateRequest futureChildPostRequest = _fixture.Build<AppObjectCreateRequest>()
                .Without(x => x.ParentId)
                .Create();

            AppObjectUpdateRequest updateFutureChildPutRequest = _fixture.Build<AppObjectUpdateRequest>()
                .With(x => x.ParentId, 1)
                .Create();

            // Act
            await PostAndReadResponseAsync<AppObjectResponse, AppObjectCreateRequest>(client, BASE_URL, parentPostRequest);
            await PostAndReadResponseAsync<AppObjectResponse, AppObjectCreateRequest>(client, BASE_URL, childPostRequest);
            AppObjectResponse futureChildResult = await PostAndReadResponseAsync<AppObjectResponse, AppObjectCreateRequest>(client, BASE_URL, futureChildPostRequest);

            var rootObjectsBeforeUpdate = await GetAndReadResponseAsync<PaginatedResponse<AppObjectResponse>>(client, $"{BASE_URL}/roots");
            var childObjectsBeforeUpdate = await GetAndReadResponseAsync<IEnumerable<AppObjectResponse>>(client, $"{BASE_URL}/1/children");
            var allObjectsBeforeUpdate = await GetAndReadResponseAsync<IEnumerable<AppObjectResponse>>(client, BASE_URL);

            await PutAsync<AppObjectUpdateRequest>(client, $"{BASE_URL}/{futureChildResult.Id}", updateFutureChildPutRequest);
            var rootObjectsAfterUpdate = await GetAndReadResponseAsync<PaginatedResponse<AppObjectResponse>>(client, $"{BASE_URL}/roots");
            var childObjectsAfterUpdate = await GetAndReadResponseAsync<IEnumerable<AppObjectResponse>>(client, $"{BASE_URL}/1/children");
            var allObjectsAfterUpdate = await GetAndReadResponseAsync<IEnumerable<AppObjectResponse>>(client, BASE_URL);

            // Assert
            rootObjectsBeforeUpdate.Data.Count().Should().Be(2);
            childObjectsBeforeUpdate.Count().Should().Be(1);
            allObjectsBeforeUpdate.Count().Should().Be(3);

            rootObjectsAfterUpdate.Data.Count().Should().Be(1);
            childObjectsAfterUpdate.Count().Should().Be(2);
            allObjectsAfterUpdate.Count().Should().Be(3);
        }
    }
}
