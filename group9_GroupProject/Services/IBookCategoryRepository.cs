

using group9_GroupProject.Models;

namespace group9_GroupProject.Services
{
    public interface IBookCategoryRepository
    {

        // GET
        Task<IEnumerable<BookCategory>> GetAllCategoriesAsync();
        Task<BookCategory?> GetCategoryByIdAsync(int categoryId);

        // POST
        Task<BookCategory> AddBookCategoryAsync(BookCategory bookCategory);

        // PUT (full update)
        Task<BookCategory?> UpdateBookCategoryAsync(int categoryId, BookCategory bookCategory);

        // PATCH (partial update)
        Task<BookCategory?> PatchBookCategoryAsync(int categoryId, Dictionary<string, object> updates);

        // DELETE
        Task<bool> DeleteBookCategoryAsync(int categoryId);

    }
}
