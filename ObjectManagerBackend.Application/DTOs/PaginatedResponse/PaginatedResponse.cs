namespace ObjectManagerBackend.Application.DTOs.PaginatedResponse
{
    /// <summary>
    /// Generic paginated response
    /// </summary>
    /// <typeparam name="T">Generic type</typeparam>
    public class PaginatedResponse<T> where T : class
    {
        /// <summary>
        /// Current page number
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Page size
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Total number of elements
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Total number of pages
        /// </summary>
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);

        /// <summary>
        /// Data for the current page
        /// </summary>
        public IEnumerable<T> Data { get; set; }
    }
}
