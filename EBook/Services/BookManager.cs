using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class BookManager : IBookService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        public BookManager(IRepositoryManager manager, ILoggerService logger)
        {
            _manager = manager;
            _logger = logger;
        }


        public async Task<IEnumerable<Book>> GetAllBooksAsync(bool trackChanges)
        {
            var books = await _manager.Book.GetAllBooksAsync(trackChanges);
            return books;
        }


        public async Task<Book?> GetOneBookByIdAsync(int id, bool trackChanges)
        {
            var book = await _manager.Book.GetOneBookByIdAsync(id, trackChanges);

            if (book is null)
            {
                _logger.LogWarning($"{id} numaralı kitap bulunamadı.");
                return null;
            }

            return book;
        }

        public async Task CreateOneBookAsync(Book book)
        {
            if (book is null)
            {
                _logger.LogWarning($"Hatalı değer girildi.");
                return;
            }

            book.Title = string.IsNullOrWhiteSpace(book.Title) ? "Default Title" : book.Title;

            await _manager.Book.CreateAsync(book);
            await SaveAsync();
        }

        public async Task UpdateOneBookAsync(int id, Book book, bool trackChanges)
        {
            var entity = await _manager.Book.GetOneBookByIdAsync(id, trackChanges);

            if (entity == null)
            {
                _logger.LogError($"{id} numaralı kitap bulunamadı.");
                return; 
            }

            entity.Title = book.Title;

            await _manager.Book.UpdateAsync(entity);
            await SaveAsync();
        }


        public async Task DeleteOneBookAsync(int id, bool trackChanges)
        {
            var entity = await _manager.Book.GetOneBookByIdAsync(id, trackChanges);

            if (entity == null)
            {
                _logger.LogError($"{id} numaralı kitap bulunamadı.");
                return;
            }


            await _manager.Book.DeleteAsync(entity);
            await SaveAsync();
        }


        public async Task SaveAsync() 
        {
            await _manager.SaveAsync(); 
        }
   
    }
}
