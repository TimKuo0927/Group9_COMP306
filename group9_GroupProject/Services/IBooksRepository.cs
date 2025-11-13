

using group9_GroupProject.Models;

namespace group9_GroupProject.Services
{
    public interface IBooksRepository
    {
        Task<bool> BookExistsAsync(string isbn);
        Task<bool> BookExistsAsync(int bookId);

        // GET
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book?> GetBookByIdAsync(int bookId);

        // POST
        Task<Book> AddBookAsync(Book book);

        // PUT (full update)
        Task<Book?> UpdateBookAsync(int bookId, Book book);

        // PATCH (partial update)
        Task<Book?> PatchBookAsync(int bookId, Dictionary<string, object> updates);

        // DELETE
        Task<bool> DeleteBookAsync(int bookId);


    }
}
