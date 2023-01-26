using Microsoft.AspNetCore.Mvc;
using ObjectManagerBackend.Application.DTOs.AppObject.Request;
using ObjectManagerBackend.Application.DTOs.AppObject.Response;
using ObjectManagerBackend.Application.DTOs.PaginatedResponse;
using ObjectManagerBackend.Application.Services.AppObject;

namespace ObjectManagerBackend.API.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class AppObjectController : ControllerBase
    {
        private readonly IAppObjectService _service;

        /// <summary>
        /// Constructor: Creates a new instance of <see cref="AppObjectController"/>
        /// </summary>
        /// <param name="service">AppObject application service</param>
        public AppObjectController(IAppObjectService service)
        {
            _service = service;
        }

        /// <summary>
        /// Gets all the objects
        /// </summary>
        /// <returns>A colection with the objects</returns>
        /// <response code="200">Operation completed successfully</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal server error</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AppObjectResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync()
        {
            IEnumerable<AppObjectResponse> objects = await _service.GetAllObjectsAsync();
            return Ok(objects);
        }

        /// <summary>
        /// Gets the root objects (objects without parent) paginated
        /// </summary>
        /// <param name="pageNumber">Page number; default value 1</param>
        /// <param name="pageSize">Page size; default value 10</param>
        /// <returns>Paginated response with the objects</returns>
        /// <response code="200">Operation completed successfully</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal server error</response>
        [HttpGet]
        [Route("roots")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedResponse<AppObjectResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRootObjectsPaginatedAsync([FromQuery] int? pageNumber, [FromQuery] int? pageSize)
        {
            if ((pageNumber.HasValue && pageNumber.Value <= 0) || (pageSize.HasValue && pageSize.Value <= 0))
                return BadRequest();

            PaginatedResponse<AppObjectResponse> rootObjects = await _service.GetRootObjectsPaginatedAsync(pageNumber, pageSize);
            return Ok(rootObjects);
        }

        /// <summary>
        /// Gets an object by id
        /// </summary>
        /// <param name="appObjectId">AppObject identifier</param>
        /// <returns>The object</returns>
        /// <response code="200">Operation completed successfully</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">AppObject not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet]
        [Route("{appObjectId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AppObjectResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int appObjectId)
        {
            if (appObjectId <= 0)
                return BadRequest();

            AppObjectResponse appObject = await _service.GetByIdAsync(appObjectId);
            if (appObject == null)
                return NotFound();

            return Ok(appObject);
        }

        /// <summary>
        /// Gets the child objects for a specified object
        /// </summary>
        /// <param name="appObjectId">AppObject identifier</param>
        /// <returns>The child objects</returns>
        /// <response code="200">Operation completed successfully</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal server error</response>
        [HttpGet]
        [Route("{appObjectId:int}/children")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AppObjectResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetChildreByIdAsync([FromRoute] int appObjectId)
        {
            if (appObjectId <= 0)
                return BadRequest();

            IEnumerable<AppObjectResponse> childObjects = await _service.GetChildObjectsAsync(appObjectId);
            return Ok(childObjects);
        }

        /// <summary>
        /// Creates a new object
        /// </summary>
        /// <param name="request">Object creation request</param>
        /// <returns>The id of the new object</returns>
        /// <response code="200">Operation completed successfully</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal server error</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AppObjectResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAsync([FromBody] AppObjectCreateRequest request)
        {
            if (request is null)
                return BadRequest();

            AppObjectResponse appObject = await _service.CreateAsync(request);
            return Ok(appObject);
        }

        /// <summary>
        /// Updates a new object
        /// </summary>
        /// <param name="appObjectId">Object identifier</param>
        /// <param name="request">Object update request</param>
        /// <returns>No content</returns>
        /// <response code="204">Operation completed successfully</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Object not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut]
        [Route("{appObjectId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAsync([FromRoute] int appObjectId, [FromBody] AppObjectUpdateRequest request)
        {
            if (appObjectId <= 0 || request is null)
                return BadRequest();

            request.Id = appObjectId;
            await _service.UpdateAsync(request);
            return NoContent();
        }

        /// <summary>
        /// Deletes an existing object
        /// </summary>
        /// <param name="appObjectId">Object identifier</param>
        /// <returns>No content</returns>
        /// <response code="204">Operation completed successfully</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Object not found</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete]
        [Route("{appObjectId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync([FromRoute] int appObjectId)
        {
            if (appObjectId <= 0)
                return BadRequest();

            await _service.DeleteAsync(appObjectId);
            return NoContent();
        }
    }
}
