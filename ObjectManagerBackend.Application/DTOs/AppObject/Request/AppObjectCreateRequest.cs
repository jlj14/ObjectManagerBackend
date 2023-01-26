using System.ComponentModel.DataAnnotations;

namespace ObjectManagerBackend.Application.DTOs.AppObject.Request
{
    /// <summary>
    /// AppObject create request
    /// </summary>
    public class AppObjectCreateRequest
    {
        /// <summary>
        /// Object name
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Object description
        /// </summary>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Object type
        /// </summary>
        [Required]
        public string Type { get; set; }

        /// <summary>
        /// Parent object identifier
        /// </summary>
        /// <remarks>
        /// The object can be associated to a parent object but it's not necessary
        /// </remarks>
        [Range(1, int.MaxValue)]
        public int? ParentId { get; set; }
    }
}
