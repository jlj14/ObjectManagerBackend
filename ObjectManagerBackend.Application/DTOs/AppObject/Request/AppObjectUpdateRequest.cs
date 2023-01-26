using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ObjectManagerBackend.Application.DTOs.AppObject.Request
{
    /// <summary>
    /// AppObject update request
    /// </summary>
    public class AppObjectUpdateRequest : AppObjectCreateRequest
    {
        /// <summary>
        /// Object identifier
        /// </summary>
        /// <remarks>
        /// This is not serialized from json body request because it comes from the route itself
        /// </remarks>
        [Required]
        [JsonIgnore]
        public int Id { get; set; }
    }
}
