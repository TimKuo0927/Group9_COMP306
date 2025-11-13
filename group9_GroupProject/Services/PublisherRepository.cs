using group9_GroupProject.Models;
using Microsoft.EntityFrameworkCore;

namespace group9_GroupProject.Services
{
    public class PublisherRepository : IPublisherRepository
    {
        private LmsContext _db;
        private readonly HashSet<string> allowedProperties = new HashSet<string>
        {
            "PublisherName"
        };

        public PublisherRepository(LmsContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }
        public async Task<Publisher> AddPublisherAsync(Publisher publisher)
        {
            publisher.CreateDate = DateTime.Now;
            publisher.UpdateDate = DateTime.Now;
            publisher.IsDelete = false;
            _db.Publishers.Add(publisher);
            await _db.SaveChangesAsync();
            return publisher;
        }

        public async Task<bool> DeletePublisherAsync(int pusblisherId)
        {
           var publisher = await _db.Publishers.FindAsync(pusblisherId);
            if (publisher == null)
            {
                return false;
            }
            _db.Publishers.Remove(publisher);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Publisher>> GetAllPublishersAsync()
        {
            return await _db.Publishers.OrderBy(x => x.CreateDate).ToListAsync();
        }

        public async Task<Publisher?> GetPublisherByIdAsync(int pusblisherId)
        {
            return await _db.Publishers.Where(x=>x.PublisherId==pusblisherId).FirstOrDefaultAsync();
        }

        public async Task<Publisher?> PatchPublisherAsync(int pusblisherId, Dictionary<string, object> updates)
        {
            var publisher = await _db.Publishers.FirstOrDefaultAsync(b => b.PublisherId == pusblisherId);
            if (publisher == null)
            {
                return null;
            }

            // Use reflection to apply valid updates only
            var bookType = typeof(Publisher);
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
                    prop.SetValue(publisher, newValue);
                }
            }
            publisher.UpdateDate = DateTime.Now;
            publisher.IsDelete = false;
            await _db.SaveChangesAsync();
            return publisher;
        }

        public async Task<Publisher?> UpdatePublisherAsync(int pusblisherId, Publisher publisher)
        {
            var publisherToUpdate = await _db.Publishers.Where(b => b.PublisherId == pusblisherId).FirstOrDefaultAsync();
            if (publisherToUpdate == null)
            {
                return null;
            }
            publisherToUpdate.UpdateDate = DateTime.Now;
            publisherToUpdate.IsDelete = false;
            publisherToUpdate.PublisherName = publisher.PublisherName;
            _db.Publishers.Update(publisherToUpdate);
            await _db.SaveChangesAsync();
            return publisherToUpdate;
        }
    }
}
