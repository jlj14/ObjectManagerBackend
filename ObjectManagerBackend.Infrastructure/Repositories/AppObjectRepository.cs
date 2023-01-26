using ObjectManagerBackend.Domain.Contracts.Repositories;
using ObjectManagerBackend.Domain.Models.AppObject;
using ObjectManagerBackend.Infrastructure.Persistence;

namespace ObjectManagerBackend.Infrastructure.Repositories
{
    /// <summary>
    /// Appobject repository implementation
    /// </summary>
    public class AppObjectRepository : IAppObjectRepository
    {
        private readonly InMemoryContext _context;
        private readonly object _contextWriteLock = new object(); // To lock the write operations in order to do operations atomically
        private int _id; // Represents an auto incremental int like in databases

        /// <summary>
        /// Constructor: Creates a new instance of <see cref="AppObjectRepository"/>
        /// </summary>
        public AppObjectRepository(InMemoryContext context)
        {
            _context = context;
            _id = context.AppObjects.Count + 1;
        }

        /// <inheritdoc/>
        public Task<IEnumerable<AppObjectModel>> GetAllObjectsAsync()
        {
            return Task.FromResult(_context.AppObjects.Values.AsEnumerable());
        }

        /// <inheritdoc/>
        public Task<IEnumerable<AppObjectModel>> GetRootObjectsAsync(int? skip = null, int? take = null)
        {
            var query = _context.AppObjects.Values.Where(x => x.ParentId is null);
            
            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return Task.FromResult(query);
        }

        /// <inheritdoc/>
        public Task<int> GetRootObjectsTotalCountAsync()
        {
            return Task.FromResult(_context.AppObjects.Values.Count(x => x.ParentId is null));
        }

        /// <inheritdoc/>
        public Task<IEnumerable<AppObjectModel>> GetChildObjectsAsync(int parentId)
        {
            return Task.FromResult(_context.AppObjects.Values.Where(x => x.ParentId == parentId));
        }

        /// <inheritdoc/>
        public Task<AppObjectModel> GetByIdAsync(int id)
        {
            if (!_context.AppObjects.ContainsKey(id))
                return Task.FromResult((AppObjectModel)null);

            return Task.FromResult(_context.AppObjects[id]);
        }

        /// <inheritdoc/>
        public Task<int> CreateAsync(AppObjectModel model)
        {
            lock (_contextWriteLock)
            {
                model.Id = _id++;
                _context.AppObjects.Add(model.Id, model);
                return Task.FromResult(model.Id); 
            }
        }

        /// <inheritdoc/>
        public Task UpdateAsync(AppObjectModel model)
        {
            _context.AppObjects[model.Id] = model;
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task DeleteAsync(int id)
        {
            _context.AppObjects.Remove(id);
            return Task.CompletedTask;
        }
    }
}
