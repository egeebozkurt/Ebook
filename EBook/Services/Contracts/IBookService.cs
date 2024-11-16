using Entities.Models;

namespace Services.Contracts
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllBooksAsync(bool trackChanges);
        Task<Book?> GetOneBookByIdAsync(int id, bool trackChanges);
        Task DeleteOneBookAsync(int id, bool trackChanges);
        Task UpdateOneBookAsync(int id, Book book, bool trackChanges);
        Task CreateOneBookAsync(Book book);
        Task SaveAsync(); 
    }
}

