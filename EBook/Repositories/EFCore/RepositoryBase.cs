using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repositories.Contracts;
using System.Linq.Expressions;

namespace Repositories.EFCore
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T>
        where T : class
    {
        protected readonly RepositoryContext _context;
        private readonly ILogger<BookRepository> _logger;

        public RepositoryBase(RepositoryContext context, ILogger<BookRepository> logger)
        {
            _context = context;
            _logger = logger;

        }

        public IQueryable<T> FindAll(bool trackChanges) =>
           !trackChanges ?
            _context.Set<T>().AsNoTracking() :
            _context.Set<T>();


        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression,
            bool trackChanges) =>
            !trackChanges ?
            _context.Set<T>().Where(expression).AsNoTracking() :
            _context.Set<T>().Where(expression);


        public async Task CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity); 
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync(); 
        }


        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity); 
            await _context.SaveChangesAsync();      
        }


        public async Task SaveAsync() => await _context.SaveChangesAsync();

    }
}
