
using group9_GroupProject.Models;
using Microsoft.EntityFrameworkCore;

namespace group9_GroupProject.Services
{
    public class BooksRepository : IBooksRepository
    {
        private LmsContext _db;
        private readonly HashSet<string> allowedProperties = new HashSet<string>
        {
            "Isbn",
            "BookTitle",
            "Author",
            "CategoryId",
            "Publisher",
            "Quantity"
        };

        public BooksRepository(LmsContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }
        public async Task<Book> AddBookAsync(Book book)
        {
            book.CreateDate = DateTime.Now;
            book.UpdateDate = DateTime.Now;
            book.IsDelete = false;
            _db.Books.Add(book);
            await _db.SaveChangesAsync();
            return book;
        }

        public async Task<bool> BookExistsAsync(string isbn)
        {
            return await _db.Books.Where(b => b.Isbn == isbn).FirstOrDefaultAsync() != null;
        }

        public async Task<bool> BookExistsAsync(int bookId)
        {
            return await _db.Books.Where(b => b.BookId == bookId).FirstOrDefaultAsync() != null;
        }

        public async Task<bool> DeleteBookAsync(int bookId)
        {
            var book = await _db.Books.Where(b => b.BookId == bookId).FirstOrDefaultAsync();
            if (book == null)
            {
                return false;
            }
            _db.Books.Remove(book);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _db.Books.OrderBy(x => x.CreateDate).ToListAsync();
        }

        public async Task<Book?> GetBookByIdAsync(int bookId)
        {
            return await _db.Books.Where(b => b.BookId == bookId).FirstOrDefaultAsync();
        }

        public async Task<Book?> PatchBookAsync(int bookId, Dictionary<string, object> updates)
        {
            var book = await _db.Books.FirstOrDefaultAsync(b => b.BookId == bookId);
            if (book == null)
            {
                return null;
            }

            // Define whitelist of properties that can be updated


            // Use reflection to apply valid updates only
            var bookType = typeof(Book);
            foreach (var update in updates)
            {
                if (!allowedProperties.Contains(update.Key))
                {
                    continue; // skip disallowed fields
                }


                var prop = bookType.GetProperty(update.Key);
                if (prop != null && prop.CanWrite)
                {
                    var newValue = Convert.ChangeType(update.Value, prop.PropertyType);
                    prop.SetValue(book, newValue);
                }
            }
            book.UpdateDate = DateTime.Now;
            book.IsDelete = false;
            await _db.SaveChangesAsync();
            return book;
        }




        public async Task<Book?> UpdateBookAsync(int bookId,Book book)
        {
            var bookToUpdate = await _db.Books.Where(b => b.BookId == bookId).FirstOrDefaultAsync();
            if (bookToUpdate == null)
            {
                return null;
            }
            bookToUpdate.UpdateDate = DateTime.Now;
            bookToUpdate.IsDelete = false;
            bookToUpdate.Publisher = book.Publisher;
            bookToUpdate.Author = book.Author;
            bookToUpdate.BookTitle = book.BookTitle;
            bookToUpdate.Isbn = book.Isbn;
            bookToUpdate.CategoryId = book.CategoryId;
            bookToUpdate.Quantity = book.Quantity;

            _db.Books.Update(bookToUpdate);
            await _db.SaveChangesAsync();
            return book;
        }
    }
}
