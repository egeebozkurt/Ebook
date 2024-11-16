using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        private readonly IServiceManager _manager;
        private readonly ILoggerService _logger;


        public BooksController(IServiceManager manager, ILoggerService logger)
        {
            _manager = manager;
            _logger = logger;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _manager.BookService.GetAllBooksAsync(false);
            
            return Ok(books);
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOneBookAsync([FromRoute(Name = "id")] int id)
        {
            var book = await _manager.BookService.GetOneBookByIdAsync(id, false);

            if (book is null)
            {
                _logger.LogWarning($"{id} numaralı kitap bulunamadı.");
                return NotFound($"{id} numaralı kitap bulunamadı.");
            }

            return Ok(book);
        }


        [HttpPost]
        public async Task<IActionResult> CreateOneBookAsync([FromBody] Book book)
        {

            if (book is null)
                throw new Exception();

            await _manager.BookService.CreateOneBookAsync(book);

            return Ok(book);
        }



        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOneBookAsync([FromRoute(Name = "id")] int id)
        {
            var book = await _manager.BookService.GetOneBookByIdAsync(id, false);

            if (book is null)
            {
                _logger.LogWarning($"{id} numaralı kitap bulunamadı.");
                return NotFound($"{id} numaralı kitap bulunamadı.");
            }

            await _manager.BookService.DeleteOneBookAsync(id, false);

            return Ok(book);
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateBookAsync(int id, [FromBody] Book book)
        {
            if (book is null)
            {
                _logger.LogWarning($"{id} numaralı kitap bulunamadı.");
                return NotFound($"{id} numaralı kitap bulunamadı.");
            }

            var existingBook = await _manager.BookService.GetOneBookByIdAsync(id, false);
            if (existingBook is null)
                throw new Exception();

            await _manager.BookService.UpdateOneBookAsync(id, book, true);

            return Ok(book);
        }
    }
}
