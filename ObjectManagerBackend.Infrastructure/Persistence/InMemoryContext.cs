using ObjectManagerBackend.Domain.Models.AppObject;

namespace ObjectManagerBackend.Infrastructure.Persistence
{
    /// <summary>
    /// Application in memory context
    /// </summary>
    public class InMemoryContext
    {
        /// <summary>
        /// Constructor: Creates a new instance of <see cref="InMemoryContext"/>
        /// </summary>
        public InMemoryContext()
        {
            // Initialize the dictionaries
            AppObjects = new Dictionary<int, AppObjectModel>();
        }

        /// <summary>
        /// AppObjects table
        /// </summary>
        public Dictionary<int, AppObjectModel> AppObjects { get; }
    }
}
