
using group9_GroupProject.DTO;
using group9_GroupProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace group9_GroupProject.Services
{
    public class BookCategoryRepository : IBookCategoryRepository
    {
        private LmsContext _db;
        private readonly HashSet<string> allowedProperties = new HashSet<string>
        {
            "CategoryName",
            "Description"
        };

        public BookCategoryRepository(LmsContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }
        public async Task<BookCategory> AddBookCategoryAsync(BookCategory bookCategory)
        {
            bookCategory.CreateDate = DateTime.Now;
            bookCategory.UpdateDate = DateTime.Now;
            bookCategory.IsDelete = false;
            _db.BookCategories.Add(bookCategory);
            await _db.SaveChangesAsync();
            return bookCategory;
        }

        public async Task<bool> DeleteBookCategoryAsync(int categoryId)
        {
            var category = await _db.BookCategories.Where(x => x.CategoryId == categoryId).FirstOrDefaultAsync();
            if (category == null)
            {
                return false;
            }
            _db.BookCategories.Remove(category);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<BookCategory>> GetAllCategoriesAsync()
        {
            return await _db.BookCategories.OrderBy(x => x.CreateDate).ToListAsync();
        }

        public async Task<BookCategory?> GetCategoryByIdAsync(int categoryId)
        {
            return await _db.BookCategories.Where(x => x.CategoryId == categoryId).FirstOrDefaultAsync();
        }

        public async Task<BookCategory?> PatchBookCategoryAsync(int categoryId, Dictionary<string, object> updates)
        {
            var bookCategories = await _db.BookCategories.FirstOrDefaultAsync(b => b.CategoryId == categoryId);
            if (bookCategories == null)
            {
                return null;
            }

            // Define whitelist of properties that can be updated


            // Use reflection to apply valid updates only
            var bookType = typeof(BookCategory);
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
                    prop.SetValue(bookCategories, newValue);
                }
            }
            bookCategories.UpdateDate = DateTime.Now;
            bookCategories.IsDelete = false;
            await _db.SaveChangesAsync();
            return bookCategories;
        }

        public async Task<BookCategory?> UpdateBookCategoryAsync(int bookCategoryId,BookCategory bookCategory)
        {
            var categoryToUpdate = await _db.BookCategories.Where(b => b.CategoryId == bookCategoryId).FirstOrDefaultAsync();
            if (categoryToUpdate == null)
            {
                return null;
            }
            categoryToUpdate.UpdateDate = DateTime.Now;
            categoryToUpdate.IsDelete = false;
            categoryToUpdate.CategoryName = bookCategory.CategoryName;
            categoryToUpdate.Description = bookCategory.Description;

            _db.BookCategories.Update(categoryToUpdate);
            await _db.SaveChangesAsync();
            return categoryToUpdate;
        }
    }
}
