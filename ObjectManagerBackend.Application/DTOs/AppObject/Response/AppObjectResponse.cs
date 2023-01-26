namespace ObjectManagerBackend.Application.DTOs.AppObject.Response
{
    /// <summary>
    /// AppObject response DTO
    /// </summary>
    public class AppObjectResponse
    {
        /// <summary>
        /// Object identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Object name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Object description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Object type
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Parent object identifier
        /// </summary>
        /// <remarks>
        /// The object can be associated to a parent object but it's not necessary
        /// </remarks>
        public int? ParentId { get; set; }
    }
}
