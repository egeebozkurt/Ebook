using Entities.Models;

namespace Repositories.Contracts
{
    public interface IBookRepository : IRepositoryBase<Book>
    {
        Task<IEnumerable<Book>> GetAllBooksAsync(bool trackChanges);
        Task<Book> GetOneBookByIdAsync(int id, bool trackChanges);
        Task CreateOneBookAsync(Book book);
        Task UpdateOneBookAsync(Book book); 
        Task DeleteOneBookAsync(int id, Book book); 
    }
}


