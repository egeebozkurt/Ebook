using Microsoft.Extensions.Logging;
using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _context;
        private readonly ILogger<BookRepository> _logger;


        public RepositoryManager(RepositoryContext context, ILogger<BookRepository> logger)
        {
            _context = context;
            _logger = logger;
        }


        public IBookRepository Book => new BookRepository(_context, _logger);


        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
