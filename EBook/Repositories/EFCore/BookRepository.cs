using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        private readonly ILogger<BookRepository> _logger;
     
        public BookRepository(RepositoryContext context, ILogger<BookRepository> logger) : base(context, logger)
        {
            _logger = logger;   
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync(bool trackChanges) =>
            await FindAll(trackChanges)
                .OrderBy(b => b.Id)
                .ToListAsync();

        public async Task<Book> GetOneBookByIdAsync(int id, bool trackChanges)
        {
            var book = await FindByCondition(b => b.Id.Equals(id), trackChanges)
                .SingleOrDefaultAsync();

            if (book is null)
            {
                _logger.LogWarning($"{id} numaralı kitap bulunamadı.");
                return null; 
            }

            return book;

        }

        public async Task CreateOneBookAsync(Book book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book), "Book object is null");

            await _context.Set<Book>().AddAsync(book);
            await _context.SaveChangesAsync();

           
        }

        public async Task UpdateOneBookAsync(Book book)
        {
            if (book is null)
            {
                _logger.LogWarning($"Kitap bulunamadı.");
                return;
            }

            await UpdateAsync(book);
            await SaveAsync();
        }

        public async Task DeleteOneBookAsync(int id, Book book)
        {
            if (book is null)
            {
                _logger.LogWarning($"{id} numaralı kitap bulunamadı.");
                return;
            }
            _context.Set<Book>().Remove(book);
            await SaveAsync();     
        }       
    
    
    }
}
